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
	/// Race Time Event
	/// </summary>
	public class RaceTime : EventArgs
	{
		#region Static Members ########################################################################
    private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    #endregion

		#region Member Variables ######################################################################
		private Packets.IS_SMALL packet;
		private TimeSpan elapsed;
		#endregion

		#region Constructors ##########################################################################
    internal RaceTime(Packets.IS_SMALL packet)
		{
      this.packet = packet;

			elapsed = new TimeSpan(0,0,0,0,(int)packet.UVal*10);
		}
		#endregion

		#region Properties ############################################################################
		/// <summary>
		/// Time elapsed since start of race or replace
		/// </summary>
		public TimeSpan ElapsedTime
		{
			get	{ return elapsed; }
		}
		#endregion

		#region Methods ###############################################################################
		#endregion

		#region Private Methods #######################################################################
		#endregion
	}
}
