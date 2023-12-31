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
using FullMotion.LiveForSpeed.InSim.Packets.Support;

namespace FullMotion.LiveForSpeed.InSim
{
	/// <summary>
	/// A simple 3D Vector class
	/// </summary>
	public class Vector
	{
		#region Member Variables ######################################################################
		private Vec vec;
		#endregion

		#region Constructors ##########################################################################
		internal Vector(Vec vec)
		{
			this.vec = vec;
		}
		/// <summary>
		/// Create a new vector object
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		public Vector(int x, int y, int z)
		{
			this.vec = new Vec();
			vec.X = x;
			vec.Y = y;
			vec.Z = z;
		}
		#endregion

		#region Properties ############################################################################
		/// <summary>
		/// X axis position in the game world. 65536 = 1 meter
		/// </summary>
		public int X
		{
			get { return vec.X; }
		}
		/// <summary>
		/// Y axis position in the game world. 65536 = 1 meter
		/// </summary>
		public int Y
		{
			get { return vec.Y; }
		}
		/// <summary>
		/// Z axis position in the game world. 65536 = 1 meter
		/// </summary>
		public int Z
		{
			get { return vec.Z; }
		}

		internal Vec Vec
		{
			get { return this.vec; }
		}
		#endregion
	}
}
