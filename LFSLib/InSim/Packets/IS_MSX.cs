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
  /// MSg eXtended - like MST but longer (not for commands)
  /// </summary>
  struct IS_MSX : IClientLfsInSimPacket
  {
    public byte Size;		// 100
    public Enums.ISP Type;		// ISP_MSX
    public byte ReqI;		// 0
    public byte Zero;

    public byte[] Msg; //[96];	// last byte must be zero

    public IS_MSX(byte[] bytes)
    {
      if (bytes.Length == 96)
      {
        // initializing with string
        Size = 100;
        Type = Enums.ISP.MSX;
        ReqI = 0;
        Zero = 0;
        Msg = bytes;
      }
      else
      {
        int position = 0;
        Size = bytes[position++];
        Type = (Enums.ISP)bytes[position++];
        ReqI = bytes[position++];
        position++;
        Zero = 0;
        Msg = new byte[96];
        Array.Copy(bytes, position, Msg, 0, 96);
      }
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
      byte[] bytes = new byte[Size];

      int position = 0;

      bytes[position++] = Size;
      bytes[position++] = (byte)Type;
      bytes[position++] = ReqI;
      bytes[position++] = Zero;

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
