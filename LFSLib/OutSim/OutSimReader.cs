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
using FullMotion.LiveForSpeed;
using FullMotion.LiveForSpeed.OutSim.PacketInfo;
using FullMotion.LiveForSpeed.OutSim.Events;
using FullMotion.LiveForSpeed.OutSim.Messages;
using FullMotion.LiveForSpeed.OutSim.EventHandlers;

namespace FullMotion.LiveForSpeed.OutSim
{
	/// <summary>
	/// InSimReader uses a separate thread to wait for incoming messages from an LFS host.
	/// Other objects can subscribe to the received events for processing
	/// </summary>
	internal class OutSimReader : AbstractLfsPacketReader, FullMotion.LiveForSpeed.OutSim.IOutSimReader
	{
		#region Static Members ########################################################################
    private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    #endregion

    #region Member Variables ######################################################################
		#endregion

		#region Constructors ##########################################################################
		internal OutSimReader(int port)
      : base(port)
    {
      log.Debug("OutSimReader started");
    }
    #endregion

		#region Properties ############################################################################
		#endregion

		#region Methods ###############################################################################
		#endregion

		#region Private Methods #######################################################################
		#endregion

		#region Events ################################################################################
		#endregion
	}
}

namespace FullMotion.LiveForSpeed.OutSim.EventHandlers
{
  internal delegate void PhysicsMessageHandler(PhysicsMessage physicsMessage);
}
