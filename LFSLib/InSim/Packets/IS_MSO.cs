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
  // TEXT MESSAGES AND KEY PRESSES
  // ==============================

  // You can send 64-byte text messages to LFS as if the user had typed them in.
  // Messages that appear on LFS screen (up to 128 bytes) are reported to the
  // external program.  You can also send simulated keypresses to LFS.

  // MESSAGES OUT (FROM LFS)
  // ------------

  /// <summary>
  /// MSg Out - system messages and user messages 
  /// </summary>
  struct IS_MSO : ILfsInSimPacket
  {
    public byte Size;		// 136
    public Enums.ISP Type;		// ISP_MSO
    public byte ReqI;		// 0
    public byte Zero;

    public byte UCID;		// connection's unique id (0 = host)
    public byte PLID;		// player's unique id (if zero, use UCID)
    public Enums.UserType UserType;	// set if typed by a user (see User Values below) 
    public byte TextStart;	// first character of the actual text (after player name)

    public byte[] Msg; //[128];

    public IS_MSO(byte[] bytes)
    {
      int position = 0;
      Size = bytes[position++];
      Type = (Enums.ISP)bytes[position++];
      ReqI = bytes[position++];
      position++;
      Zero = 0;
      UCID = bytes[position++];
      PLID = bytes[position++];
      UserType = (Enums.UserType)bytes[position++];
      TextStart = bytes[position++];
      Msg = new byte[128];
      Array.Copy(bytes, position, Msg, 0, 128);
    }

    #region ILfsInSimPacket Members

    public Enums.ISP PacketType
    {
      get { return Type; }
    }

    #endregion
  }
}
