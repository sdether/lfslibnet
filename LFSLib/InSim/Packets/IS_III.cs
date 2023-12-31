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
  /// <summary>
  /// InsIm Info - /i message from user to host's InSim
  /// </summary>
  struct IS_III : ILfsInSimPacket
  {
    public byte Size;		// 72
    public Enums.ISP Type;		// ISP_III
    public byte ReqI;		// 0
    public byte Zero;

    public byte UCID;		// connection's unique id (0 = host)
    public byte PLID;		// player's unique id (if zero, use UCID)
    public byte Sp2;
    public byte Sp3;

    public byte[] Msg; //[64];

    public IS_III(byte[] bytes)
    {
      int position = 0;
      Size = bytes[position++];
      Type = (Enums.ISP)bytes[position++];
      ReqI = bytes[position++];
      position++;
      Zero = 0;
      UCID = bytes[position++];
      PLID = bytes[position++];
      position++;
      Sp2 = 0;
      position++;
      Sp3 = 0;
      Msg = new byte[64];
      Array.Copy(bytes, position, Msg, 0, 64);
    }

    #region ILfsInSimPacket Members

    public Enums.ISP PacketType
    {
      get { return Type; }
    }

    #endregion
  }

}
