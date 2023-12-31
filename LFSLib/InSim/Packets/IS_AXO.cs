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
  // On false start or wrong route / restricted area, an IS_PEN packet is sent :

  // False start : OldPen = 0 / NewPen = PENALTY_30 / Reason = PENR_FALSE_START
  // Wrong route : OldPen = 0 / NewPen = PENALTY_45 / Reason = PENR_WRONG_WAY

  // If an autocross object is hit (2 second time penalty) this packet is sent :

  /// <summary>
  /// Autocross object
  /// </summary>
  [StructLayout(LayoutKind.Sequential)]
  struct IS_AXO : ILfsInSimPacket
  {
    public byte Size;		// 4
    public Enums.ISP Type;		// ISP_AXO
    public byte ReqI;		// ReqI as received in the IS_BTN
    public byte PLID;		// player's unique id

    #region ILfsInSimPacket Members

    public Enums.ISP PacketType
    {
      get { return Type; }
    }

    #endregion
  }

}
