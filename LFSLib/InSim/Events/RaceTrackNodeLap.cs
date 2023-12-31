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
	/// Node and Lap Event describing each players current position in the race as current lap
	/// and node in that lap
	/// </summary>
  public class RaceTrackNodeLap : RaceTrackEvent
	{
		#region Static Members ########################################################################
    private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    #endregion

		#region Member Variables ######################################################################
		private Packets.IS_NLP packet;
		private NodeLapInfo[] nodeLaps;
		#endregion

		#region Constructors ##########################################################################
    internal RaceTrackNodeLap(Packets.IS_NLP packet)
		{
      this.packet = packet;

      nodeLaps = new NodeLapInfo[packet.NumP];
			for(int i = 0; i < packet.NumP; i++)
			{
				nodeLaps[i] = new NodeLapInfo(packet.Info[i]);
			}
			log.Debug("RaceTrackNodeLap event created");
		}
		#endregion

		#region Properties ############################################################################
		/// <summary>
		/// Array of LapInfo objects, ordered by player number
		/// </summary>
		public NodeLapInfo[] LapInfo
		{
			get { return nodeLaps; }
		}
		#endregion

		#region Methods ###############################################################################
		#endregion

		#region Private Methods #######################################################################
		#endregion
	}
}
