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
  /// <summary>
  /// Pit Lane Facts
  /// </summary>
  public enum Pitlane : byte
  {
    /// <summary>
    /// left pit lane
    /// </summary>
    Exit,		// 0 - left pit lane
    /// <summary>
    /// entered pit lane
    /// </summary>
    Enter,		// 1 - entered pit lane
    /// <summary>
    /// entered for no purpose
    /// </summary>
    NoPurpose,	// 2 - entered for no purpose
    /// <summary>
    /// entered for drive-through
    /// </summary>
    DriveThrough,			// 3 - entered for drive-through
    /// <summary>
    /// entered for stop-go
    /// </summary>
    StopGo,			// 4 - entered for stop-go
    /// <summary>
    /// Num
    /// </summary>
    Num
  }
}
