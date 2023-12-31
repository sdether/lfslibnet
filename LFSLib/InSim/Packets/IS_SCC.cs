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
  // IN GAME camera control
  // ----------------------

  // You can set the viewed car and selected camera directly with a special packet
  // These are the states normally set in-game by using the TAB and V keys

  /// <summary>
  /// Set Car Camera - Simplified camera packet (not SHIFT+U mode)
  /// </summary>
  [StructLayout(LayoutKind.Sequential)]
  struct IS_SCC : IClientLfsInSimPacket
  {
    public byte Size;		// 8
    public Enums.ISP Type;		// ISP_SCC
    public byte ReqI;		// 0
    public byte Zero;

    public byte ViewPLID;	// UniqueID of player to view
    public Enums.View InGameCam;	// InGameCam (as reported in StatePack)
    public byte Sp2;
    public byte Sp3;

    public IS_SCC(byte playerId, Enums.View camera)
    {
      Size = 8;
      Type = Enums.ISP.SCC;
      ReqI = 0;
      Zero = 0;
      ViewPLID = playerId;
      InGameCam = camera;
      Sp2 = 0;
      Sp3 = 0;
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
      return PacketFactory.GetBytesSequentially(this, Size);
    }

    public byte RequestId
    {
      get { return ReqI; }
    }

    #endregion
  }
}
