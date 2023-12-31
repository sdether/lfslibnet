using System;
using System.Net;

namespace FullMotion.LiveForSpeed.OutGauge
{
  interface IOutGaugeReader : ILfsReader
  {
    event OutGaugePacketReceivedEventHandler OutGaugePacketReceived;
    IPAddress RemoteIP { get; }
    IPAddress[] LocalIPs { get; }
  }
}
