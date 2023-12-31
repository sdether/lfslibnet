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
  /// <summary>
  /// FINished race notification (not a final result - use IS_RES)
  /// </summary>
  [StructLayout(LayoutKind.Sequential)]
  struct IS_FIN : ILfsInSimPacket
  {
    public byte Size;		// 20
    public Enums.ISP Type;		// ISP_FIN
    public byte ReqI;		// 0
    public byte PLID;		// player's unique id (0 = player left before result was sent)

    public uint TTime;	// race time (ms)
    public uint BTime;	// best lap (ms)

    public byte SpA;
    public byte NumStops;	// number of pit stops
    public Flags.CONF Confirm;	// confirmation flags : disqualified etc - see below
    public byte SpB;

    public ushort LapsDone;	// laps completed
    public Flags.PIF Flags;		// player flags : help settings etc - see below

    #region ILfsInSimPacket Members

    public Enums.ISP PacketType
    {
      get { return Type; }
    }

    #endregion
  }

}
