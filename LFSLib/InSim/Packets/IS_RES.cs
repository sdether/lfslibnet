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
  /// RESult (qualify or confirmed finish)
  /// </summary>
  [StructLayout(LayoutKind.Sequential)]
  struct IS_RES : ILfsInSimPacket
  {
    public byte Size;		// 84
    public Enums.ISP Type;		// ISP_RES
    public byte ReqI;		// 0 unless this is a reply to a TINY_RES request
    public byte PLID;		// player's unique id (0 = player left before result was sent)

    public Support.Char24 UName; //[24];	// username
    public Support.Char24 PName; //[24];	// nickname
    public Support.Char8 Plate; //[8];	// number plate - NO ZERO AT END!
    public Support.Char4 CName; //[4];	// skin prefix

    public uint TTime;	// race time (ms)
    public uint BTime;	// best lap (ms)

    public byte SpA;
    public byte NumStops;	// number of pit stops
    public Flags.CONF Confirm;	// confirmation flags : disqualified etc - see below
    public byte SpB;

    public ushort LapsDone;	// laps completed
    public Flags.PIF Flags;		// player flags : help settings etc - see below

    public byte ResultNum;	// finish or qualify pos (0 = win / 255 = not added to table)
    public byte NumRes;		// total number of results (qualify doesn't always add a new one)
    public ushort PSeconds;	// penalty time in seconds (already included in race time)

    #region ILfsInSimPacket Members

    public Enums.ISP PacketType
    {
      get { return Type; }
    }

    #endregion
  }
}
