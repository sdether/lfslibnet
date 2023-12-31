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
  /// Client request for AutocrossInfo information
  /// </summary>
  public class AutocrossInfo : EventArgs
  {
    #region Static Members ########################################################################
    private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    #endregion

    #region Member Variables ######################################################################
    private Packets.IS_AXI packet;
    private string layout;
    #endregion

    #region Constructors ##########################################################################
    internal AutocrossInfo(Packets.IS_AXI packet)
    {
      this.packet = packet;
      this.layout = CharHelper.GetString(packet.LName);
    }
    #endregion

    #region Properties ############################################################################
    /// <summary>
    /// The id of the originating request, if any
    /// </summary>
    public byte RequestId
    {
      get { return packet.ReqI; }
    }

    /// <summary>
    /// Number of Checkpoints in the layout
    /// </summary>
    public int NumberOfCheckPoints
    {
      get { return packet.NumCP; }
    }

    /// <summary>
    /// Number of objects in the layout
    /// </summary>
    public int NumberOfObjects
    {
      get { return packet.NumO; }
    }

    /// <summary>
    /// The name of the layout last loaded (if loaded locally)
    /// </summary>
    public string Layoutname
    {
      get { return layout; }
    }
    #endregion

    #region Methods ###############################################################################
    #endregion

    #region Private Methods #######################################################################
    #endregion
  }
}
