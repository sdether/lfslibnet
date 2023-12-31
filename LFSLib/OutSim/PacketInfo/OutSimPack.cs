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
using System.Text;
using System.Runtime.InteropServices;

namespace FullMotion.LiveForSpeed.OutSim.PacketInfo
{
  [StructLayout(LayoutKind.Sequential)]
  internal struct OutSimPack
  {
    public uint Time;           // time in milliseconds (to check order)
    public float AngularVelocityX;
    public float AngularVelocityY;
    public float AngularVelocityZ;
    public float Heading;
    public float Pitch;
    public float Roll;
    public float AccelerationX;
    public float AccelerationY;
    public float AccelerationZ;
    public float VelocityX;
    public float VelocityY;
    public float VelocityZ;
    public int PositionX;
    public int PositionY;
    public int PositionZ;
    public int Id;             // (optional ID - if specified in cfg.txt)

    public OutSimPack(byte[] raw)
    {
      Int32 size = raw.Length;
      IntPtr pnt = Marshal.AllocHGlobal(size);
      try
      {
        GCHandle pin = GCHandle.Alloc(pnt, GCHandleType.Pinned);
        try
        {
          Marshal.Copy(raw, 0, pnt, size);
          this = (OutSimPack)Marshal.PtrToStructure(pnt, typeof(OutSimPack));
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
