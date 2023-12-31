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
  // VERSION REQUEST
  // ===============

  // It is advisable to request version information as soon as you have connected, to
  // avoid problems when connecting to a host with a later or earlier version.  You will
  // be sent a version packet on connection if you set ReqI in the IS_ISI packet.

  // This version packet can be sent on request :

  /// <summary>
  /// VERsion
  /// </summary>
  [StructLayout(LayoutKind.Sequential)]
  struct IS_VER : ILfsInSimPacket
  {
    public byte Size;			// 20
    public Enums.ISP Type;			// ISP_VERSION
    public byte ReqI;			// ReqI as received in the request packet
    public byte Zero;

    public Support.Char8 Version; //[8];		// LFS version, e.g. 0.3G
    public Support.Char6 Product; //[6];		// Product : DEMO or S1
    public ushort InSimVer;		// InSim Version : increased when InSim packets change

    #region ILfsInSimPacket Members

    public Enums.ISP PacketType
    {
      get { return Type; }
    }

    #endregion
  }

  // To request an InSimVersion packet at any time, send this IS_TINY :

  // ReqI : non-zero		(returned in the reply)
  // SubT : TINY_VER		(request an IS_VER)
}
