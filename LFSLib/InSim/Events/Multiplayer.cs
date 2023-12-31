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
using log4net;
using FullMotion.LiveForSpeed.Util;

namespace FullMotion.LiveForSpeed.InSim.Events
{
	/// <summary>
	/// Multiplayer Notification
	/// </summary>
	public class Multiplayer : EventArgs
	{
		#region Static Members ########################################################################
    private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    #endregion

		#region Member Variables ######################################################################
		private bool isEnd = false;
    private bool isHost = false;
		private string hostName;
    private byte requestId = 0;
		#endregion

		#region Constructors ##########################################################################
    internal Multiplayer(Packets.IS_ISM packet)
    {
      isHost = (packet.Host == Enums.ServerType.Host);
      hostName = CharHelper.GetString(packet.HName);
      if (hostName == string.Empty)
      {
        isEnd = true;
      }
    }
    internal Multiplayer(Packets.IS_TINY packet)
    {
      this.isEnd = true;
    }
		#endregion

		#region Properties ############################################################################
    /// <summary>
    /// Id of the request that caused this event to be triggered. 0 if the event trigger was not
    /// manually requested or the event signifies the end of Multiplayer (<see cref="IsEnd"/>
    /// will be true as well)
    /// </summary>
    public byte RequestId
    {
      get { return requestId; }
    }

		/// <summary>
		/// True if the event is a notification of a host ending or being left
		/// </summary>
		public bool IsEnd
		{
			get { return isEnd; }
		}

		/// <summary>
		/// True if the sender is the Host instead of a Guest. Always false if <see cref="IsEnd"/> is true.
		/// </summary>
		public bool IsHost
		{
			get { return isHost; }
		}

		/// <summary>
		/// The name of the Host joined or started. Null if <see cref="IsEnd"/> is true.
		/// </summary>
		public string HostName
		{
			get { return hostName; }
		}
		#endregion

		#region Methods ###############################################################################
		#endregion

		#region Private Methods #######################################################################
		#endregion
	}
}
