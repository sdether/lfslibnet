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
  [StructLayout(LayoutKind.Sequential)]
  struct CompCar // Car info in 28 bytes - there is an array of these in the MCI (below)
  {
    public const int SIZE = 28;

    public ushort Node;		// current path node
    public ushort Lap;		// current lap
    public byte PLID;		// player's unique id
    public byte Position;	// current race position : 0 = unknown, 1 = leader, etc...
    public Flags.CCI Info;		// flags and other info - see below
    public byte Sp3;
    public int X;			// X map (65536 = 1 metre)
    public int Y;			// Y map (65536 = 1 metre)
    public int Z;			// Z alt (65536 = 1 metre)
    public ushort Speed;		// speed (32768 = 100 m/s)
    public ushort Direction;	// direction of car's motion : 0 = world y direction, 32768 = 180 deg
    public ushort Heading;	// direction of forward axis : 0 = world y direction, 32768 = 180 deg
    public short AngVel;		// signed, rate of change of heading : (16384 = 360 deg/s)

    public CompCar(byte[] bytes)
    {
      Int32 size = bytes.Length;
      IntPtr pnt = Marshal.AllocHGlobal(size);
      try
      {
        GCHandle pin = GCHandle.Alloc(pnt, GCHandleType.Pinned);
        try
        {
          Marshal.Copy(bytes, 0, pnt, size);
          this = (CompCar)Marshal.PtrToStructure(pnt, typeof(CompCar));
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

  // NOTE 1) Info byte - the bits in this byte have the following meanings :
  // NOTE 2) Heading : 0 = world y axis direction, 32768 = 180 degrees, anticlockwise from above
  // NOTE 3) AngVel  : 0 = no change in heading,    8192 = 180 degrees per second anticlockwise
}
