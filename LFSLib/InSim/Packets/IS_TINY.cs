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
  // GENERAL PURPOSE PACKETS - IS_TINY (4 bytes) and IS_SMALL (8 bytes)
  // =======================

  // To avoid defining several packet structures that are exactly the same, and to avoid
  // wasting the ISP_ enumeration, IS_TINY is used at various times when no additional data
  // other than SubT is required.  IS_SMALL is used when an additional integer is needed.

  // IS_TINY - used for various requests, replies and reports

  /// <summary>
  /// General purpose 4 byte packet
  /// </summary>
  [StructLayout(LayoutKind.Sequential)]
  struct IS_TINY : IClientLfsInSimPacket
  {
    public byte Size;		// always 4
    public Enums.ISP Type;		// always ISP_TINY
    public byte ReqI;		// 0 unless it is an info request or a reply to an info request
    public Enums.TINY SubT;		// subtype, from TINY_ enumeration (e.g. TINY_RACE_END)

    #region ILfsInSimPacket Members

    public Enums.ISP PacketType
    {
      get { return Type; }
    }

    public IS_TINY(Enums.TINY subType, byte requestId)
    {
      Size = 4;
      Type = Enums.ISP.TINY;
      ReqI = requestId;
      SubT = subType;
    }

    #endregion

    #region IClientLfsInSimPacket Members

    public byte[] GetBytes()
    {
      return PacketFactory.GetBytesSequentially(this, Size);
    }

    public byte RequestId
    {
      get { return ReqI; }
    }

    #endregion
  }

}
