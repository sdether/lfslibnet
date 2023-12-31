using System;
using FullMotion.LiveForSpeed.OutGauge;
using FullMotion.LiveForSpeed.OutSim;
namespace FullMotion.LiveForSpeed.InSim
{
  interface IInSimReader : ILfsReader
  {
    event InSimPacketReceivedEventHandler InSimPacketReceived;
    event OutSimPacketReceivedEventHandler OutSimPacketReceived;
    event OutGaugePacketReceivedEventHandler OutGaugePacketReceived;
  }
}
