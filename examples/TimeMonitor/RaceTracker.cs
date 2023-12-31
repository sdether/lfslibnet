/* ------------------------------------------------------------------------- *
 * Copyright (C) 2007 Arne Claassen
 *
 * Arne Claassen <lfslib [at] claassen [dot] net>
 *
 * This program is free software; you can redistribute it and/or modify
 * it as you want. There is no license attached whatsoever. Although
 * a mention in the credits would be appreciated.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. 
 * ------------------------------------------------------------------------- */
using System;
using System.Collections.Generic;
using System.Text;
using log4net;
using FullMotion.LiveForSpeed.InSim;
using FullMotion.LiveForSpeed.InSim.EventHandlers;
using FullMotion.LiveForSpeed.InSim.Events;
using FullMotion.LiveForSpeed.InSim.Enums;

namespace FullMotion.LiveForSpeed.TimeMonitor
{
  public class RaceTracker
  {
    #region log4net -------------------------------------------------------------------------------
    private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    #endregion

    class ButtonSet
    {
      public LfsButton Label;
      public LfsButton Time;
      public LfsButton Delta;
    }

    const byte LEFT = 0;
    const byte TOP = 40;
    const byte HEIGHT = 8;
    const byte WIDTH = 20;

    InSimHandler handler;

    Dictionary<byte, PlayerInfo> players = new Dictionary<byte, PlayerInfo>();

    byte buttonId = 0;

    LfsButton header;
    ButtonSet set1 = new ButtonSet();
    ButtonSet set2 = new ButtonSet();
    ButtonSet set3 = new ButtonSet();
    ButtonSet set4 = new ButtonSet();

    public RaceTracker(InSimHandler handler)
    {
      this.handler = handler;
      handler.RaceTrackPlayer
        += new RaceTrackPlayerHandler(handler_RaceTrackPlayer);
      handler.RaceTrackPlayerLeave
        += new RaceTrackPlayerLeaveHandler(handler_RaceTrackPlayerLeave);
      handler.RaceTrackPlayerSplitTime
        += new RaceTrackPlayerSplitTimeHandler(handler_RaceTrackPlayerSplitTime);
      handler.RaceTrackPlayerLap
        += new RaceTrackPlayerLapHandler(handler_RaceTrackPlayerLap);
      handler.LFSState += new StateEventHandler(handler_LFSState);
      handler.RaceTrackRaceStart += new RaceTrackRaceStartHandler(handler_RaceTrackRaceStart);

      // Request the current players on joining in case LFS is in race
      handler.RequestPlayerInfo();

      // set up our static buttons
      InitButtons();
    }

    /// <summary>
    /// Initialize all buttons to be used while running. We never move buttons, just change their state
    /// </summary>
    private void InitButtons()
    {
      byte top = TOP;
      header = CreateButton();
      header.Left = LEFT;
      header.Top = top;
      header.Width = (byte)(2 * WIDTH);
      header.TextAlignment = ButtonTextAlignment.Left;
      top += HEIGHT;

      // We have 4 sets of label,time,delta that we use as appropriate depending on splits
      // Each set is initialized to its fixed location and we just update their data and
      // show/hide them as needed
      InitSet(set1, top);
      top += (byte)(2 * HEIGHT);
      InitSet(set2, top);
      top += (byte)(2 * HEIGHT);
      InitSet(set3, top);
      top += (byte)(2 * HEIGHT);
      InitSet(set4, top);
    }

    /// <summary>
    /// Initialize a label,time,delta set based on a relative top
    /// </summary>
    /// <param name="set">The set to initialize</param>
    /// <param name="top">The relative top of the set</param>
    private void InitSet(ButtonSet set, byte top)
    {
      set.Label = CreateButton();
      set.Label.Left = LEFT;
      set.Label.Top = top;
      set.Label.TextAlignment = ButtonTextAlignment.Left;
      set.Time = CreateButton();
      set.Time.Left = (byte)(LEFT + WIDTH);
      set.Time.Top = top;
      set.Delta = CreateButton();
      set.Delta.Left = (byte)(LEFT + WIDTH);
      set.Delta.Top = (byte)(top + HEIGHT);
    }

    /// <summary>
    /// Helper for creating a button with the common settings.
    /// </summary>
    /// <returns></returns>
    private LfsButton CreateButton()
    {
      //we use buttonId to make sure we don't create colliding buttons
      LfsButton b = new LfsButton(buttonId++);
      b.TextColor = ButtonTextColor.SelectedText;
      b.Color = ButtonColor.Dark;
      b.TextAlignment = ButtonTextAlignment.Right;
      b.Height = HEIGHT;
      b.Width = WIDTH;
      return b;
    }

