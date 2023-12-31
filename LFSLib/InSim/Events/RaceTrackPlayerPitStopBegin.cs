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

namespace FullMotion.LiveForSpeed.InSim.Events
{
	/// <summary>
	/// Event describing a player starting a pit stop
	/// </summary>
  public class RaceTrackPlayerPitStopBegin : RaceTrackEvent
	{
		#region Static Members ########################################################################
    private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    #endregion

		#region Member Variables ######################################################################
		private Packets.IS_PIT packet;
    Tyres tyres;
    #endregion

		#region Constructors ##########################################################################
    internal RaceTrackPlayerPitStopBegin(Packets.IS_PIT packet)
		{
      this.packet = packet;

      log.Debug("RaceTrackPlayerPitStopBegin event created");
      this.tyres = new Tyres(packet.Tyres);
    }
		#endregion

		#region Properties ############################################################################
		/// <summary>
		/// The player's unique Id
		/// </summary>
		public byte PlayerId
		{
			get { return packet.PLID; }
		}

    /// <summary>
    /// Number of laps completed
    /// </summary>
    public ushort LapsDone
    {
      get { return packet.LapsDone; }
    }

    /// <summary>
    /// Is the player a left hand driver
    /// </summary>
    public bool SwapSide
    {
      get { return (packet.Flags & Flags.PIF.SWAPSIDE) == Flags.PIF.SWAPSIDE; }
    }
    /// <summary>
    /// Is gear change throttle cut enabled
    /// </summary>
    public bool GearChangeCut
    {
      get { return (packet.Flags & Flags.PIF.GC_CUT) == Flags.PIF.GC_CUT; }
    }
    /// <summary>
    /// Is gear change throttle blip enabled
    /// </summary>
    public bool GearChangeBlip
    {
      get { return (packet.Flags & Flags.PIF.GC_BLIP) == Flags.PIF.GC_BLIP; }
    }
    /// <summary>
    /// Is the player using automatic gear changes
    /// </summary>
    public bool AutoGears
    {
      get { return (packet.Flags & Flags.PIF.AUTOGEARS) == Flags.PIF.AUTOGEARS; }
    }
    /// <summary>
    /// Is brake help turned on
    /// </summary>
    public bool HelpBrake
    {
      get { return (packet.Flags & Flags.PIF.HELP_B) == Flags.PIF.HELP_B; }
    }
    /// <summary>
    /// Is the clutch set to automatic
    /// </summary>
    public bool Autoclutch
    {
      get { return (packet.Flags & Flags.PIF.AUTOCLUTCH) == Flags.PIF.AUTOCLUTCH; }
    }
    /// <summary>
    /// Is the player using a mouse
    /// </summary>
    public bool Mouse
    {
      get { return (packet.Flags & Flags.PIF.MOUSE) == Flags.PIF.MOUSE; }
    }
    /// <summary>
    /// Is the player using a keyboard without aids
    /// </summary>
    public bool KeyboardNoHelp
    {
      get { return (packet.Flags & Flags.PIF.KB_NO_HELP) == Flags.PIF.KB_NO_HELP; }
    }
    /// <summary>
    /// Is the player using a stabilized keyboard
    /// </summary>
    public bool KeyboardStabilized
    {
      get { return (packet.Flags & Flags.PIF.KB_STABILISED) == Flags.PIF.KB_STABILISED; }
    }
    /// <summary>
    /// Is the player using a custom view
    /// </summary>
    public bool CustomView
    {
      get { return (packet.Flags & Flags.PIF.CUSTOM_VIEW) == Flags.PIF.CUSTOM_VIEW; }
    }
    /// <summary>
    /// Is the player in the Pits
    /// </summary>
    public bool InPits
    {
      get { return (packet.Flags & Flags.PIF.INPITS) == Flags.PIF.INPITS; }
    }
    /// <summary>
    /// Is the player using a Shifter
    /// </summary>
    public bool Shifter
    {
      get { return (packet.Flags & Flags.PIF.SHIFTER) == Flags.PIF.SHIFTER; }
    }

    /// <summary>
    /// Current Penalty assessed for Player
    /// </summary>
    public Enums.Penalty Penalty
    {
      get { return packet.Penalty; }
    }

    /// <summary>
    /// Number of Pit Stops completed during race
    /// </summary>
    public int NumberOfPitStops
    {
      get { return packet.NumStops; }
    }

