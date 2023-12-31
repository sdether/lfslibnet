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

namespace FullMotion.LiveForSpeed.InSim.Flags
{
  [Flags()]
  enum ISB : byte
  {
    // BStyle byte : style flags for the button

    //C1 = 1,		// you can choose a standard
    //C2 = 2,		// interface colour using
    //C4 = 4,		// these 3 lowest bits - see below
    COLOR_OPP = 7,
    CLICK = 8,		// click this button to send IS_BTC
    LIGHT = 16,		// light button
    DARK = 32,		// dark button
    LEFT = 64,		// align text to left
    RIGHT = 128,	// align text to right
  }
  // colour 0 : light grey		(not user editable)
  // colour 1 : title colour		(default:yellow)
  // colour 2 : unselected text	(default:black)
  // colour 3 : selected text		(default:white)
  // colour 4 : ok				(default:green)
  // colour 5 : cancel			(default:red)
  // colour 6 : text string		(default:pale blue)
  // colour 7 : unavailable		(default:grey)
}
