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
  /// Type of Tyre in use
  /// </summary>
  public enum Tyre : byte
  {
    /// <summary>
    /// R1 Compound
    /// </summary>
    R1,			// 0
    /// <summary>
    /// R2 Compound
    /// </summary>
    R2,			// 1
    /// <summary>
    /// R3 Compound
    /// </summary>
    R3,			// 2
    /// <summary>
    /// R4 Compound
    /// </summary>
    R4,			// 3
    /// <summary>
    /// High Performance Road Car Tyre
    /// </summary>
    RoadSuper,	// 4
    /// <summary>
    /// Regular Road Car Tyre
    /// </summary>
    RoadNormal,	// 5
    /// <summary>
    /// Combination Off- and On-road Type
    /// </summary>
    Hybrid,		// 6
    /// <summary>
    /// Off-Road Tyre
    /// </summary>
    Knobbly,		// 7
    //Num,
    /// <summary>
    /// Tyre was not changed from last state
    /// </summary>
    NotChanged = 255,
  }
  //const int NOT_CHANGED = 255;
}
