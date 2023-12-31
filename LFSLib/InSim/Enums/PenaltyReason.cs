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
using System.Collections.Generic;
using System.Text;

namespace FullMotion.LiveForSpeed.InSim.Enums
{
  // Penalty reasons

  /// <summary>
  /// Reason for assessed Penalty
  /// </summary>
  public enum PenaltyReason : byte
  {
    /// <summary>
    /// unknown or cleared penalty
    /// </summary>
    Unknown,		// 0 - unknown or cleared penalty
    /// <summary>
    /// penalty given by admin
    /// </summary>
    Admin,			// 1 - penalty given by admin
    /// <summary>
    /// wrong way driving
    /// </summary>
    WrongWay,		// 2 - wrong way driving
    /// <summary>
    /// starting before green light
    /// </summary>
    FalseStart,	// 3 - starting before green light
    /// <summary>
    /// speeding in pit lane
    /// </summary>
    Speeding,		// 4 - speeding in pit lane
    /// <summary>
    /// stop-go pit stop too short
    /// </summary>
    StopShort,	// 5 - stop-go pit stop too short
    /// <summary>
    /// compulsory stop is too late
    /// </summary>
    StopLate,		// 6 - compulsory stop is too late
    //Num
  }
}
