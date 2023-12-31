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

namespace FullMotion.LiveForSpeed.InSim
{
  /// <summary>
  /// Collection of Tire Compounds for a car
  /// </summary>
  public class Tyres
  {
    Packets.Support.Tyres tyres;

    internal Tyres(Packets.Support.Tyres tyres)
    {
      this.tyres = tyres;
    }

    /// <summary>
    /// Rear Left Tire
    /// </summary>
    public Enums.Tyre RearLeft { get { return tyres.RearLeft; } }
    /// <summary>
    /// Rear Right Tire
    /// </summary>
    public Enums.Tyre RearRight { get { return tyres.RearRight; } }
    /// <summary>
    /// Front Left Tire
    /// </summary>
    public Enums.Tyre FrontLeft { get { return tyres.FrontLeft; } }
    /// <summary>
    /// Front Right Tire
    /// </summary>
    public Enums.Tyre FrontRight { get { return tyres.FrontRight; } }

  }
}
