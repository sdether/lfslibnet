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
  // MULTIPLAYER NOTIFICATION
  // ========================

  // LFS will send this packet when a host is started or joined :

  /// <summary>
  /// InSim Multi
  /// </summary>
  struct IS_ISM : ILfsInSimPacket
  {
    public byte Size;		// 40
    public Enums.ISP Type;		// ISP_ISM
    public byte ReqI;		// usually 0 / or if a reply : ReqI as received in the TINY_ISM
    public byte Zero;

    public Enums.ServerType Host;		// 0 = guest / 1 = host
    public byte Sp1;
    public byte Sp2;
    public byte Sp3;

    public byte[] HName; //[32];	// the name of the host joined or started

    public IS_ISM(byte[] bytes)
    {
      int position = 0;
      Size = bytes[position++];
      Type = (Enums.ISP)bytes[position++];
      ReqI = bytes[position++];
      position++;
      Zero = 0;
      Host = (Enums.ServerType)bytes[position++];
      position++;
      Sp1 = 0;
      position++;
      Sp2 = 0;
      position++;
      Sp3 = 0;
      HName = new byte[32];
      Array.Copy(bytes, position, HName, 0, 32);
    }

    #region ILfsInSimPacket Members

    public Enums.ISP PacketType
    {
      get { return Type; }
    }

    #endregion
  }

  // On ending or leaving a host, LFS will send this IS_TINY :

  // ReqI : 0
  // SubT : TINY_MPE		(MultiPlayerEnd)

  // To request an IS_ISM packet at any time, send this IS_TINY :

  // ReqI : non-zero		(returned in the reply)
  // SubT : TINY_ISM		(request an IS_ISM)

  // NOTE : If LFS is not in multiplayer mode, the host name in the ISM will be empty.
}
