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
using FullMotion.LiveForSpeed.Util;

namespace FullMotion.LiveForSpeed.InSim.Packets
{
  /// <summary>
  /// Msg To Connection - hosts only - send to a connection or a player
  /// </summary>
  struct IS_MTC : IClientLfsInSimPacket
  {
    public byte Size;		// 72
    public Enums.ISP Type;		// ISP_MTC
    public byte ReqI;		// 0
    public byte Zero;

    public byte UCID;		// connection's unique id (0 = host)
    public byte PLID;		// player's unique id (if zero, use UCID)
    public byte Sp2;
    public byte Sp3;

    public byte[] Msg; //[64];	// last byte must be zero

    public IS_MTC(byte[] bytes)
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

    public IS_MTC(byte recipient, string message, bool player)
    {
      Size = 72;
      Type = Enums.ISP.MTC;
      ReqI = 0;
      Zero = 0;
      UCID = (player) ? (byte)0 : recipient;
      PLID = (player) ? recipient : (byte)0;
      Sp2 = 0;
      Sp3 = 0;
      Msg = CharHelper.GetBytes(message, 64, true);
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
      byte[] bytes = new byte[72];

      int position = 0;

      bytes[position++] = Size;
      bytes[position++] = (byte)Type;
      bytes[position++] = ReqI;
      bytes[position++] = Zero;
      bytes[position++] = UCID;
      bytes[position++] = PLID;
      bytes[position++] = Sp2;
      bytes[position++] = Sp3;

      Array.Copy(this.Msg, 0, bytes, position, this.Msg.Length);

      return bytes;
    }

    public byte RequestId
    {
      get { return ReqI; }
    }

    #endregion
  }
}