    /// <summary>
    /// Get a player by Id from our storage or return null if that player isn't in our storage
    /// </summary>
    /// <param name="playerId"></param>
    /// <returns></returns>
    private PlayerInfo GetPlayer(byte playerId)
    {
      if (!players.ContainsKey(playerId))
      {
        return null;
      }
      return players[playerId];
    }

    /// <summary>
    /// On Race Start, clear out all data and re-request the player roster
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void handler_RaceTrackRaceStart(InSimHandler sender, RaceTrackRaceStart e)
    {
      players.Clear();
      Clear();
      handler.RequestPlayerInfo();
    }

    /// <summary>
    /// Monitor the state of LFS to modify the HUD as needed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void handler_LFSState(InSimHandler sender, State e)
    {
      // If we're not in a game, clear out all data
      if (!e.IsInGame)
      {
        players.Clear();
        Clear();
      }
      else
      {
        // we're in a game
        if (!handler.LastLFSState.IsInGame)
        {
          // just entered into game, set everything up
          players.Clear();
          Clear();
          handler.RequestPlayerInfo();
        }
        else if (handler.LastLFSState.ViewedPlayerId != e.ViewedPlayerId)
        {
          // our viewed player changed, so we need to change the HUD to match
          PlayerInfo player = GetPlayer(e.ViewedPlayerId);
          if (player == null)
          {
            // there is no such player (this is a sanity catch in case we get this
            // event before LFS has given us the full roster)
            log.DebugFormat("no player {0} in roster", e.ViewedPlayerId);
            Clear();
            return;
          }
          DisplayTiming(player);
        }
      }
    }

    /// <summary>
    /// Information about a plyer in the race
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void handler_RaceTrackPlayer(InSimHandler sender, RaceTrackPlayer e)
    {
      // check if we already know about this player
      if (!players.ContainsKey(e.PlayerId))
      {
        PlayerInfo player = new PlayerInfo(e.Playername, e.PlayerId);
        players.Add(player.Id, player);
        if( handler.CurrentLFSState.ViewedPlayerId == player.Id )
        {
          DisplayTiming(player);
        }
      }
    }

    /// <summary>
    /// A player has left the server
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void handler_RaceTrackPlayerLeave(InSimHandler sender, RaceTrackPlayerLeave e)
    {
      // remove the player from the roster, in case someone else connects and the playerId
      // is recycled
      players.Remove(e.PlayerId);
    }

    /// <summary>
    /// A lap was completed by a player
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void handler_RaceTrackPlayerLap(InSimHandler sender, RaceTrackPlayerLap e)
    {
      PlayerInfo player = GetPlayer( e.PlayerId );
      if (player == null)
      {
        // there is no such player (this is a sanity catch in case we get this
        // event before LFS has given us the full roster)
        log.DebugFormat("no player {0} in roster", e.PlayerId);
        return;
      }
      if (!player.CurrentLap.Splits[0].HasValue)
      {
        // we only display information if we got all lap data from Split 1
        log.DebugFormat("partial lap for {0}, ignoring",e.PlayerId);
        return;
      }
      player.CurrentLap.LapTime = e.LapTime;
      if (player.Id == handler.CurrentLFSState.ViewedPlayerId)
      {
        // the lap data belongs to the player being viewed, so we update the HUD
        DisplayTiming(player);
      }
    }

    /// <summary>
    /// A split was reached by a player
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void handler_RaceTrackPlayerSplitTime(InSimHandler sender, RaceTrackPlayerSplitTime e)
    {
      PlayerInfo player = GetPlayer(e.PlayerId);
      if (player == null)
      {
        // there is no such player (this is a sanity catch in case we get this
        // event before LFS has given us the full roster)
        log.DebugFormat("no player {0} in roster", e.PlayerId);
        return;
      }
      if (e.Split > 1 && !player.CurrentLap.Splits[0].HasValue)
      {
        // we only display information if we got all lap data from Split 1
        log.DebugFormat("partial lap for {0}, ignoring", e.PlayerId);
        return;
      }
      else if (e.Split == 1)
      {
        // first split marks the beginning of the lap. if we did it at the end of the lap
        // we wouldn't display the past lap stats on a HUD redraw (i.e. when the viewed
        // player changes
        player.NewLap();
      }
      player.CurrentLap.Splits[e.Split - 1] = e.SplitTime;
      if (player.Id == handler.CurrentLFSState.ViewedPlayerId)
      {
        // the split data belongs to the player being viewed, so we update the HUD
        DisplayTiming(player);
      }
    }

