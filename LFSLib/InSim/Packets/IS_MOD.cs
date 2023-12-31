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
  // SCREEN MODE
  // ===========

  // You can send this packet to LFS to set the screen mode :

  /// <summary>
  /// MODe : send to LFS to change screen mode
  /// </summary>
  [StructLayout(LayoutKind.Sequential)]
  struct IS_MOD : IClientLfsInSimPacket
  {
    public byte Size;		// 20
    public Enums.ISP Type;		// ISP_MOD
    public byte ReqI;		// 0
    public byte Zero;

    public int Bits16;		// set to choose 16-bit
    public int RR;			// refresh rate - zero for default
    public int Width;		// 0 means go to window
    public int Height;		// 0 means go to window

    public IS_MOD(int width, int height, int refreshRate, bool screenDepth16)
    {
      Size = 20;
      Type = Enums.ISP.MOD;
      ReqI = 0;
      Zero = 0;
      Bits16 = (screenDepth16) ? 1 : 0;
      RR = refreshRate;
      Width = width;
      Height = height;
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

  // The refresh rate actually selected by LFS will be the highest available rate
  // that is less than or equal to the specified refresh rate.  Refresh rate can
  // be specified as zero in which case the default refresh rate will be used.

  // If Width and Height are both zero, LFS will switch to windowed mode.


}
