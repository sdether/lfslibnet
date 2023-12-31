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
using log4net;

namespace FullMotion.LiveForSpeed.InSim
{
	/// <summary>
	/// InSimWriter handles outbound messages to an LFS Host
	/// </summary>
	internal class InSimWriter : IInSimWriter
	{
		#region Static Members ########################################################################
    private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    #endregion

		#region Member Variables ######################################################################
		UdpClient connection;
		#endregion

		#region Constructors ##########################################################################
		/// <summary>
		/// Public constructor requring a host and port to connect to
		/// </summary>
		/// <param name="server"></param>
		/// <param name="port"></param>
		public InSimWriter(string server, int port)
		{
			try
			{
				connection = new UdpClient(server, port);
			}
			catch(Exception e)
			{
				throw new InSim.Exceptions.InSimHandlerException.ConnectionFailed(e);
			}
		}
		#endregion

		#region Properties ############################################################################
		#endregion

		#region Methods ###############################################################################

    /// <summary>
    /// Send an arbitraty IClientLfsInSimPacket
    /// </summary>
    /// <param name="packet"></param>
    public void Send(Packets.IClientLfsInSimPacket packet)
    {
      log.Debug("Sent packet: " + packet.PacketType);
      byte[] packetBytes = packet.GetBytes();
      connection.Send(packetBytes, packetBytes.Length);

    }
		/// <summary>
		/// Send a MsgClose and close the connection
		/// </summary>
		public void Close()
		{
      this.Send(Packets.PacketFactory.GetClosePacket());
			this.connection.Close();
		}
    #endregion
	}
}
