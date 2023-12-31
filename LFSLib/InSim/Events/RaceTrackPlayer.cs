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
  /// Event describing a player, either sent on join, leaving the pits or by manually requesting a player event
  /// </summary>
  public class RaceTrackPlayer : RaceTrackEvent
  {
    #region Static Members ########################################################################
    private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    #endregion

    #region Member Variables ######################################################################
    private Packets.IS_NPL packet;
    string playerName;
    string plate;
    string carName;
    string skinName;
    Tyres tyres;
    Passengers passengers;
    #endregion

    #region Constructors ##########################################################################
    internal RaceTrackPlayer(Packets.IS_NPL packet)
    {
      this.packet = packet;

      log.Debug("RaceTrackPlayer event created");
      this.tyres = new Tyres(packet.Tyres);
      this.passengers = new Passengers(packet.Pass);
    }
    #endregion

    #region Properties ############################################################################
    /// <summary>
    /// Id of the request that caused this event to be triggered. 0 if the event trigger was not
    /// manually requested
    /// </summary>
    public byte RequestId
    {
      get { return packet.ReqI; }
    }
    /// <summary>
    /// DEPRECATED: Name of the Player
    /// </summary>
    [Obsolete("Replaced with PlayerName in 0.18+")]
    public string Playername
    {
      get { return PlayerName; }
    }
    /// <summary>
    /// Name of the Player
    /// </summary>
    public string PlayerName
    {
      get
      {
        if (string.IsNullOrEmpty(playerName))
        {
          playerName = packet.PName.Value;
        }
        return playerName;
      }
    }
    /// <summary>
    /// License Plate used by the player's car
    /// </summary>
    public string Plate
    {
      get
      {
        if (string.IsNullOrEmpty(plate))
        {
          plate = packet.Plate.Value;
        }
        return plate;
      }
    }
    /// <summary>
    /// DEPRECATED: Name of the car
    /// </summary>
    [Obsolete("Replaced with CarName in 0.18+")]
    public string Carname
    {
      get { return CarName; }
    }
    /// <summary>
    /// Name of the car
    /// </summary>
    public string CarName
    {
      get
      {
        if (string.IsNullOrEmpty(carName))
        {
          carName = packet.CName.Value;
        }
        return carName;
      }
    }
    /// <summary>
    /// DEPRECATED: Name of the car skin
    /// </summary>
    [Obsolete("Replaced with SkinName in 0.18+")]
    public string Skinname
    {
      get
      {
        if (string.IsNullOrEmpty(skinName))
        {
          skinName = packet.SName.Value;
        }
        return skinName;
      }
    }
    /// <summary>
    /// Name of the car skin
    /// </summary>
    public string SkinName
    {
      get
      {
        if (string.IsNullOrEmpty(skinName))
        {
          skinName = packet.SName.Value;
        }
        return skinName;
      }
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
    /// Is the driver male
    /// </summary>
    public bool Male
    {
      get { return !((packet.PType & Flags.PlayerType.Female) == Flags.PlayerType.Female); }
    }
    /// <summary>
    /// Is the driver an AI
    /// </summary>
    public bool AI
    {
      get { return (packet.PType & Flags.PlayerType.AI) == Flags.PlayerType.AI; }
    }
    /// <summary>
    /// Is the driver a remote driver
    /// </summary>
    public bool Remote
    {
      get { return (packet.PType & Flags.PlayerType.Remote) == Flags.PlayerType.Remote; }
    }
    /// <summary>
    /// The connections's unique Id
    /// </summary>
    public byte ConnectionId
    {
      get { return packet.UCID; }
    }
    /// <summary>
    /// The player's unique Id
    /// </summary>
    public byte PlayerId
    {
      get { return packet.PLID; }
    }
    /// <summary>
    /// Number of Player on the server
    /// </summary>
    public byte PlayerNumber
    {
      get { return packet.NumP; }
    }

    /// <summary>
    /// Passenger load out for the car
    /// </summary>
    public Passengers Passengers
    {
      get { return passengers; }
    }

    /// <summary>
    /// The current tire compound selection
    /// </summary>
    public Tyres Tyres
    {
      get { return tyres; }
    }

    /// <summary>
    /// Voluntary Mass handicap in Kg
    /// </summary>
    public byte HandicapMass
    {
      get { return packet.H_Mass; }
    }

    /// <summary>
    /// Voluntary Intake restriction handicap in restriction percentage (0 - 1)
    /// </summary>
    public decimal HandicapIntakeRestriction
    {
      get { return (decimal)packet.H_TRes/100; }
    }

    /// <summary>
    /// The id of the driver model in use
    /// </summary>
    public byte ModelId
    {
      get { return packet.Model; }
    }
    #endregion

    #region Methods ###############################################################################
    #endregion

    #region Private Methods #######################################################################
    #endregion
  }
}

