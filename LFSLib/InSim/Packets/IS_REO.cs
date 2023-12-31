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
  // IS_REO : REOrder - this packet can be sent in either direction

  // LFS sends one at the start of every race or qualifying session, listing the start order

  // You can send one to LFS before a race start, to specify the starting order.
  // It may be a good idea to avoid conflict by using /start=fixed (LFS setting).
  // Alternatively, you can leave the LFS setting, but make sure you send your IS_REO
  // AFTER you receive the IS_VTA.  LFS does its default grid reordering at the same time
  // as it sends the IS_VTA (VoTe Action) and you can override this by sending an IS_REO.

  /// <summary>
  /// REOrder (when race restarts after qualifying)
  /// </summary>
  struct IS_REO : IClientLfsInSimPacket
  {
    public byte Size;		// 36
    public Enums.ISP Type;		// ISP_REO
    public byte ReqI;		// 0 unless this is a reply to an TINY_REO request
    public byte NumP;		// number of players in race

    public byte[] PLID; //[32];	// all PLIDs in new order

    public IS_REO(byte[] bytes)
    {
      int position = 0;
      Size = bytes[position++];
      Type = (Enums.ISP)bytes[position++];
      ReqI = bytes[position++];
      NumP = bytes[position++];
      PLID = new byte[32];
      for (int i = 0; i < 32; i++)
      {
        PLID[i] = bytes[position++];
      }
    }

    public IS_REO(byte[] players, byte requestId)
    {
      Size = 36;
      Type = Enums.ISP.REO;
      ReqI = requestId;
      NumP = (byte)players.Length;
      PLID = new byte[32];
      for (int i = 0; i < NumP; i++)
      {
        PLID[i] = players[i];
      }
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
      byte[] bytes = new byte[Size];

      int position = 0;

      bytes[position++] = Size;
      bytes[position++] = (byte)Type;
      bytes[position++] = ReqI;
      bytes[position++] = NumP;

      Array.Copy(this.PLID, 0, bytes, position, this.PLID.Length);

      return bytes;
    }

    #endregion
  }

  // To request an IS_REO packet at any time, send this IS_TINY :

  // ReqI : non-zero		(returned in the reply)
  // SubT : TINY_REO		(request an IS_REO)}
}
