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

namespace FullMotion.LiveForSpeed.InSim.Enums
{
	/// <summary>
	/// In Game Weather
	/// </summary>
	public enum Weather : byte
	{
		/// <summary>
		/// Day
		/// </summary>
		Day = 0,
		/// <summary>
		/// Evening
		/// </summary>
		Evening = 1,
		/// <summary>
		/// Dusk Overcast
		/// </summary>
		DuskOvercast = 2,
    ///// <summary>
    ///// Spare, so we don't throw an exception if LFS adds another before we notice
    ///// </summary>
    //SpareWeather3 = 3,
    ///// <summary>
    ///// Spare, so we don't throw an exception if LFS adds another before we notice
    ///// </summary>
    //SpareWeather4 = 4,
    ///// <summary>
    ///// Spare, so we don't throw an exception if LFS adds another before we notice
    ///// </summary>
    //SpareWeather5 = 5,
    ///// <summary>
    ///// Spare, so we don't throw an exception if LFS adds another before we notice
    ///// </summary>
    //SpareWeather6 = 6,
    ///// <summary>
    ///// Spare, so we don't throw an exception if LFS adds another before we notice
    ///// </summary>
    //SpareWeather7 = 7,
    ///// <summary>
    ///// Spare, so we don't throw an exception if LFS adds another before we notice
    ///// </summary>
    //SpareWeather8 = 8,
	}
}
