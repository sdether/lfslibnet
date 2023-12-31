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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using log4net;
using FullMotion.LiveForSpeed.InSim;
using FullMotion.LiveForSpeed.InSim.EventHandlers;
using FullMotion.LiveForSpeed.InSim.Events;

namespace StateInspector
{
  public partial class LfsStateInspector : Form
  {
    #region log4net -------------------------------------------------------------------------------
    private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    #endregion

    /// <summary>
    /// Lfs Handler
    /// </summary>
    InSimHandler handler;
    /// <summary>
    /// Background thread for automatically connecting and re-connecting to LFS
    /// </summary>
    Thread connectionManager;
    /// <summary>
    /// Run state (i.e. the background state should keep going
    /// </summary>
    bool run;
    /// <summary>
    /// Notification to the background thread that the settings have changed and a
    /// new connection is required
    /// </summary>
    bool settingsChanged = true;
    /// <summary>
    /// a lockable object to act as a semaphore for synchronizing threads
    /// </summary>
    object lockObject = new object();
    /// <summary>
    /// the LFS port divined from the port textbox
    /// </summary>
    int port = 30000;

    /// <summary>
    /// Set of colors for aging changed state variables
    /// </summary>
    Color[] aging = new Color[] 
      {
        Color.FromArgb(233,150,122),
        Color.FromArgb(234,178,153),
        Color.FromArgb(235,205,185),
      };

    public LfsStateInspector()
    {
      InitializeComponent();
      this.FormClosing += new FormClosingEventHandler(LfsStateInspector_FormClosing);
    }

    void LfsStateInspector_FormClosing(object sender, FormClosingEventArgs e)
    {
      run = false;
      try
      {
        handler.Close();
      }
      catch
      {
        // nothing can stop us now. We're exiting!
      }
    }

    private void LfsStateInspector_Load(object sender, EventArgs e)
    {
      // prep the background thread and fire it off
      connectionManager = new Thread(new ThreadStart(CheckConnection));
      connectionManager.IsBackground = true;
      connectionManager.Start();
      lfsPortBox.Text = port.ToString();
    }

    /// <summary>
    /// Background loop constantly trying to connect
    /// </summary>
    private void CheckConnection()
    {
      log.Debug("Starting Connection checker");
      run = true;
      while (run)
      {
        if (run && handler == null && settingsChanged)
        {
          // we need to see if can make a connection
          lock (lockObject)
          {
            try
            {
              log.Debug("trying to create handler");
              // initialize our InSim handler
              handler = new InSimHandler();
              handler.Configuration.LFSHost = lfsHostBox.Text;
              handler.Configuration.LFSHostPort = port;
              handler.Configuration.UseTCP = true;
              handler.Configuration.Local = true;
              handler.Configuration.ProgramName = "StateInSpector";
              handler.LFSState += new StateEventHandler(handler_LFSState);
              handler.StateChanged += new StateChangeHandler(handler_StateChanged);
              handler.Initialize();
              settingsChanged = false;
              log.Debug("handler created");
            }
            catch (Exception e)
            {
              log.Error("handler creation failed", e);
              handler = null;
              Thread.Sleep(2000);
              continue;
            }
          }
        }
        Thread.Sleep(5000);
      }
    }

    void handler_StateChanged(InSimHandler sender, FullMotion.LiveForSpeed.InSim.StateChangeEventArgs e)
    {
      // WinForms boilerplate for invoking UI actions on the UI thread
      if (InvokeRequired)
      {
        BeginInvoke(new StateChangeHandler(handler_StateChanged), sender, e);
        return;
      }

      // deal with Lfs handler connecting and disconnecting
      switch (e.CurrentState)
      {
        case InSimHandler.HandlerState.Connected:
          stateBox.Enabled = true;
          versionLabel.Text = handler.Version.Version;
          break;
        case InSimHandler.HandlerState.Closed:
          DisconnectHandler();
          break;
      }

    }

