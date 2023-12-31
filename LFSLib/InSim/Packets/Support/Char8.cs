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
using System.Runtime.InteropServices;
using System.Text;
using FullMotion.LiveForSpeed.Util;

namespace FullMotion.LiveForSpeed.InSim.Packets.Support
{
  [StructLayout(LayoutKind.Sequential)]
  struct Char8
  {
    byte c0;
    byte c1;
    byte c2;
    byte c3;
    byte c4;
    byte c5;
    byte c6;
    byte c7;

    public Char8(string value)
    {
      byte[] bytes = CharHelper.GetBytes(value, 8, true);
      c0 = bytes[0];
      c1 = bytes[1];
      c2 = bytes[2];
      c3 = bytes[3];
      c4 = bytes[4];
      c5 = bytes[5];
      c6 = bytes[6];
      c7 = bytes[7];
    }
    public string Value
    {
      get
      {
        return CharHelper.GetString(new byte[] { c0, c1, c2, c3, c4, c5, c6, c7 });
      }
      set
      {
        byte[] bytes = CharHelper.GetBytes(value, 8, true);
        c0 = bytes[0];
        c1 = bytes[1];
        c2 = bytes[2];
        c3 = bytes[3];
        c4 = bytes[4];
        c5 = bytes[5];
        c6 = bytes[6];
        c7 = bytes[7];
      }
    }
  }
}
