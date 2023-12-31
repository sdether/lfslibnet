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
using log4net;
using FullMotion.LiveForSpeed.OutGauge.PacketInfo;

namespace FullMotion.LiveForSpeed.OutGauge.Messages
{
  internal class GaugeMessage
  {
		#region Static Members ########################################################################
    private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    #endregion

		#region Member Variables ######################################################################
		private OutGaugePack packet = new OutGaugePack();
		#endregion

		#region Constructors ##########################################################################
    internal GaugeMessage(byte[] message)
		{
      int position = 0;
      int size = 4;
      packet.Time = BitConverter.ToUInt32(message,position);
      position += size;
      packet.Car = new byte[size];
      Array.Copy(message, position, packet.Car, 0, size);
      position += size;
      packet.Flags = (OutGaugeFlags)BitConverter.ToInt16(message,position);
			position += 2;
      packet.CurrentGear = (Gear)message[position++];
      packet.SpareB = message[position++];
      packet.Speed = BitConverter.ToSingle(message,position);
      position += size;
      packet.RPM = BitConverter.ToSingle(message,position);
      position += size;
      packet.Turbo = BitConverter.ToSingle(message,position);
      position += size;
      packet.EngTemp = BitConverter.ToSingle(message,position);
      position += size;
      packet.Fuel = BitConverter.ToSingle(message,position);
      position += size;
      packet.OilPress = BitConverter.ToSingle(message,position);
      position += size;
      packet.Spare1 = BitConverter.ToSingle(message,position);
      position += size;
      packet.Spare2 = BitConverter.ToSingle(message,position);
      position += size;
      packet.Spare3 = BitConverter.ToSingle(message,position);
      position += size;
      packet.Throttle = BitConverter.ToSingle(message,position);
      position += size;
      packet.Brake = BitConverter.ToSingle(message,position);
      position += size;
      packet.Clutch = BitConverter.ToSingle(message,position);
      position += size;
      size = 16;
      packet.Display1 = new byte[size];
      Array.Copy(message, position, packet.Display1, 0, size);
      position += size;
      packet.Display2 = new byte[size];
      Array.Copy(message, position, packet.Display2, 0, size);
      position += size;
      packet.Id = BitConverter.ToInt32(message,position);
    }
		#endregion

		#region Properties ############################################################################
    public OutGaugePack Packet { get { return packet; } }
    #endregion

		#region Methods ###############################################################################
		#endregion

		#region Private Methods #######################################################################
		#endregion
  }
}
