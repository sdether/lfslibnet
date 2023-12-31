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
  // When all objects are cleared from a layout, LFS sends this IS_TINY :

  // ReqI : 0
  // SubT : TINY_AXC		(AutoX Cleared)

  // You can request information about the current layout with this IS_TINY :

  // ReqI : non-zero		(returned in the reply)
  // SubT : TINY_AXI		(AutoX Info)

  // The information will be sent back in this packet (also sent when a layout is loaded) :

  struct IS_AXI : ILfsInSimPacket
  {
    public byte Size;       // 40
    public Enums.ISP Type;		// ISP_AXI
    public byte ReqI;		// 0 unless this is a reply to an TINY_AXI request
    public byte Zero;

    public byte AXStart;	// autocross start position
    public byte NumCP;		// number of checkpoints
    public ushort NumO;		// number of objects

    public byte[] LName;	// [32] //the name of the layout last loaded (if loaded locally)

    public IS_AXI(byte[] bytes)
    {
      int position = 0;
      Size = bytes[position++];
      Type = (Enums.ISP)bytes[position++];
      ReqI = bytes[position++];
      position++;
      Zero = 0;
      AXStart = bytes[position++];
      NumCP = bytes[position++];
      NumO = (ushort)BitConverter.ToInt16(bytes, position);
      position += 2;
      LName = new byte[32];
      Array.Copy(bytes, position, LName, 0, 32);
    }

    #region ILfsInSimPacket Members

    public Enums.ISP PacketType
    {
      get { return Type; }
    }

    #endregion

  }
}
