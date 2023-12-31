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
using log4net;

namespace FullMotion.LiveForSpeed.InSim.Events
{
	/// <summary>
	/// Event describing a player changing its name
	/// </summary>
  public class RaceTrackPlayerRename : RaceTrackEvent
	{
		#region Static Members ########################################################################
    private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    #endregion

		#region Member Variables ######################################################################
		private Packets.IS_CPR packet;
		#endregion

		#region Constructors ##########################################################################
    internal RaceTrackPlayerRename(Packets.IS_CPR packet)
		{
      this.packet = packet;

			log.Debug("RaceTrackPlayerRename event created");
		}
		#endregion

		#region Properties ############################################################################
		/// <summary>
		/// Username
		/// </summary>
		public string PlayerName
		{
			get { return packet.PName.Value; }
		}
		/// <summary>
		/// License Plate used by the player's car
		/// </summary>
		public string Plate
		{
			get { return packet.Plate.Value; }
		}
    /// <summary>
    /// DEPRECATED: The connection's Id
    /// </summary>
    [Obsolete("Replaced with ConnectionId in 0.18+")]
    public byte ConnectionID
    {
      get { return packet.UCID; }
    }
    /// <summary>
    /// The connection's Id
    /// </summary>
    public byte ConnectionId
    {
      get { return packet.UCID; }
    }
    #endregion

		#region Methods ###############################################################################
		#endregion

		#region Private Methods #######################################################################
		#endregion
	}
}
