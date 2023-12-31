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
  // BUTTONS
  // =======

  // You can make up to 240 buttons appear on the host or guests (ID = 0 to 239).
  // You should set the ISF_LOCAL flag (in IS_ISI) if your program is not a host control
  // system, to make sure your buttons do not conflict with any buttons sent by the host.

  // LFS can display normal buttons in these four screens :

  // - main entry screen
  // - game setup screen
  // - in game
  // - SHIFT+U mode

  // To delete one button or clear all buttons, send this packet :

  /// <summary>
  /// Button FunctioN - delete buttons / receive button requests
  /// </summary>
  [StructLayout(LayoutKind.Sequential)]
  struct IS_BFN : IClientLfsInSimPacket
  {
    public byte Size;		// 8
    public Enums.ISP Type;		// ISP_BFN
    public byte ReqI;		// 0
    public Enums.BFN SubT;		// subtype, from BFN_ enumeration (see below)

    public byte UCID;		// connection to send to or from (0 = local / 255 = all)
    public byte ClickID;	// ID of button to delete (if SubT is BFN_DEL_BTN)
    public byte Inst;		// used internally by InSim
    public byte Sp3;

    public IS_BFN(Enums.BFN type, byte ucid, byte buttonId)
    {
      Size = 8;
      Type = Enums.ISP.BFN;
      ReqI = 0;
      SubT = type;

      UCID = ucid;
      ClickID = buttonId;
      Inst = 0;
      Sp3 = 0;
    }

    #region ILfsInSimPacket Members

    public Enums.ISP PacketType
    {
      get { return Type; }
    }

    #endregion

    #region IClientLfsInSimPacket Members

    public byte RequestId
    {
      get { return ReqI; }
    }

    public byte[] GetBytes()
    {
      return PacketFactory.GetBytesSequentially(this, Size);
    }

    #endregion
  }
}
