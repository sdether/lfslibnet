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
  // DIRECT camera control
  // ---------------------

  // A Camera Position Packet can be used for LFS to report a camera position and state.
  // An InSim program can also send one to set LFS camera position in game or SHIFT+U mode.

  // Type : "Vec" : 3 ints (X, Y, Z) - 65536 means 1 metre

  /// <summary>
  /// Cam Pos Pack - Full camera packet (in car OR SHIFT+U mode)
  /// </summary>
  [StructLayout(LayoutKind.Sequential)]
  struct IS_CPP : IClientLfsInSimPacket
  {
    public byte Size;		// 32
    public Enums.ISP Type;		// ISP_CPP
    public byte ReqI;		// instruction : 0 / or reply : ReqI as received in the TINY_SCP
    public byte Zero;

    public Support.Vec Pos;		// Position vector

    public ushort H;			// heading - 0 points along Y axis
    public ushort P;			// pitch   - 0 means looking at horizon
    public ushort R;			// roll    - 0 means no roll

    public byte ViewPLID;	// Unique ID of viewed player (0 = none)
    public Enums.View InGameCam;	// InGameCam (as reported in StatePack)

    public float FOV;		// 4-byte float : FOV in radians

    public ushort Time;		// Time to get there (0 means instant + reset)
    public Flags.ISS StateFlags;		// ISS state flags (see below)

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

  // The ISS state flags that can be set are :

  // ISS_SHIFTU			- in SHIFT+U mode
  // ISS_SHIFTU_HIGH		- HIGH view
  // ISS_SHIFTU_FOLLOW	- following car
  // ISS_VIEW_OVERRIDE	- override user view

  // On receiving this packet, LFS will set up the camera to match the values in the packet,
  // including switching into or out of SHIFT+U mode depending on the ISS_SHIFTU flag.

  // If ISS_SHIFTU is not set, then ViewPLID and InGameCam will be used.

  // If ISS_VIEW_OVERRIDE is set, the in-car view Heading Pitch and Roll will be taken
  // from the values in this packet.  Otherwise normal in-game control will be used.

  // Position vector (Vec Pos) - in SHIFT+U mode, Pos can be either relative or absolute.

  // If ISS_SHIFTU_FOLLOW is set, it's a following camera, so the position is relative to
  // the selected car.  Otherwise, the position is absolute, as used in normal SHIFT+U mode.

  // NOTE : Set InGameCam or ViewPLID to 255 to leave that option unchanged.}
}
