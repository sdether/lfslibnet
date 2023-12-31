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
  // Setting states

  // These states can be set by a special packet :

  // ISS_SHIFTU_FOLLOW	- following car
  // ISS_SHIFTU_NO_OPT	- SHIFT+U buttons hidden
  // ISS_SHOW_2D			- showing 2d display
  // ISS_MPSPEEDUP		- multiplayer speedup option
  // ISS_SOUND_MUTE		- sound is switched off

  /// <summary>
  /// State Flags Pack
  /// </summary>
  [StructLayout(LayoutKind.Sequential)]
  struct IS_SFP : IClientLfsInSimPacket
  {
    public byte Size;		// 8
    public Enums.ISP Type;		// ISP_SFP
    public byte ReqI;		// 0
    public byte Zero;

    public Flags.ISS Flag;		// the state to set
    public byte OffOn;		// 0 = off / 1 = on
    public byte Sp3;		// spare

    public IS_SFP(Flags.ISS flag, bool on)
    {
      Size = 8;
      Type = Enums.ISP.SFP;
      ReqI = 0;
      Zero = 0;
      Flag = flag;
      OffOn = (byte)((on) ? 1 : 0);
      Sp3 = 0;
    }

    #region ILfsInSimPacket Members

    public Enums.ISP PacketType
    {
      get { return Type; }
    }

    #endregion

    #region IClientLfsInSimPacket Members

    public byte[] GetBytes()
    {
      return PacketFactory.GetBytesSequentially(this, Size);
    }

    public byte RequestId
    {
      get { return ReqI; }
    }

    #endregion
  }

  // Other states must be set by using keypresses or messages (see below)


}
