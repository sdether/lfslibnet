using System;
using System.Collections.Generic;
using System.Text;
using FullMotion.LiveForSpeed.InSim.Packets;

namespace FullMotion.LiveForSpeed.OutSim
{
  class OutSimPacketEventArgs : EventArgs
  {
    PacketInfo.OutSimPack packet;

    public OutSimPacketEventArgs(PacketInfo.OutSimPack packet)
    {
      this.packet = packet;
    }

    public PacketInfo.OutSimPack Packet { get { return packet; } }
  }

  delegate void OutSimPacketReceivedEventHandler(ILfsReader sender, OutSimPacketEventArgs e);

}
