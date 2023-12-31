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

namespace FullMotion.LiveForSpeed.InSim.Events
{
	/// <summary>
	/// Vote Action Notification
	/// </summary>
	public class VoteAction : VoteEventArgs
	{
		#region Static Members ########################################################################
    private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    #endregion

		#region Member Variables ######################################################################
    private Enums.Vote vote;
		private bool isFinal = false;
		private byte connectionId = 0;
		#endregion

		#region Constructors ##########################################################################
    internal VoteAction(Packets.IS_VTN packet)
		{
      vote = packet.Action;
      connectionId = packet.UCID;
		}

    internal VoteAction(Packets.IS_SMALL packet)
    {
      isFinal = true;
      vote = (Enums.Vote)packet.UVal;
    }
    #endregion

		#region Properties ############################################################################
		/// <summary>
		/// True if the event is a notification of a host ending or being left
		/// </summary>
		public bool IsFinalResult
		{
			get { return isFinal; }
		}

		/// <summary>
    /// Id of Connection issuing vote. Zero if <see cref="IsFinalResult"/> is true or if the host is voting
		/// </summary>
    public byte ConnectionId
		{
      get { return connectionId; }
		}

		/// <summary>
		/// The vote action triggered
		/// </summary>
		public Enums.Vote Vote
		{
			get { return vote; }
		}
		#endregion

		#region Methods ###############################################################################
		#endregion

		#region Private Methods #######################################################################
		#endregion
	}
}
