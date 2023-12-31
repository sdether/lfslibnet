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
using System.Text;
using System.Net;
using log4net;
using FullMotion.LiveForSpeed.OutSim;
using FullMotion.LiveForSpeed.OutSim.Events;
using FullMotion.LiveForSpeed.OutSim.Messages;

namespace FullMotion.LiveForSpeed.OutSim
{
  /// <summary>
  /// The OutSimHandler listens for OutSim packets from Live For Speed and exposes them via
  /// the <see cref="PhysicsEvent"/> event
  /// </summary>
  public class OutSimHandler
  {
    #region Static Members ########################################################################
    private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    #endregion

    #region Member Variables ######################################################################
    IOutSimReader reader;
    int port;
    #endregion

    #region Constructors ##########################################################################
    /// <summary>
    /// Create a new OutSimHandler for listening on a specified port. Does not start listening until
    /// <see cref="Initialize"/> is called.
    /// </summary>
    /// <param name="port">Port that the handler will listen on</param>
    public OutSimHandler(int port)
    {
      this.port = port;
    }
    #endregion

    #region Properties ############################################################################
    /// <summary>
    /// The IP address the handler is receiving OutSim data from
    /// </summary>
    public IPAddress RemoteIP
    {
      get
      {
        if (reader == null)
        {
          return new IPAddress(0);
        }
        return reader.RemoteIP;
      }
    }
    /// <summary>
    /// List of IP addresses this handler is listening on
    /// </summary>
    public IPAddress[] LocalIPs
    {
      get
      {
        if (reader == null)
        {
          return new IPAddress[0];
        }
       return reader.LocalIPs;
      }
    }
    #endregion

    #region Methods ###############################################################################
    /// <summary>
    /// Start listening on the specified port. If there already is a listener, it is shut down first
    /// </summary>
    public void Initialize()
    {
      if (reader != null)
      {
        Close();
      }
      reader = new OutSimReader(port);
      reader.OutSimPacketReceived += new OutSimPacketReceivedEventHandler(reader_OutSimPacketReceived);
    }

    /// <summary>
    /// Shut down the listener
    /// </summary>
    public void Close()
    {
      if (reader != null)
      {
        reader.Stop();
        reader.OutSimPacketReceived -= new OutSimPacketReceivedEventHandler(reader_OutSimPacketReceived);
        reader = null;
      }
    }
    #endregion

    #region Private Methods #######################################################################
    private void reader_OutSimPacketReceived(ILfsReader sender, OutSimPacketEventArgs e)
    {
      if (Updated != null)
      {
        PhysicsState phys = new PhysicsState(e.Packet);
        Updated(this, phys);
      }
    }
    #endregion

    #region Events ################################################################################
    /// <summary>
    /// The delegate used by the <see cref="PhysicsEvent"/> event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="physicsState"></param>
    public delegate void PhysicsEvent(object sender, PhysicsState physicsState);
    /// <summary>
    /// This event is fired off for every OutSim packet the listener receives from Live For Speed
    /// </summary>
    public event PhysicsEvent Updated;
    #endregion
  }
}
