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
using System.Text;

namespace FullMotion.LiveForSpeed.InSim.Enums
{
  enum TINY : byte  // the fourth byte of IS_TINY packets is one of these
  {
    NONE,		//  0					: see "maintaining the connection"
    VER,		//  1 - info request	: get version
    CLOSE,		//  2 - instruction		: close insim
    PING,		//  3 - ping request	: external progam requesting a reply
    REPLY,		//  4 - ping reply		: reply to a ping request
    VTC,		//  5 - info			: vote cancelled
    SCP,		//  6 - info request	: send camera pos
    SST,		//  7 - info request	: send state info
    GTH,		//  8 - info request	: get time in hundredths (i.e. SMALL_RTP)
    MPE,		//  9 - info			: multi player end
    ISM,		// 10 - info request	: get multiplayer info (i.e. ISP_ISM)
    REN,		// 11 - info			: race end (return to game setup screen)
    CLR,		// 12 - info			: all players cleared from race
    NCN,		// 13 - info			: get all connections
    NPL,		// 14 - info			: get all players
    RES,		// 15 - info			: get all results
    NLP,		// 16 - info request	: send an IS_NLP packet
    MCI,		// 17 - info request	: send an IS_MCI packet
    REO,		// 18 - info request	: send an IS_REO packet
    RST,		// 19 - info request	: send an IS_RST packet
    AXI,    // 20 - info request	: send an IS_AXI
    AXC,    // 21 - info			: autocross cleared
  }

}
