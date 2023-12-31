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
  /// Type of Lap Information
  /// </summary>
  public enum LapInfoType
  {
    /// <summary>
    /// Actual Lap Count
    /// </summary>
    Lap,
    /// <summary>
    /// Race Length in Hours
    /// </summary>
    Hour,
    /// <summary>
    /// Practice Session
    /// </summary>
    Practice
  }

  /// <summary>
  /// LapInfo encapsulates the race setups' definition of the race length
  /// </summary>
  public class LapInfo
  {
    int value = 0;
    LapInfoType type;

    internal LapInfo(ushort lap)
    {
      if (lap == 0)
      {
        type = LapInfoType.Practice;
      }
      else if (lap < 100)
      {
        value = lap;
        type = LapInfoType.Lap;
      }
      else if (lap < 191)
      {
        value = (lap - 100) * 10 + 100;
        type = LapInfoType.Lap;
      }
      else
      {
        value = lap - 190;
        type = LapInfoType.Hour;
      }
    }

    /// <summary>
    /// Type of information represented by <see cref="Value"/>
    /// </summary>
    public LapInfoType Type { get { return type; } }

    /// <summary>
    /// Value of the Lap Info Object
    /// </summary>
    public int Value { get { return value; } }
  }
}
