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
  struct Char24
  {
    byte c0;
    byte c1;
    byte c2;
    byte c3;
    byte c4;
    byte c5;
    byte c6;
    byte c7;
    byte c8;
    byte c9;
    byte c10;
    byte c11;
    byte c12;
    byte c13;
    byte c14;
    byte c15;
    byte c16;
    byte c17;
    byte c18;
    byte c19;
    byte c20;
    byte c21;
    byte c22;
    byte c23;

    public Char24(string value)
    {
      byte[] bytes = CharHelper.GetBytes(value, 24, true);
      c0 = bytes[0];
      c1 = bytes[1];
      c2 = bytes[2];
      c3 = bytes[3];
      c4 = bytes[4];
      c5 = bytes[5];
      c6 = bytes[6];
      c7 = bytes[7];
      c8 = bytes[8];
      c9 = bytes[9];
      c10 = bytes[10];
      c11 = bytes[11];
      c12 = bytes[12];
      c13 = bytes[13];
      c14 = bytes[14];
      c15 = bytes[15];
      c16 = bytes[16];
      c17 = bytes[17];
      c18 = bytes[18];
      c19 = bytes[19];
      c20 = bytes[20];
      c21 = bytes[21];
      c22 = bytes[22];
      c23 = bytes[23];
    }

    public string Value
    {
      get
      {
        return CharHelper.GetString(new byte[] { 
          c0, c1, c2, c3, c4, c5, c6, c7, c8, c9, c10, c11, c12,
          c13, c14, c15, c16, c17, c18, c19, c20, c21, c22, c23 });
      }
      set
      {
        byte[] bytes = CharHelper.GetBytes(value, 24, true);
        c0 = bytes[0];
        c1 = bytes[1];
        c2 = bytes[2];
        c3 = bytes[3];
        c4 = bytes[4];
        c5 = bytes[5];
        c6 = bytes[6];
        c7 = bytes[7];
        c8 = bytes[8];
        c9 = bytes[9];
        c10 = bytes[10];
        c11 = bytes[11];
        c12 = bytes[12];
        c13 = bytes[13];
        c14 = bytes[14];
        c15 = bytes[15];
        c16 = bytes[16];
        c17 = bytes[17];
        c18 = bytes[18];
        c19 = bytes[19];
        c20 = bytes[20];
        c21 = bytes[21];
        c22 = bytes[22];
        c23 = bytes[23];
      }
    }
  }
}
