using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FullMotion.LiveForSpeed.OutGauge;
using FullMotion.LiveForSpeed.OutGauge.Events;

namespace OutGaugeMonitor
{
  public partial class OutGaugeMonitor : Form
  {
    OutGaugeHandler outGaugeHandler;

    public OutGaugeMonitor()
    {
      InitializeComponent();
      this.FormClosing += new FormClosingEventHandler(OutGaugeMonitor_FormClosing);
    }

    void OutGaugeMonitor_FormClosing(object sender, FormClosingEventArgs e)
    {
      // clean-up before exit
      if (outGaugeHandler != null)
      {
        outGaugeHandler.Close();
      }
    }

    private void applyButton_Click(object sender, EventArgs e)
    {
      // make sure we got a valid port
      int port;
      
      try
      {
        port = Convert.ToInt32(outGaugePortBox.Text);

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
            outGaugePortBox.Text,
            UInt16.MaxValue),
          "Invalid Port",MessageBoxButtons.OK);
        return;
      }

      if (outGaugeHandler != null)
      {
        // unsubscribe from and close the existing outGaugeHandler
        outGaugeHandler.Updated -= new OutGaugeHandler.GaugeEvent(outGaugeHandler_Updated);
        outGaugeHandler.Close();
        outGaugeHandler = null;
      }

      outGaugeHandler = new OutGaugeHandler(port);
      outGaugeHandler.Initialize();
      outGaugeHandler.Updated += new OutGaugeHandler.GaugeEvent(outGaugeHandler_Updated);
    }

    private void outGaugeHandler_Updated(object sender, Gauge gauge)
    {
      // this code is required for Windows Forms, since our OutGauge update
      // comes from another thread and we can only perform form updates on
      // the UI thread
      if (this.InvokeRequired)
      {
        BeginInvoke(new OutGaugeHandler.GaugeEvent(outGaugeHandler_Updated), sender, gauge);
        return;
      }

      // now we update the form with the new data

      // text boxes
      timeTextBox.Text = gauge.Time.ToString();
      carTextBox.Text = gauge.Car;
      gearTextBox.Text = gauge.Gear.ToString();
      speedTextBox.Text = gauge.Speed.ToString("0.000");
      rpmTextBox.Text = gauge.RPM.ToString("0.000");
      turboTextBox.Text = gauge.Turbo.ToString("0.000");
      engineTempTextBox.Text = gauge.EngTemp.ToString("0.000");
      fuelTextBox.Text = gauge.Fuel.ToString("0.000");
      oilPressureTextBox.Text = gauge.OilPress.ToString("0.000");
      throttleTextBox.Text = gauge.Throttle.ToString("0.000");
      brakeTextBox.Text = gauge.Brake.ToString("0.000");
      clutchTextBox.Text = gauge.Clutch.ToString("0.000");
      display1TextBox.Text = gauge.DisplayMessage1;
      display2TextBox.Text = gauge.DisplayMessage2;
      idTextBox.Text = gauge.Id.ToString();

      // check boxes
      shiftCheckBox.Checked = gauge.ShiftLight;
      fullBeamCheckBox.Checked = gauge.FullBeam;
      handBrakeCheckBox.Checked = gauge.Handbrake;
      pitSpeedCheckBox.Checked = gauge.PitSpeedLimiter;
      tractionControlCheckBox.Checked = gauge.TractionControl;
      headLightsCheckBox.Checked = gauge.HeadLights;
      signalLeftCheckBox.Checked = gauge.SignalLeft;
      signalRightCheckBox.Checked = gauge.SignalRight;
      redlineCheckBox.Checked = gauge.Redline;
      oilWarnCheckBox.Checked = gauge.OilWarning;
      kmCheckBox.Checked = gauge.IsKM;
      barCheckBox.Checked = gauge.IsBar;

    }

  }
}
