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
using System.Windows.Forms;

using log4net.Config;
using log4net.Appender;
using log4net.Layout;
using log4net.Filter;

namespace FullMotion.LiveForSpeed.TimeMonitor
{
  static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      // set up console logger for debugging.
      // currently filtering out FullMotion.LiveForSpeed.InSim.*
      // & FullMotion.LiveForSpeed.Util.*
      ConsoleAppender appender = new ConsoleAppender();
      LoggerMatchFilter filter = new LoggerMatchFilter();
      filter.LoggerToMatch = "FullMotion.LiveForSpeed.InSim";
      filter.AcceptOnMatch = false;
      appender.AddFilter(filter); filter = new LoggerMatchFilter();
      filter.LoggerToMatch = "FullMotion.LiveForSpeed.Util";
      filter.AcceptOnMatch = false;
      appender.AddFilter(filter);
      appender.Layout = new PatternLayout("%-6r %-25C:%L %m\n");
      BasicConfigurator.Configure(appender);

      // Fire up the UI
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new TimeMonitor());
    }
  }
}