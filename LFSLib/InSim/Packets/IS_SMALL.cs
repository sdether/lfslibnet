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

  // IS_SMALL - used for various requests, replies and reports

  /// <summary>
  /// General purpose 8 byte packet
  /// </summary>
  [StructLayout(LayoutKind.Sequential)]
  struct IS_SMALL : IClientLfsInSimPacket
  {
    public byte Size;		// always 8
    public Enums.ISP Type;		// always ISP_SMALL
    public byte ReqI;		// 0 unless it is an info request or a reply to an info request
    public Enums.SMALL SubT;		// subtype, from SMALL_ enumeration (e.g. SMALL_SSP)

    public uint UVal;	// value (e.g. for SMALL_SSP this would be the OutSim packet rate)

    #region ILfsInSimPacket Members

    public Enums.ISP PacketType
    {
      get { return Type; }
    }

    public IS_SMALL(Enums.SMALL subType, byte requestId, uint value)
    {
      Size = 8;
      Type = Enums.ISP.SMALL;
      ReqI = requestId;
      SubT = subType;
      UVal = value;
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
