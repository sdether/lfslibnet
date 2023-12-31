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

namespace FullMotion.LiveForSpeed.InSim.Packets.Support
{
  class StateModHelper
  {
    public static StateModHelper FollowCar
    {
      get { return new StateModHelper(Flags.ISS.SHIFTU_FOLLOW); }
    }

    public static StateModHelper ShiftUButtonsHidden
    {
      get { return new StateModHelper(Flags.ISS.SHIFTU_NO_OPT); }
    }

    public static StateModHelper Show2D
    {
      get { return new StateModHelper(Flags.ISS.SHOW_2D); }
    }

    public static StateModHelper MultiplayerSpeedup
    {
      get { return new StateModHelper(Flags.ISS.MPSPEEDUP); }
    }

    public static StateModHelper Mute
    {
      get { return new StateModHelper(Flags.ISS.SOUND_MUTE); }
    }

    public Flags.ISS flag;

    private StateModHelper(Flags.ISS flag)
    {
      this.flag = flag;
    }

    public Flags.ISS Flag { get { return flag; } }
  }
}
