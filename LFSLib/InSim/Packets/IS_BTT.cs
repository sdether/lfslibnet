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

namespace FullMotion.LiveForSpeed.InSim.Packets
{
  // If the TypeIn byte is set in IS_BTN the user can type text into the button
  // In that case no IS_BTC is sent - an IS_BTT is sent when the user presses ENTER

  /// <summary>
  /// BuTton Type - sent back when user types into a text entry button
  /// </summary>
  struct IS_BTT : ILfsInSimPacket
  {
    public byte Size;		// 104
    public Enums.ISP Type;		// ISP_BTT
    public byte ReqI;		// ReqI as received in the IS_BTN
    public byte UCID;		// connection that typed into the button (zero if local)

    public byte ClickID;	// button identifier originally sent in IS_BTN
    public byte Inst;		// used internally by InSim
    public byte TypeIn;		// from original button specification
    public byte Sp3;

    public byte[] Text; //[96];	// typed text, zero to TypeIn specified in IS_BTN

    public IS_BTT(byte[] bytes)
    {
      int position = 0;
      Size = bytes[position++];
      Type = (Enums.ISP)bytes[position++];
      ReqI = bytes[position++];
      UCID = bytes[position++];
      ClickID = bytes[position++];
      Inst = bytes[position++];
      TypeIn = bytes[position++];
      position++;
      Sp3 = 0;
      Text = new byte[96];
      Array.Copy(bytes, position, Text, 0, 96);
    }

    #region ILfsInSimPacket Members

    public Enums.ISP PacketType
    {
      get { return Type; }
    }

    #endregion
  }
}
