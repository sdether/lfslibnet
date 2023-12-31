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
  // VOTE NOTIFY AND CANCEL
  // ======================

  // LFS notifies the external program of any votes to restart or qualify

  /// <summary>
  /// VoTe Notify
  /// </summary>
  [StructLayout(LayoutKind.Sequential)]
  struct IS_VTN : ILfsInSimPacket
  {
    public byte Size;		// 8
    public Enums.ISP Type;		// ISP_VTN
    public byte ReqI;		// 0
    public byte Zero;

    public byte UCID;		// connection's unique id
    public Enums.Vote Action;		// VOTE_X (Vote Action as defined above)
    public byte Spare2;
    public byte Spare3;

    #region ILfsInSimPacket Members

    public Enums.ISP PacketType
    {
      get { return Type; }
    }

    #endregion
  }

  // When a vote is cancelled, LFS sends this IS_TINY

  // ReqI : 0
  // SubT : TINY_VTC		(VoTe Cancelled)

  // When a vote is completed, LFS sends this IS_SMALL

  // ReqI : 0
  // SubT : SMALL_VTA  	(VoTe Action)
  // UVal : action 		(VOTE_X - Vote Action as defined above)

  // You can instruct LFS host to cancel a vote using an IS_TINY

  // ReqI : 0
  // SubT : TINY_VTC		(VoTe Cancel)}
}
