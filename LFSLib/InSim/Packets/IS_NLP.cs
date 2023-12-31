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
  /// Node and Lap Packet - variable size
  /// </summary>
  struct IS_NLP : ILfsInSimPacket
  {
    public byte Size;		// 4 + NumP * 6 (PLUS 2 if needed to make it a multiple of 4)
    public Enums.ISP Type;		// ISP_NLP
    public byte ReqI;		// 0 unless this is a reply to an TINY_NLP request
    public byte NumP;		// number of players in race

    public Support.NodeLap[] Info; //[32];	// node and lap of each player, 1 to 32 of these (NumP)

    public IS_NLP(byte[] bytes)
    {
      int position = 0;
      Size = bytes[position++];
      Type = (Enums.ISP)bytes[position++];
      ReqI = bytes[position++];
      NumP = bytes[position++];
      Info = new Support.NodeLap[NumP];
      for (int i = 0; i < NumP; i++)
      {
        byte[] nodeLapBytes = new byte[Support.NodeLap.SIZE];
        Array.Copy(bytes, position, nodeLapBytes, 0, Support.NodeLap.SIZE);
        position += Support.NodeLap.SIZE;
        Info[i] = new Support.NodeLap(nodeLapBytes);
      }
    }

    #region ILfsInSimPacket Members

    public Enums.ISP PacketType
    {
      get { return Type; }
    }

    #endregion
  }

  // If ISF_MCI flag is set, a set of IS_MCI packets is sent...
}
