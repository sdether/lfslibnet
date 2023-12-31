using System;
using System.Collections.Generic;
using System.Text;
using FullMotion.LiveForSpeed.InSim.Packets;

namespace FullMotion.LiveForSpeed.OutGauge
{
  class OutGaugePacketEventArgs : EventArgs
  {
    PacketInfo.OutGaugePack packet;

    public OutGaugePacketEventArgs(PacketInfo.OutGaugePack packet)
    {
      this.packet = packet;
    }

    public PacketInfo.OutGaugePack Packet { get { return packet; } }
  }

  delegate void OutGaugePacketReceivedEventHandler(ILfsReader sender, OutGaugePacketEventArgs e);

}
