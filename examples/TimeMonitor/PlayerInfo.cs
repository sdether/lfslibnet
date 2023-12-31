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

namespace FullMotion.LiveForSpeed.TimeMonitor
{
  /// <summary>
  /// Basic player information for race tracking
  /// </summary>
  class PlayerInfo
  {
    string name;
    byte playerId;
    LapInfo currentLap = new LapInfo();
    LapInfo previousLap = new LapInfo();

    /// <summary>
    /// Create a new player based on name and id
    /// </summary>
    /// <param name="name"></param>
    /// <param name="playerId"></param>
    public PlayerInfo(string name, byte playerId)
    {
      this.name = name;
      this.playerId = playerId;
    }

    /// <summary>
    /// The Player's Id
    /// </summary>
    public byte Id
    {
      get { return playerId; }
    }

    /// <summary>
    /// The Player's Name
    /// </summary>
    public string Name
    {
      get { return name; }
    }

    /// <summary>
    /// Data about the current lap
    /// </summary>
    public LapInfo CurrentLap
    {
      get { return currentLap; }
    }

    /// <summary>
    /// Data about the most recent lap
    /// </summary>
    public LapInfo PreviousLap
    {
      get { return previousLap; }
    }

    /// <summary>
    /// Start a new lap
    /// </summary>
    public void NewLap()
    {
      // rotate current into previous and put a clean current record in place
      previousLap = currentLap;
      currentLap = new LapInfo();
    }

  }
}
