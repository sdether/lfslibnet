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
	/// CarInfo is used by the <see cref="RaceTrackMultiCarInfo"/> Event to store information about
	/// each car
	/// </summary>
  public class CarInfo : EventArgs
	{
		#region Static Members ########################################################################
    private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    #endregion

		#region Member Variables ######################################################################
		private Packets.Support.CompCar compCar;
		private Vector vector;
		#endregion

		#region Constructors ##########################################################################
    internal CarInfo(Packets.Support.CompCar compCar)
		{
			this.compCar = compCar;
			this.vector = new Vector(this.compCar.X,this.compCar.Y,this.compCar.Z);
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
        return compCar.Lap;
      }
    }
    /// <summary>
    /// The node in the current lap the car is at
    /// </summary>
    public int Node
    {
      get
      {
        int node = compCar.Node;
        return node;
      }
    }
		/// <summary>
		/// The player's unique Id
		/// </summary>
		public byte PlayerId
		{
			get { return compCar.PLID; }
		}

		/// <summary>
		/// DEPRECATED: Use <see cref="Location"/>
		/// </summary>
    [Obsolete]
		public Vector Position
		{
			get { return vector; }
		}

		/// <summary>
		/// The location of the car on the Track (65536 = 1m)
		/// </summary>
		public Vector Location
		{
			get { return vector; }
		}

    /// <summary>
    /// Position of the car in the car
    /// </summary>
    public int RacePosition
    {
      get { return (int)compCar.Position; }
    }

		/// <summary>
		/// The speed of the car in meters/second
		/// </summary>
		public float Speed
		{
			get { return (int)compCar.Speed*100/32768; }
		}
		/// <summary>
		/// The direction of the Car's motion in degrees off the world y axis.
		/// Positive value is anti-clockwise
		/// </summary>
		public float Direction
		{
			get { return compCar.Direction*180/32768; }
		}
		/// <summary>
		/// The heading of the car's forward axis in degrees off the world y axis
		/// Positive value is anti-clockwise
		/// </summary>
		public float Heading
		{
			get { return compCar.Heading*180/32768; }
		}
		/// <summary>
		/// The rate of change in the heading as a signed degree measurement.
		/// 0 is no change in heading, otherwise the degrees per second anti-clockwise
		/// </summary>
		public float AngularVelocity
		{
			get { return compCar.AngVel*180/8192; }
		}
		#endregion
	}
}
