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
  // Pit Work Flags

  [Flags()]
  enum PSE : uint
  {
    NOTHING = 1,		// bit 0 (1)
    STOP = 2,			// bit 1 (2)
    FR_DAM = 4,			// bit 2 (4)
    FR_WHL = 8,			// etc...
    LE_FR_DAM = 16,
    LE_FR_WHL = 32,
    RI_FR_DAM = 64,
    RI_FR_WHL = 128,
    RE_DAM = 256,
    RE_WHL = 512,
    LE_RE_DAM = 1024,
    LE_RE_WHL = 2048,
    RI_RE_DAM = 4096,
    RI_RE_WHL = 8192,
    BODY_MINOR = 16384,
    BODY_MAJOR = 32768,
    SETUP = 65536,
    REFUEL = 131072,
  }
}
