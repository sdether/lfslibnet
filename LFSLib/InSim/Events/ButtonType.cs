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
using FullMotion.LiveForSpeed.Util;

namespace FullMotion.LiveForSpeed.InSim.Events
{
  /// <summary>
  /// Text typed into a custom Dialog
  /// </summary>
  public class ButtonType : EventArgs
  {
    #region Static Members ########################################################################
    private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    #endregion

    #region Member Variables ######################################################################
    private Packets.IS_BTT packet;
    private string text;
    #endregion

    #region Constructors ##########################################################################
    internal ButtonType(Packets.IS_BTT packet)
    {
      this.packet = packet;
    }
    #endregion

    #region Properties ############################################################################
    /// <summary>
    /// Did the type event come from the local connection or from another client
    /// </summary>
    public bool Local
    {
      get { return (packet.UCID == 0); }
    }

    /// <summary>
    /// The connection Id of the requestor (0 if the type was locally originated)
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
    /// The Id of the button dialog activated
    /// </summary>
    public byte ButtonId
    {
      get { return packet.ClickID; }
    }

    /// <summary>
    /// The typed in text
    /// </summary>
    public string Text
    {
      get
      {
        if (string.IsNullOrEmpty(text))
        {
          text = CharHelper.GetString(packet.Text);
        }
        return text;
      }
    }

    #endregion

    #region Methods ###############################################################################
    #endregion

    #region Private Methods #######################################################################
    #endregion
  }
}