    /// <summary>
    /// Tyre compounds for all wheels
    /// </summary>
    public Tyres Tyres
    {
      get { return tyres; }
    }

    /// <summary>
    /// Pit Stop requires Major Body Work
    /// </summary>
    public bool MajorBodyWork
    {
      get { return Flags.PSE.BODY_MAJOR == (Flags.PSE.BODY_MAJOR & packet.Work); }
    }

    /// <summary>
    /// Pit Stop requires Minor Body Work
    /// </summary>
    public bool MinorBodyWork
    {
      get { return Flags.PSE.BODY_MINOR == (Flags.PSE.BODY_MINOR & packet.Work); }
    }

    /// <summary>
    /// Front is damaged
    /// </summary>
    public bool FrontDamaged
    {
      get { return Flags.PSE.FR_DAM == (Flags.PSE.FR_DAM & packet.Work); }
    }

    /// <summary>
    /// Front Wheels need change
    /// </summary>
    public bool FrontWheelsChange
    {
      get { return Flags.PSE.FR_WHL == (Flags.PSE.FR_WHL & packet.Work); }
    }

    /// <summary>
    /// Left Front is damaged
    /// </summary>
    public bool LeftFrontDamage
    {
      get { return Flags.PSE.LE_FR_DAM == (Flags.PSE.LE_FR_DAM & packet.Work); }
    }

    /// <summary>
    /// Left Front Wheel needs change
    /// </summary>
    public bool LeftFrontWheelChange
    {
      get { return Flags.PSE.LE_FR_WHL == (Flags.PSE.LE_FR_WHL & packet.Work); }
    }

    /// <summary>
    /// Left Rear is damaged
    /// </summary>
    public bool LeftRearDamage
    {
      get { return Flags.PSE.LE_RE_DAM == (Flags.PSE.LE_RE_DAM & packet.Work); }
    }

    /// <summary>
    /// Left Rear Wheel needs change
    /// </summary>
    public bool LeftRearWheelChange
    {
      get { return Flags.PSE.LE_RE_WHL == (Flags.PSE.LE_RE_WHL & packet.Work); }
    }

    /// <summary>
    /// Rear is damaged
    /// </summary>
    public bool RearDamage
    {
      get { return Flags.PSE.RE_DAM == (Flags.PSE.RE_DAM & packet.Work); }
    }

    /// <summary>
    /// Rear Wheels needs change
    /// </summary>
    public bool RearWheelsChange
    {
      get { return Flags.PSE.RE_WHL == (Flags.PSE.RE_WHL & packet.Work); }
    }

    /// <summary>
    /// Refueling required
    /// </summary>
    public bool Refuel
    {
      get { return Flags.PSE.REFUEL == (Flags.PSE.REFUEL & packet.Work); }
    }

    /// <summary>
    /// Right Front is damaged
    /// </summary>
    public bool RightFrontDamage
    {
      get { return Flags.PSE.RI_FR_DAM == (Flags.PSE.RI_FR_DAM & packet.Work); }
    }

    /// <summary>
    /// Right Front Wheel needs change
    /// </summary>
    public bool RightFrontWheelChange
    {
      get { return Flags.PSE.RI_FR_WHL == (Flags.PSE.RI_FR_WHL & packet.Work); }
    }

    /// <summary>
    /// Right Rear is damaged
    /// </summary>
    public bool RightRearDamage
    {
      get { return Flags.PSE.RI_RE_DAM == (Flags.PSE.RI_RE_DAM & packet.Work); }
    }

    /// <summary>
    /// Right Rear Wheel needs change
    /// </summary>
    public bool RightRearWheelChange
    {
      get { return Flags.PSE.RI_RE_WHL == (Flags.PSE.RI_RE_WHL & packet.Work); }
    }

    /// <summary>
    /// Setup will be changed
    /// </summary>
    public bool SetupChange
    {
      get { return Flags.PSE.SETUP == (Flags.PSE.SETUP & packet.Work); }
    }

    /// <summary>
    /// This pit stop is a required stop
    /// </summary>
    public bool Stop
    {
      get { return Flags.PSE.STOP == (Flags.PSE.STOP & packet.Work); }
    }
    #endregion

		#region Methods ###############################################################################
		#endregion

		#region Private Methods #######################################################################
		#endregion
	}
}
