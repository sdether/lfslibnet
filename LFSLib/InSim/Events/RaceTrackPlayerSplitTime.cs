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
	/// Event sent each time a split is reached by a player
	/// </summary>
  public class RaceTrackPlayerSplitTime : RaceTrackEvent
	{
		#region Static Members ########################################################################
    private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    #endregion

		#region Member Variables ######################################################################
		private Packets.IS_SPX packet;
		#endregion

		#region Constructors ##########################################################################
    internal RaceTrackPlayerSplitTime(Packets.IS_SPX packet)
		{
      this.packet = packet;
      log.Debug("RaceTrackPlayerSplitTime event created");
		}
		#endregion

		#region Properties ############################################################################
    /// <summary>
    /// The Split time as a timespan object
    /// </summary>
    public TimeSpan SplitTime
    {
      get { return new TimeSpan(0, 0, 0, 0, (int)packet.STime); }
    }
    /// <summary>
    /// The Total time as a timespan object
    /// </summary>
    public TimeSpan TotalTime
    {
      get { return new TimeSpan(0, 0, 0, 0, (int)packet.ETime); }
    }
    /// <summary>
		/// The player's unique Id
		/// </summary>
		public byte PlayerId
		{
			get { return packet.PLID; }
		}
    /// <summary>
		/// Which split does this event concern?
		/// </summary>
		public int Split
		{
			get { return packet.Split; }
		}
    /// <summary>
    /// Current Penalty
    /// </summary>
    public Enums.Penalty Penalty
    {
      get { return packet.Penalty; }
    }
    /// <summary>
    /// Number of Pit Stops
    /// </summary>
    public int NumberOfPitStops
    {
      get { return packet.NumStops; }
    }
    #endregion

		#region Methods ###############################################################################
		#endregion

		#region Private Methods #######################################################################
		#endregion
	}
}
