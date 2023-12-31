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

namespace FullMotion.LiveForSpeed.InSim.Packets.Support
{
  // CAR TRACKING PACKETS - car position info sent at constant intervals
  // ====================

  // IS_NLP - compact, all cars in 1 variable sized packet
  // IS_MCI - detailed, max 8 cars per variable sized packet

  // To receive IS_NLP or IS_MCI packets at a specified interval :

  // 1) Set the Interval field in the IS_ISI (InSimInit) packet
  // 2) Set one of the flags ISF_NLP or ISF_MCI in the IS_ISI packet

  // If ISF_NLP flag is set, one IS_NLP packet is sent...

  [StructLayout(LayoutKind.Sequential)]
  struct NodeLap // Car info in 6 bytes - there is an array of these in the NLP (below)
  {
    public const int SIZE = 6;

    public ushort Node;		// current path node
    public ushort Lap;		// current lap
    public byte PLID;		// player's unique id
    public byte Position;	// current race position : 0 = unknown, 1 = leader, etc...

    public NodeLap(byte[] bytes)
    {
      Int32 size = bytes.Length;
      IntPtr pnt = Marshal.AllocHGlobal(size);
      try
      {
        GCHandle pin = GCHandle.Alloc(pnt, GCHandleType.Pinned);
        try
        {
          Marshal.Copy(bytes, 0, pnt, size);
          this = (NodeLap)Marshal.PtrToStructure(pnt, typeof(NodeLap));
        }
        finally
        {
          pin.Free();
        }
      }
      finally
      {
        Marshal.FreeHGlobal(pnt);
      }
    }
  }

}
