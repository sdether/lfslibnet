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
  // STATE REPORTING AND REQUESTS
  // ============================

  // LFS will send a StatePack any time the info in the StatePack changes.

  /// <summary>
  /// STAte
  /// </summary>
  [StructLayout(LayoutKind.Sequential)]
  struct IS_STA : ILfsInSimPacket
  {
    public byte Size;			// 28
    public Enums.ISP Type;			// ISP_STA
    public byte ReqI;			// ReqI if replying to a request packet
    public byte Zero;

    public float ReplaySpeed;	// 4-byte float - 1.0 is normal speed

    public Flags.ISS Flags;			// ISS state flags (see below)
    public Enums.View InGameCam;		// Which type of camera is selected (see below)
    public byte ViewPLID;		// Unique ID of viewed player (0 = none)

    public byte NumP;			// Number of players in race
    public byte NumConns;		// Number of connections including host
    public byte NumFinished;	// Number finished or qualified
    public Enums.RaceState RaceInProg;		// 0 - no race / 1 - race / 2 - qualifying

    public byte QualMins;
    public byte RaceLaps;		// see "RaceLaps" near the top of this document
    public byte Spare2;
    public byte Spare3;

    public Support.Char6 Track; //[6];		// short name for track e.g. FE2R
    public Enums.Weather Weather;		// 0,1,2...
    public Enums.Wind Wind;			// 0=off 1=weak 2=strong

    #region ILfsInSimPacket Members

    public Enums.ISP PacketType
    {
      get { return Type; }
    }

    #endregion
  }

  // InGameCam is the in-game selected camera mode (which is
  // still selected even if LFS is actually in SHIFT+U mode).
  // For InGameCam's values, see "View identifiers" below.

  // To request a StatePack at any time, send this IS_TINY :

  // ReqI : non-zero		(returned in the reply)
  // SubT : TINY_SST		(Send STate)
}
