using System;
using System.Net;

namespace FullMotion.LiveForSpeed.OutSim
{
  interface IOutSimReader : ILfsReader
  {
    event OutSimPacketReceivedEventHandler OutSimPacketReceived;
    IPAddress RemoteIP { get; }
    IPAddress[] LocalIPs { get; }
  }
}
