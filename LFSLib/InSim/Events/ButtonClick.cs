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
using System.Collections.Generic;
using System.Text;

using log4net;

namespace FullMotion.LiveForSpeed.InSim.Events
{
  /// <summary>
  /// A button was clicked
  /// </summary>
  public class ButtonClick : EventArgs
  {
    #region Static Members ########################################################################
    private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    #endregion

    #region Member Variables ######################################################################
    private Packets.IS_BTC packet;
    #endregion

    #region Constructors ##########################################################################
    internal ButtonClick(Packets.IS_BTC packet)
    {
      this.packet = packet;
    }
    #endregion

    #region Properties ############################################################################
    /// <summary>
    /// Did the click event come from the local connection or from another client
    /// </summary>
    public bool Local
    {
      get { return (packet.UCID == 0); }
    }

    /// <summary>
    /// The connection Id of the requestor (0 if the click was locally originated)
    /// </summary>
    public byte ConnectionId
    {
      get { return packet.UCID; }
    }

    /// <summary>
    /// The Id of the request that created the button
    /// </summary>
    public byte RequestId
    {
      get { return packet.ReqI; }
    }

    /// <summary>
    /// The Id of the button clicked
    /// </summary>
    public byte ButtonId
    {
      get { return packet.ClickID; }
    }

    /// <summary>
    /// Was the Left Mouse Button clicked?
    /// </summary>
    public bool LeftMouseButtonClick
    {
      get { return (Flags.ClickFlags.LMB == (Flags.ClickFlags.LMB & packet.CFlags)); }
    }

    /// <summary>
    /// Was the Right Mouse Button clicked?
    /// </summary>
    public bool RightMouseButtonClick
    {
      get { return (Flags.ClickFlags.RMB == (Flags.ClickFlags.RMB & packet.CFlags)); }
    }

    /// <summary>
    /// Was the Ctrl key down at the time of the click?
    /// </summary>
    public bool CtrlClick
    {
      get { return (Flags.ClickFlags.CTRL == (Flags.ClickFlags.CTRL & packet.CFlags)); }
    }

    /// <summary>
    /// Was the Shift key down at the time of the click?
    /// </summary>
    public bool ShiftClick
    {
      get { return (Flags.ClickFlags.SHIFT == (Flags.ClickFlags.SHIFT & packet.CFlags)); }
    }
    #endregion

    #region Methods ###############################################################################
    #endregion

    #region Private Methods #######################################################################
    #endregion
  }
}
