/* ------------------------------------------------------------------------- *
 * Copyright (C) 2023 Arne Claassen
 *
 * Permission is hereby granted, free of charge, to any person obtaining a
 * copy of this software and associated documentation files (the “Software”),
 * to deal in the Software without restriction, including without limitation
 * the rights to use, copy, modify, merge, publish, distribute, sublicense,
 * and/or sell copies of the Software, and to permit persons to whom the
 * Software is furnished to do so, subject to the following conditions:
 *
 *
 * The above copyright notice and this permission notice shall be included
 * in all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS
 * OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
 * THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
 * DEALINGS IN THE SOFTWARE.
 * ------------------------------------------------------------------------- */
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using log4net;
using FullMotion.LiveForSpeed.InSim.Packets;
using FullMotion.LiveForSpeed.InSim;
using FullMotion.LiveForSpeed.OutSim;
using FullMotion.LiveForSpeed.OutGauge;
using FullMotion.LiveForSpeed.OutSim.PacketInfo;
using FullMotion.LiveForSpeed.OutGauge.PacketInfo;

namespace FullMotion.LiveForSpeed
{
  /// <summary>
  /// AbstractLfsPacketReader uses a separate thread to wait for incoming messages from an LFS host.
  /// Other objects can subscribe to the received events for processing
  /// </summary>
  internal class AbstractLfsPacketReader : ILfsReader
  {
    #region Static Members ########################################################################
    private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    #endregion

    #region Member Variables ######################################################################
    Thread listenerThread;
    Socket listener;
    int port;
    IPAddress[] localIPs;
    EndPoint remoteEP;
    #endregion

    #region Constructors ##########################################################################
    /// <summary>
    /// Public constructor requiring a port to listen on
    /// </summary>
    /// <param name="port"></param>
    public AbstractLfsPacketReader(int port)
    {
      this.port = port;
      localIPs = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
      remoteEP = new IPEndPoint(IPAddress.Any, this.port);
      try
      {
        //Starting the UDP Server thread.
        listenerThread = new Thread(new ThreadStart(Listen));
        listenerThread.Start();
        log.Debug("Started Listener Thread on port " + this.port);
      }
      catch (Exception e)
      {
        log.Debug("Listener Thread unable to start", e);
        listenerThread.Abort();
      }
    }
    #endregion

    #region Properties ############################################################################
    public IPAddress RemoteIP
    {
      get { return ((IPEndPoint)remoteEP).Address; }
    }
    public IPAddress[] LocalIPs
    {
      get { return localIPs; }
    }
    #endregion

    #region Methods ###############################################################################

    public void Stop()
    {
      log.Debug("shutting down");
      try
      {
        listener.Shutdown(SocketShutdown.Both);
      } catch { }
      try
      {
        listener.Close();
      }
      catch { }
      if (listenerThread != null)
      {
        listenerThread.Abort();
        listenerThread.Join();
      }
      log.Debug("shut down");
    }
    #endregion

    #region Private Methods #######################################################################
    private void Listen()
    {
      try
      {
        IPEndPoint ipep = new IPEndPoint(IPAddress.Any, this.port);

        listener = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        listener.Bind(ipep);

        log.Debug("Listening...");

        Byte[] received = new Byte[512];

        while (true)
        {
          int bytesReceived = listener.ReceiveFrom(received, ref remoteEP);

          log.Debug(String.Format("received {0} bytes", bytesReceived));
          if ((bytesReceived == 68 || bytesReceived == 64) && received[3] == 0)
          {
            if (bytesReceived == 68)
            {
              log.Debug("Received OutSim Packet w/Id");
            }
            else
            {
              log.Debug("Received OutSim Packet w/o Id");
            }
            if (OutSimPacketReceived != null)
            {
              byte[] raw = new byte[68];
              Array.Copy(received, 0, raw, 0, bytesReceived);
              OutSimPack packet = new OutSimPack(raw);
              OutSimPacketReceived(this, new OutSimPacketEventArgs(packet));
            }
          }
          else if ((bytesReceived == 92 || bytesReceived == 96) && received[3] == 0)
          {
            if (bytesReceived == 92)
            {
              log.Debug("Received OutGauge Packet w/o Id");
            }
            else
            {
              log.Debug("Received OutGauge Packet w/ Id");
            }
            if (OutGaugePacketReceived != null)
            {
              byte[] raw = new byte[96];
              Array.Copy(received, 0, raw, 0, bytesReceived);
              OutGaugePack packet = new OutGaugePack(raw);
              OutGaugePacketReceived(this, new OutGaugePacketEventArgs(packet));
            }
          }
          else
          {
            int packetByteCount = PacketFactory.CheckHeader(received, 0);
            if (packetByteCount == 0)
            {
              log.Debug("Unknown packet");
            }
            else
            {
              ILfsInSimPacket packet = PacketFactory.GetPacket(received, 0);
              log.Debug("InSim packet: " + packet.PacketType);
              if (InSimPacketReceived != null)
              {
                InSimPacketReceived(this, new InSimPacketEventArgs(packet));
              }
            }
          }

          log.Debug("waiting");

        }
      }
      catch (SocketException se)
      {
        if (se.ErrorCode == 10004)
        {
          log.Error("been asked to shut down", se);
        }
        else
        {
          log.Error("A Socket Exception has occurred!" + se.ToString(), se);
        }
      }
      catch (Exception e)
      {
        log.Error("An unknown exception happened in our packet reader", e);
      }
    }
    #endregion

    #region Events ################################################################################
    public event InSimPacketReceivedEventHandler InSimPacketReceived;

    public event OutSimPacketReceivedEventHandler OutSimPacketReceived;

    public event OutGaugePacketReceivedEventHandler OutGaugePacketReceived;
    #endregion
  }
}