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
using FullMotion.LiveForSpeed.Util;

namespace FullMotion.LiveForSpeed.OutGauge.Events
{
  /// <summary>
  /// Every OutGauge packet received by the handler generates a new Gauge object describing the current state
  /// </summary>
  public class Gauge
  {
		#region Static Members ########################################################################
    private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    #endregion

		#region Member Variables ######################################################################
    private OutGaugePack packet;
		#endregion

		#region Constructors ##########################################################################
    internal Gauge(OutGaugePack packet)
		{
      this.packet = packet;
    }
		#endregion

		#region Properties ############################################################################
    /// <summary>
    /// Time in milliseconds
    /// </summary>
    public uint Time
    {
      get { return packet.Time; }
    }
    /// <summary>
    /// Short Car name
    /// </summary>
    public string Car
    {
      get { return CharHelper.GetString(packet.Car); }
    }
    /// <summary>
    /// Shift Light On
    /// </summary>
    public bool ShiftLight 
    {
			get { return (packet.Flags&OutGaugeFlags.ShiftLight)==OutGaugeFlags.ShiftLight; }
    }
    /// <summary>
    /// Full Beam On
    /// </summary>
    public bool FullBeam 
    {
			get { return (packet.Flags&OutGaugeFlags.FullBeam)==OutGaugeFlags.FullBeam; }
    }
    /// <summary>
    /// Handbrake On
    /// </summary>
    public bool Handbrake 
    {
			get { return (packet.Flags&OutGaugeFlags.Handbrake)==OutGaugeFlags.Handbrake; }
    }
    /// <summary>
    /// Traction Control Activated
    /// </summary>
    public bool PitSpeedLimiter 
    {
			get { return (packet.Flags&OutGaugeFlags.PitSpeedLimiter)==OutGaugeFlags.PitSpeedLimiter; }
    }
    /// <summary>
    /// 
    /// </summary>
    public bool TractionControl 
    {
			get { return (packet.Flags&OutGaugeFlags.TractionControl)==OutGaugeFlags.TractionControl; }
    }
    /// <summary>
    /// Head Lights On
    /// </summary>
    public bool HeadLights 
    {
			get { return (packet.Flags&OutGaugeFlags.HeadLights)==OutGaugeFlags.HeadLights; }
    }
    /// <summary>
    /// Left Signal On
    /// </summary>
    public bool SignalLeft 
    {
			get { return (packet.Flags&OutGaugeFlags.SignalLeft)==OutGaugeFlags.SignalLeft; }
    }
    /// <summary>
    /// Right Signal On
    /// </summary>
    public bool SignalRight 
    {
			get { return (packet.Flags&OutGaugeFlags.SignalRight)==OutGaugeFlags.SignalRight; }
    }
    /// <summary>
    /// Redline reached
    /// </summary>
    public bool Redline 
    {
			get { return (packet.Flags&OutGaugeFlags.Redline)==OutGaugeFlags.Redline; }
    }
    /// <summary>
    /// Oil Warning Light On
    /// </summary>
    public bool OilWarning 
    {
			get { return (packet.Flags&OutGaugeFlags.OilWarning)==OutGaugeFlags.OilWarning; }
    }
    /// <summary>
    /// Is Display preference set to Kilometers or Miles
    /// </summary>
    public bool IsKM 
    {
			get { return (packet.Flags&OutGaugeFlags.KM)==OutGaugeFlags.KM; }
    }
    /// <summary>
    /// Is Display preference set to  Bar or Psi
    /// </summary>
    public bool IsBar 
    {
			get { return (packet.Flags&OutGaugeFlags.Bar)==OutGaugeFlags.Bar; }
    }
    /// <summary>
    /// Currently selected Gear
    /// </summary>
    public Gear Gear
    {
      get { return packet.CurrentGear; }
    }
    /// <summary>
    /// Current Speed in meters/second
    /// </summary>
    public float Speed
    {
      get { return packet.Speed; }
    }
    /// <summary>
    /// RPM of Engine
    /// </summary>
    public float RPM
    {
      get { return packet.RPM; }
    }
    /// <summary>
    /// Turbo manifold pressure in BAR
    /// </summary>
    public float Turbo
    {
      get { return packet.Turbo; }
    }
    /// <summary>
    /// Engine Temperature
    /// </summary>
    public float EngTemp
    {
      get { return packet.EngTemp; }
    }
    /// <summary>
    /// Fuel on board
    /// </summary>
    public float Fuel
    {
      get { return packet.Fuel; }
    }
    /// <summary>
    /// Oil Pressure in BAR
    /// </summary>
    public float OilPress
    {
      get { return packet.OilPress; }
    }
    /// <summary>
    /// Throttle position form 0 to 1
    /// </summary>
    public float Throttle
    {
      get { return packet.Throttle; }
    }
    /// <summary>
    /// Brake pedal position from 0 to 1
    /// </summary>
    public float Brake
    {
      get { return packet.Brake; }
    }
    /// <summary>
    /// Clutch position from 0 to 1
    /// </summary>
    public float Clutch
    {
      get { return packet.Clutch; }
    }
    /// <summary>
    /// Display Message 1. Usually Fuel
    /// </summary>
    public string DisplayMessage1
    {
      get { return CharHelper.GetString(packet.Display1); }
    }
    /// <summary>
    /// Display Message 2. Usually Settings
    /// </summary>
    public string DisplayMessage2
    {
      get { return CharHelper.GetString(packet.Display2); }
    }
    /// <summary>
    /// OutGauge Id specified in the Live For Speed Configuration
    /// </summary>
    public int Id
    {
      get { return packet.Id; }
    }
    #endregion

		#region Methods ###############################################################################
		#endregion

		#region Private Methods #######################################################################
		#endregion
  }
}
