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
  /// Helper class for accessing LFS color control sequences for changing colors mid-string
  /// </summary>
  public class TextColor
  {
    /// <summary>
    /// Black
    /// </summary>
    public static readonly string Black = "^0";

    /// <summary>
    /// Red
    /// </summary>
    public static readonly string Red = "^1";
    /// <summary>
    /// Green
    /// </summary>
    public static readonly string Green = "^2";
    /// <summary>
    /// Yellow
    /// </summary>
    public static readonly string Yellow = "^3";
    /// <summary>
    /// Blue
    /// </summary>
    public static readonly string Blue = "^4";
    /// <summary>
    /// Cyan
    /// </summary>
    public static readonly string Cyan = "^5";
    /// <summary>
    /// Light Blue
    /// </summary>
    public static readonly string LightBlue = "^6";

    /// <summary>
    /// White
    /// </summary>
    public static readonly string White = "^7";

    /// <summary>
    /// Return to default text color
    /// </summary>
    public static readonly string Default = "^8";

    private TextColor()
    {
    }
  }
}
