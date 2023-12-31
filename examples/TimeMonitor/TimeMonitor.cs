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
using FullMotion.LiveForSpeed.InSim;

namespace FullMotion.LiveForSpeed.TimeMonitor
{
  public partial class TimeMonitor : Form
  {
    InSimHandler handler;
    RaceTracker tracker;

    public TimeMonitor()
    {
      InitializeComponent();
      this.FormClosing += new FormClosingEventHandler(TimeMonitor_FormClosing);
    }

    void TimeMonitor_FormClosing(object sender, FormClosingEventArgs e)
    {
      // clean up our handler as needed
      if (handler != null)
      {
        handler.Close();
      }
    }

    private void actionButton_Click(object sender, EventArgs e)
    {
      if (handler != null)
      {
        // we have an existing handler, this click was a disconnect
        handler.Close();
        actionButton.Text = "Connect";
        textBoxPort.Enabled = true;
        textBoxHost.Enabled = true;
        tracker = null;
        handler = null;
      }
      else
      {
        // no existing handler, this click as a connect
        int port = 0;

        try
        {
          // make sure our port textbox has a valid data
          port = Convert.ToInt32(textBoxPort.Text);

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
              textBoxPort.Text,
              UInt16.MaxValue),
            "Invalid Port", MessageBoxButtons.OK);
          return;
        }

        try
        {
          // initialize our InSim handler
          handler = new InSimHandler();
          handler.Configuration.LFSHost = textBoxHost.Text;
          handler.Configuration.LFSHostPort = port;
          handler.Configuration.UseTCP = true;
          handler.Configuration.Local = true;
          handler.Configuration.ProgramName = "TimeMonitor";
          handler.Initialize();

          // initialize the tracker (it just runs itself once it has a handler)
          tracker = new RaceTracker(handler);
          actionButton.Text = "Disconnect";
          textBoxPort.Enabled = false;
          textBoxHost.Enabled = false;
        }
        catch (Exception ex)
        {
          handler = null;
          tracker = null;
          MessageBox.Show(this, "Unable to connect: " + ex.Message, "Connection Error");
          return;
        }
      }
    }

  }
}