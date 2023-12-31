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
  /// New PLayer joining race (if PLID already exists, then leaving pits)
  /// </summary>
  [StructLayout(LayoutKind.Sequential)]
  struct IS_NPL : ILfsInSimPacket
  {
    public byte Size;		// 76
    public Enums.ISP Type;		// ISP_NPL
    public byte ReqI;		// 0 unless this is a reply to an TINY_NPL request
    public byte PLID;		// player's newly assigned unique id

    public byte UCID;		// connection's unique id
    public Flags.PlayerType PType;		// bit 0 : female / bit 1 : AI / bit 2 : remote
    public Flags.PIF Flags;		// player flags

    public Support.Char24 PName; //[24];	// nickname
    public Support.Char8 Plate; //[8];	// number plate - NO ZERO AT END!

    public Support.Char4 CName; //[4];	// car name
    public Support.Char16 SName; //[16];	// skin name - MAX_CAR_TEX_NAME
    public Support.Tyres Tyres; //[4];	// compounds

    public byte H_Mass;		// added mass (kg)
    public byte H_TRes;		// intake restriction
    public byte Model;
    public Flags.Passengers Pass;		// passengers byte

    public int Spare;

    public byte Sp0;
    public byte NumP;		// number in race (same when leaving pits, 1 more if new)
    public byte Sp2;
    public byte Sp3;

    #region ILfsInSimPacket Members

    public Enums.ISP PacketType
    {
      get { return Type; }
    }

    #endregion
  }
}
