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

namespace FullMotion.LiveForSpeed.OutGauge.PacketInfo
{
	[Flags()] internal enum OutGaugeFlags : ushort
	{
    ShiftLight = 1,
    FullBeam = 2,
    Handbrake = 4,
    PitSpeedLimiter = 8,
    TractionControl = 16,
    HeadLights = 32,
    SignalLeft = 64,
    SignalRight = 128,
    Redline = 256,
    OilWarning = 512,
    Spare1 = 1024,
    Spare2 = 2048,
    Spare3 = 4096,
    Spare4 = 8192,
    KM = 16384,
    Bar = 32768,
  }

  /// <summary>
  /// Enumeration of the gears sent by Live For Speed
  /// </summary>
  public enum Gear : byte
  {
    /// <summary>Reverse</summary>
    Reverse = 0,
    /// <summary>No gear selected</summary>
    Neutral = 1,
    /// <summary>First gear</summary>
    First = 2,
    /// <summary>Second gear</summary>
    Second = 3,
    /// <summary>Third gear</summary>
    Third = 4,
    /// <summary>Fourth gear</summary>
    Fourth = 5,
    /// <summary>Fifth gear</summary>
    Fifth = 6,
    /// <summary>Sixth gear</summary>
    Sixth = 7,
    /// <summary>Seventh gear</summary>
    Seventh = 8
  }

  internal struct OutGaugePack
  {
    public uint Time;           // time in milliseconds (to check order)
    public byte[] Car; //[4];         // Car name
    public OutGaugeFlags Flags;          // Combination of OG_FLAGS, see below
    public Gear CurrentGear;           // Reverse:0, Neutral:1, First:2...
    public byte SpareB;
    public float Speed;          // M/S
    public float RPM;            // RPM
    public float Turbo;          // BAR
    public float EngTemp;        // C
    public float Fuel;           // 0 to 1
    public float OilPress;       // BAR
    public float Spare1;
    public float Spare2;
    public float Spare3;
    public float Throttle;       // 0 to 1
    public float Brake;          // 0 to 1
    public float Clutch;         // 0 to 1
    public byte[] Display1; //[16];   // Usually Fuel
    public byte[] Display2; //[16];   // Usually Settings
    public int Id;             // (optional ID - if specified in cfg.txt)

    public OutGaugePack(byte[] message)
    {
      int position = 0;
      int size = 4;
      this.Time = BitConverter.ToUInt32(message, position);
      position += size;
      this.Car = new byte[size];
      Array.Copy(message, position, this.Car, 0, size);
      position += size;
      this.Flags = (OutGaugeFlags)BitConverter.ToInt16(message, position);
      position += 2;
      this.CurrentGear = (Gear)message[position++];
      this.SpareB = message[position++];
      this.Speed = BitConverter.ToSingle(message, position);
      position += size;
      this.RPM = BitConverter.ToSingle(message, position);
      position += size;
      this.Turbo = BitConverter.ToSingle(message, position);
      position += size;
      this.EngTemp = BitConverter.ToSingle(message, position);
      position += size;
      this.Fuel = BitConverter.ToSingle(message, position);
      position += size;
      this.OilPress = BitConverter.ToSingle(message, position);
      position += size;
      this.Spare1 = BitConverter.ToSingle(message, position);
      position += size;
      this.Spare2 = BitConverter.ToSingle(message, position);
      position += size;
      this.Spare3 = BitConverter.ToSingle(message, position);
      position += size;
      this.Throttle = BitConverter.ToSingle(message, position);
      position += size;
      this.Brake = BitConverter.ToSingle(message, position);
      position += size;
      this.Clutch = BitConverter.ToSingle(message, position);
      position += size;
      size = 16;
      this.Display1 = new byte[size];
      Array.Copy(message, position, this.Display1, 0, size);
      position += size;
      this.Display2 = new byte[size];
      Array.Copy(message, position, this.Display2, 0, size);
      position += size;
      this.Id = BitConverter.ToInt32(message, position);
    }
  }
}
