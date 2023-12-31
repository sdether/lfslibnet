namespace FullMotion.LiveForSpeed.TimeMonitor
{
  partial class TimeMonitor
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.actionButton = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.textBoxHost = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.textBoxPort = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // actionButton
      // 
      this.actionButton.Location = new System.Drawing.Point(127, 32);
      this.actionButton.Name = "actionButton";
      this.actionButton.Size = new System.Drawing.Size(92, 23);
      this.actionButton.TabIndex = 0;
      this.actionButton.Text = "Connect";
      this.actionButton.UseVisualStyleBackColor = true;
      this.actionButton.Click += new System.EventHandler(this.actionButton_Click);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 9);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(32, 13);
      this.label1.TabIndex = 1;
      this.label1.Text = "Host:";
      // 
      // textBoxHost
      // 
      this.textBoxHost.Location = new System.Drawing.Point(65, 6);
      this.textBoxHost.Name = "textBoxHost";
      this.textBoxHost.Size = new System.Drawing.Size(77, 20);
      this.textBoxHost.TabIndex = 2;
      this.textBoxHost.Text = "127.0.0.1";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(142, 9);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(29, 13);
      this.label2.TabIndex = 3;
      this.label2.Text = "Port:";
      // 
      // textBoxPort
      // 
      this.textBoxPort.Location = new System.Drawing.Point(177, 6);
      this.textBoxPort.Name = "textBoxPort";
      this.textBoxPort.Size = new System.Drawing.Size(42, 20);
      this.textBoxPort.TabIndex = 4;
      this.textBoxPort.Text = "30000";
      // 
      // TimeMonitor
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(232, 65);
      this.Controls.Add(this.textBoxPort);
      this.Controls.Add(this.textBoxHost);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.actionButton);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.Name = "TimeMonitor";
      this.Text = "Time Monitor";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button actionButton;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox textBoxHost;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox textBoxPort;
  }
}