    /// <summary>
    /// Remove all HUD elements
    /// </summary>
    public void Clear()
    {
      handler.DeleteButton(header.ButtonId, true);
      HideSet(set1);
      HideSet(set2);
      HideSet(set3);
      HideSet(set4);
    }

    /// <summary>
    /// Show the HUD for the current player
    /// </summary>
    /// <param name="player"></param>
    private void DisplayTiming(PlayerInfo player)
    {
      // we always display the header
      header.Text = player.Name;
      handler.SetButton(header, true);
      
      if (!player.CurrentLap.Splits[0].HasValue)
      {
        // if we have no split 0, we have nothing else, clean up and be done
        log.Debug("not enough data to display anything");
        HideSet(set1);
        HideSet(set2);
        HideSet(set3);
        HideSet(set4);
        return;
      }

      // show the first split.. this one is always in set1
      DisplaySet(set1, "Split 1", player.CurrentLap.Splits[0].Value, player.PreviousLap.Splits[0]);

      if (player.CurrentLap.LapTime.HasValue)
      {
        // we have a full lap. determine how many splits we have to figure out in which
        // set the lap gets displayed and hide the remaining sets, just in case
        log.Debug("displaying full lap");
        ButtonSet[] hideSet = null;
        ButtonSet lapSet = null;
        if (!player.CurrentLap.Splits[1].HasValue)
        {
          lapSet = set2;
          hideSet = new ButtonSet[] { set3, set4 };
        }
        else if (!player.CurrentLap.Splits[2].HasValue)
        {
          lapSet = set3;
          hideSet = new ButtonSet[] { set4 };
        }
        else
        {
          lapSet = set4;
          hideSet = new ButtonSet[0];
        }
        DisplaySet(lapSet, "Total", player.CurrentLap.LapTime.Value, player.PreviousLap.LapTime);
        foreach (ButtonSet set in hideSet)
        {
          HideSet(set);
        }
      }
      else if (player.CurrentLap.Splits[2].HasValue)
      {
        // showing split 1, 2 & 3
        log.Debug("displaying 3 splits");
        DisplaySet(set2, "Split 2", player.CurrentLap.Splits[1].Value, player.PreviousLap.Splits[1]);
        DisplaySet(set3, "Split 3", player.CurrentLap.Splits[2].Value, player.PreviousLap.Splits[2]);
        HideSet(set4);
      }
      else if (player.CurrentLap.Splits[1].HasValue)
      {
        // just showing split 1 & 2
        log.Debug("displaying 2 splits");
        DisplaySet(set2, "Split 2", player.CurrentLap.Splits[1].Value, player.PreviousLap.Splits[1]);
        HideSet(set3);
        HideSet(set4);
      }
      else
      {
        // showing just split 1
        log.Debug("displaying 1 split");
        HideSet(set2);
        HideSet(set3);
        HideSet(set4);
      }
    }

    /// <summary>
    /// Hide all the buttons in a set
    /// </summary>
    /// <param name="t2"></param>
    private void HideSet(ButtonSet t2)
    {
      handler.DeleteButton(t2.Label.ButtonId, true);
      handler.DeleteButton(t2.Time.ButtonId, true);
      handler.DeleteButton(t2.Delta.ButtonId, true);
    }

    /// <summary>
    /// Update and display a sset
    /// </summary>
    /// <param name="set"></param>
    /// <param name="labelString"></param>
    /// <param name="current"></param>
    /// <param name="last"></param>
    private void DisplaySet(ButtonSet set, string labelString, TimeSpan current, TimeSpan? last)
    {
      log.Debug("display set " + labelString);
      set.Label.Text = labelString;
      handler.SetButton(set.Label, true);
      set.Time.Text = FormatTime(current);
      handler.SetButton(set.Time, true);
      if (last.HasValue)
      {
        // we have timing for the last lap's instance, so we can determine the delta
        TimeSpan d = current - last.Value;
        if (d.TotalMilliseconds < 0)
        {
          set.Delta.Text = "-" + FormatTime(d.Negate());
          set.Delta.TextColor = ButtonTextColor.Ok;
        }
        else
        {
          set.Delta.Text = "+" + FormatTime(d);
          set.Delta.TextColor = ButtonTextColor.Cancel;
        }
      }
      else
      {
        // no previous data, so no delta
        set.Delta.Text = "-:--.--";
        set.Delta.TextColor = ButtonTextColor.Ok;
      }
      handler.SetButton(set.Delta, true);
    }

    /// <summary>
    /// Format a timespan into a race appropriate timing string
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    private string FormatTime(TimeSpan t)
    {
      int secFraction = t.Milliseconds / 10;
      return string.Format("{0}:{1:00}.{2:00}", t.Minutes, t.Seconds, secFraction);
    }

  }
}
