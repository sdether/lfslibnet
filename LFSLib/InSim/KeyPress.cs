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

namespace FullMotion.LiveForSpeed.InSim
{
	/// <summary>
	/// A keypress object to send to LFS
	/// </summary>
	public class KeyPress
	{
		#region Member Variables ######################################################################
		private char key = '\0';
		private bool ctrlOn = false;
		private bool shiftOn = false;
		#endregion

		#region Constructors ##########################################################################
		/// <summary>
		/// Create a new keypress without any modifier keys
		/// </summary>
		/// <param name="key"></param>
		public KeyPress(char key)
		{
			this.key = key;
		}

		/// <summary>
		/// Create a blank keypress (useful if one keypress object is to be re-used
		/// </summary>
		public KeyPress()
		{
		}

		/// <summary>
		/// Create a fully defined key press object
		/// </summary>
		/// <param name="key"></param>
		/// <param name="ctrlOn"></param>
		/// <param name="shiftOn"></param>
		public KeyPress(char key, bool ctrlOn, bool shiftOn)
		{
			this.key = key;
			this.ctrlOn = ctrlOn;
			this.shiftOn = shiftOn;
		}
		#endregion

		#region Properties ############################################################################
		/// <summary>
		/// The key to be sent
		/// </summary>
		public char Key
		{
			get { return key; }
			set { key = value; }
		}
		/// <summary>
		/// Is the Control Key pressed?
		/// </summary>
		public bool CtrlOn
		{
			get { return ctrlOn; }
			set { ctrlOn = value; }
		}
		/// <summary>
		/// Is the shift key pressed?
		/// </summary>
		public bool	ShiftOn
		{
			get { return shiftOn; }
			set { shiftOn = value; }
		}
		#endregion
	}
}