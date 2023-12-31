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

namespace FullMotion.LiveForSpeed.InSim.Packets
{
  /// <summary>
  /// Single CHaracter - send to simulate single character
  /// </summary>
  [StructLayout(LayoutKind.Sequential)]
  struct IS_SCH : IClientLfsInSimPacket
  {
    public byte Size;		// 8
    public Enums.ISP Type;		// ISP_SCH
    public byte ReqI;		// 0
    public byte Zero;

    public byte CharB;		// key to press
    public Flags.KeyModifier KeyModifiers;		// bit 0 : SHIFT / bit 1 : CTRL
    public byte Spare2;
    public byte Spare3;

    public IS_SCH(KeyPress keyPress)
    {
      Size = 8;
      Type = Enums.ISP.SCH;
      ReqI = 0;
      Zero = 0;
      CharB = (byte)keyPress.Key;
      KeyModifiers = 0;
      KeyModifiers = (keyPress.CtrlOn) ? KeyModifiers | Flags.KeyModifier.Ctrl : KeyModifiers & ~Flags.KeyModifier.Ctrl;
      KeyModifiers = (keyPress.ShiftOn) ? KeyModifiers | Flags.KeyModifier.Shift : KeyModifiers & ~Flags.KeyModifier.Shift;
      Spare2 = 0;
      Spare3 = 0;
    }

    #region ILfsInSimPacket Members

    public Enums.ISP PacketType
    {
      get { return Type; }
    }

    #endregion

    #region IClientLfsInSimPacket Members

    public byte[] GetBytes()
    {
      return PacketFactory.GetBytesSequentially(this, Size);
    }

    public byte RequestId
    {
      get { return ReqI; }
    }

    #endregion
  }
}