    void handler_LFSState(InSimHandler sender, State e)
    {
      // WinForms boilerplate for invoking UI actions on the UI thread
      if (InvokeRequired)
      {
        BeginInvoke(new StateEventHandler(handler_LFSState), sender, e);
        return;
      }

      string lapString = "";
      switch (e.RaceLaps.Type)
      {
        case LapInfoType.Hour:
          lapString = e.RaceLaps.Value + " hours";
          break;
        case LapInfoType.Lap:
          lapString = e.RaceLaps.Value + " laps";
          break;
        case LapInfoType.Practice:
          lapString = "Practice";
          break;
      }

      //textboxes
      Update(dateTime, e.DateTime.ToLongTimeString());
      Update(inGameCamera, e.InGameCamera.ToString());
      Update(numConnections, e.NumberOfConnections.ToString());
      Update(numPlayers, e.NumberOfPlayers.ToString());
      Update(playersFinished, e.PlayersFinished.ToString());
      Update(qualifyingMinutes, e.QualifyingMinutes.ToString());
      Update(raceLaps, lapString);
      Update(raceState, e.RaceState.ToString());
      Update(replaySpeed, e.ReplaySpeed.ToString());
      Update(shortTrackName, e.ShortTrackName.ToString());
      Update(viewedPlayerIndex, e.ViewedPlayerId.ToString());
      Update(weather, e.Weather.ToString());
      Update(wind, e.Wind.ToString());

      //checkboxes
      Update(buttonsAreHidden, e.ShiftUButtonsHidden);
      Update(inShiftUMode, e.InShiftUMode);
      Update(overidingUserView, e.OverridingUserView);
      Update(isFollowing, e.IsFollowing);
      Update(isHighView, e.IsHighView);
      Update(isIn2DMode, e.IsIn2DView);
      Update(isInFrontEndScreen, e.IsInFrontEndScreen);
      Update(isInGame, e.IsInGame);
      Update(isInMultiplayerMode, e.IsInMultiplayerMode);
      Update(multiplayerSpeedupOptionOn, e.MultiplayerSpeedupOptionOn);
      Update(isMuted, e.IsMuted);
      Update(isPaused, e.IsPaused);
      Update(isReplay, e.IsReplay);
      Update(isWindowed, e.IsWindowed);

    }

    /// <summary>
    /// Generic helper for updating and aging TextBoxes
    /// </summary>
    /// <param name="control"></param>
    /// <param name="currentState"></param>
    private void Update(TextBox control, string currentState)
    {
      AgeColor(control, control.Text != currentState);
      control.Text = currentState;
    }

    /// <summary>
    /// Generic helper for updating and aging CheckBoxes
    /// </summary>
    /// <param name="control"></param>
    /// <param name="currentState"></param>
    private void Update(CheckBox control, bool currentState)
    {
      AgeColor(control, control.Checked != currentState);
      control.Checked = currentState;
    }

    /// <summary>
    /// Helper for modify the color of a control depending on change or age or change
    /// </summary>
    /// <param name="control"></param>
    /// <param name="change"></param>
    private void AgeColor(Control control, bool change)
    {
      if (change)
      {
        control.BackColor = aging[0];
      }
      else if (control.BackColor == aging[0])
      {
        control.BackColor = aging[1];
      }
      else if (control.BackColor == aging[1])
      {
        control.BackColor = aging[2];
      }
      else
      {
        control.BackColor = SystemColors.Control;
      }
    }

    /// <summary>
    /// Notify the background thread of possibly changed connection settings
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void applyButton_Click(object sender, EventArgs e)
    {
      try
      {
        // make sure our port textbox has a valid data
        port = Convert.ToInt32(lfsPortBox.Text);

        if (port < 1 || port > UInt16.MaxValue)
        {
          throw new Exception();
        }
      }
      catch
      {
        MessageBox.Show(
          this,
          string.Format(
            "'{0}' is not a valid port number. Port must be between 1 and {1}",
            lfsPortBox.Text,
            UInt16.MaxValue),
          "Invalid Port", MessageBoxButtons.OK);
        return;
      }
      if (lfsHostBox.Text.Length == 0)
      {
        MessageBox.Show(
          this,
          string.Format(
            "You must specify a host (use 127.0.0.1 for a local connection)",
            lfsPortBox.Text,
            UInt16.MaxValue),
          "Invalid Host", MessageBoxButtons.OK);
        return;
      }

      log.Debug("applying settings");

      if (handler.Configuration.LFSHost != lfsHostBox.Text
        || handler.Configuration.LFSHostPort != port)
      {
        DisconnectHandler();
      }
    }

    /// <summary>
    /// Disconnect and clean-up our LFS handler
    /// </summary>
    private void DisconnectHandler()
    {
      lock (lockObject)
      {
        log.Debug("disconnecting handler");
        // race condition protection
        if (handler != null)
        {
          try
          {
            // handler can throw a NotConnected exception ...
            handler.Close();
          }
          catch
          {
            // ... but obviously we don't care
          }
        }
        handler = null;
        settingsChanged = true;
        stateBox.Enabled = false;
        versionLabel.Text = "...";
      }
    }
  }
}