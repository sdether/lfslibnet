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
using FullMotion.LiveForSpeed.Util;

namespace FullMotion.LiveForSpeed.InSim.Events
{
  /// <summary>
  /// Event describing a player's option changing
  /// </summary>
  public class RaceTrackPlayerChange : RaceTrackEvent
  {
    #region Static Members ########################################################################
    private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    #endregion

    #region Member Variables ######################################################################
    private Packets.IS_PFL packet;
    #endregion

    #region Constructors ##########################################################################
    internal RaceTrackPlayerChange(Packets.IS_PFL packet)
    {
      this.packet = packet;

      log.Debug("RaceTrackPlayerChange event created");
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
    #endregion

    #region Methods ###############################################################################
    #endregion

    #region Private Methods #######################################################################
    #endregion
  }
}

