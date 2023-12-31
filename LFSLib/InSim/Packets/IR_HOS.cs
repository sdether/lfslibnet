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
  struct IR_HOS : ILfsInSimPacket
  {
	public byte Size;		// 4 + NumHosts * 40
	public Enums.ISP Type;		// IRP_HOS
	public byte ReqI;		// 0
	public byte NumHosts;	// Number of hosts described in this packet

	public Support.HInfo[]	Info; //[6];	// Host info for every host in the Relay. 1 to 6 of these in a IR_HOS

    public IR_HOS(byte[] bytes)
    {
      int position = 0;
      Size = bytes[position++];
      Type = (Enums.ISP)bytes[position++];
      ReqI = bytes[position++];
      NumHosts = bytes[position++];
      Info = new Support.HInfo[NumHosts];
      for (int i = 0; i < NumHosts; i++)
      {
        byte[] hostInfoBytes = new byte[Support.HInfo.SIZE];
        Array.Copy(bytes, position, hostInfoBytes, 0, Support.HInfo.SIZE);
        position += Support.HInfo.SIZE;
        Info[i] = new Support.HInfo(hostInfoBytes);
      }
    }

    #region ILfsInSimPacket Members

    public Enums.ISP PacketType
    {
      get { return Type; }
    }

    #endregion
  }
}
