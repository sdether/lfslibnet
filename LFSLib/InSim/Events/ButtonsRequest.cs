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
  /// DEPRECATED: User has requested buttons to be shown
  /// </summary>
  [Obsolete("Replaced by ButtonsRequest in 0.18+")]
  public class ButtonRequest : ButtonsRequest
  {
    #region Constructors ##########################################################################
    internal ButtonRequest(Packets.IS_BFN packet)
      : base(packet)
    {
    }
    #endregion
  }

  /// <summary>
  /// User has requested buttons to be shown
  /// </summary>
  public class ButtonsRequest : EventArgs
  {
    #region Static Members ########################################################################
    private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    #endregion

    #region Member Variables ######################################################################
    private Packets.IS_BFN packet;
    #endregion

    #region Constructors ##########################################################################
    internal ButtonsRequest(Packets.IS_BFN packet)
    {
      this.packet = packet;
    }
    #endregion

    #region Properties ############################################################################
    /// <summary>
    /// Did the request come from the local connection or from another client
    /// </summary>
    public bool Local
    {
      get { return (packet.UCID == 0); }
    }

    /// <summary>
    /// The connection Id of the requestor (0 if the request was locally originated)
    /// </summary>
    public byte ConnectionId
    {
      get { return packet.UCID; }
    }
    #endregion

    #region Methods ###############################################################################
    #endregion

    #region Private Methods #######################################################################
    #endregion
  }
}
