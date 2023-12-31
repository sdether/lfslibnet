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
  /// Helper class without code, just data for Lap data
  /// </summary>
  class LapInfo
  {
    public TimeSpan? LapTime;
    public TimeSpan?[] Splits = new TimeSpan?[3];
  }
}
