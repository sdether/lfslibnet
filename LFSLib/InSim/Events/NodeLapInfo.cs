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
using FullMotion.LiveForSpeed.InSim.Packets.Support;

namespace FullMotion.LiveForSpeed.InSim.Events
{
	/// <summary>
	/// Node and Lap infromat
	/// </summary>
	public class NodeLapInfo
	{
		#region Static Members ########################################################################
    private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    #endregion

		#region Member Variables ######################################################################
		private Packets.Support.NodeLap nodeLap;
		#endregion

		#region Constructors ##########################################################################
    internal NodeLapInfo(Packets.Support.NodeLap nodeLap)
		{
			this.nodeLap = nodeLap;
		}
		#endregion

		#region Properties ############################################################################
		/// <summary>
		/// The lap the car is on
		/// </summary>
		public int Lap
		{
      get
      {
        return nodeLap.Lap;
      }
		}
		/// <summary>
		/// The node in the current lap the car is at
		/// </summary>
		public int Node
		{
			get
      {
        return nodeLap.Node;
      }
		}
		/// <summary>
		/// The player's unique Id
		/// </summary>
		public byte PlayerId
		{
			get { return nodeLap.PLID; }
		}

    /// <summary>
    /// Position in race
    /// </summary>
    public int Position
    {
      get { return nodeLap.Position; }
    }
		#endregion
	}
}
