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

  // In LFS there is a list of connections AND a list of players in the race
  // Some packets are related to connections, some players, some both

  // If you are making a multiplayer InSim program, you must maintain two lists
  // You should use the unique identifier UCID to identify a connection

  // Each player has a unique identifier PLID from the moment he joins the race, until he
  // leaves.  It's not possible for PLID and UCID to be the same thing, for two reasons :

  // 1) there may be more than one player per connection if AI drivers are used
  // 2) a player can swap between connections, in the case of a driver swap (IS_TOC)

  // When all players are cleared from race (e.g. /clear) LFS sends this IS_TINY

  // ReqI : 0
  // SubT : TINY_CLR		(CLear Race)

  // When a race ends (return to game setup screen) LFS sends this IS_TINY

  // ReqI : 0
  // SubT : TINY_REN  	(Race ENd)

  // You can instruct LFS host to cancel a vote using an IS_TINY

  // ReqI : 0
  // SubT : TINY_VTC		(VoTe Cancel)

  /// <summary>
  /// Race STart
  /// </summary>
  [StructLayout(LayoutKind.Sequential)]
  struct IS_RST : ILfsInSimPacket
  {
    public byte Size;		// 28
    public Enums.ISP Type;		// ISP_RST
    public byte ReqI;		// 0 unless this is a reply to an TINY_RST request
    public byte Zero;

    public byte RaceLaps;	// 0 if qualifying
    public byte QualMins;	// 0 if race
    public byte NumP;		// number of players in race
    public byte Spare;

    public Support.Char6 Track; //[6];	// short track name
    public Enums.Weather Weather;
    public Enums.Wind Wind;

    public Flags.RaceFlags Flags;		// race flags (must pit, can reset, etc - see below)
    public ushort NumNodes;	// total number of nodes in the path
    public ushort Finish;		// node index - finish line
    public ushort Split1;		// node index - split 1
    public ushort Split2;		// node index - split 2
    public ushort Split3;		// node index - split 3

    #region ILfsInSimPacket Members

    public Enums.ISP PacketType
    {
      get { return Type; }
    }

    #endregion
  }

  // To request an IS_RST packet at any time, send this IS_TINY :

  // ReqI : non-zero		(returned in the reply)
  // SubT : TINY_RST		(request an IS_RST)
}
