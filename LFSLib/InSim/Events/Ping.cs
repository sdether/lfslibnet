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
  /// Response to <see cref="InSimHandler.RequestPing"/>
  /// </summary>
  public class Ping : EventArgs
  {
    #region Static Members ########################################################################
    private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    #endregion

    #region Member Variables ######################################################################
    private Packets.IS_TINY packet;
    #endregion

    #region Constructors ##########################################################################
    internal Ping(Packets.IS_TINY packet)
    {
      this.packet = packet;
    }
    #endregion

    #region Properties ############################################################################
    /// <summary>
    /// Id of the request that caused this event to be triggered. 0 if the event trigger was not
    /// manually requested
    /// </summary>
    public byte RequestId
    {
      get { return packet.ReqI; }
    }
    #endregion

    #region Methods ###############################################################################
    #endregion

    #region Private Methods #######################################################################
    #endregion
  }
}
