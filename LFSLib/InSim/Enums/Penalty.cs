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
  // Penalty values (VALID means the penalty can now be cleared)

  /// <summary>
  /// Penalty issued by Race Control
  /// </summary>
  public enum Penalty : byte
  {
    /// <summary>
    /// No Penalty
    /// </summary>
    None,		// 0		
    /// <summary>
    /// Drive Through Penalty
    /// </summary>
    DriveThrough,			// 1
    /// <summary>
    /// Drive Through Penalty (has been completed)
    /// </summary>
    DriveThroughValid,	// 2
    /// <summary>
    /// Stop And Go Penalty
    /// </summary>
    StopGo,			// 3
    /// <summary>
    /// Stop And Go Penalty (has been completed)
    /// </summary>
    StopGoValid,	// 4
    /// <summary>
    /// 30 Second Time Penalty
    /// </summary>
    Time30,			// 5
    /// <summary>
    /// 45 Second Time Penalty
    /// </summary>
    time45,			// 6
    //Num
  }
}
