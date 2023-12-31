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
using log4net;
using FullMotion.LiveForSpeed.InSim.Packets.Support;
using FullMotion.LiveForSpeed.Util;

namespace FullMotion.LiveForSpeed.InSim.Events
{
  /// <summary>
  /// Info about a single host accessible via the relay
  /// </summary>
  public class RelayHostInfo : EventArgs
	{
		#region Static Members ########################################################################
    private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    #endregion

		#region Member Variables ######################################################################
    private Packets.Support.HInfo packet;
    private string hostName;
    private string trackName;
		#endregion

		#region Constructors ##########################################################################
    internal RelayHostInfo(Packets.Support.HInfo packet)
		{
      this.packet = packet;
		}
		#endregion

		#region Properties ############################################################################
    /// <summary>
    /// Name of the host
    /// </summary>
    public string Name
    {
      get
      {
        if (string.IsNullOrEmpty(hostName))
        {
          hostName = CharHelper.GetString(packet.HName);
        }
        return hostName;
      }
    }

    /// <summary>
    /// The track currently being run on the host
    /// </summary>
    public string ShortTrackname
    {
      get
      {
        if (string.IsNullOrEmpty(trackName))
        {
          trackName = CharHelper.GetString(packet.Track);
        }
        return trackName;
      }
    }

    /// <summary>
    /// Number of connections on the host
    /// </summary>
    public byte NumberOfConnections
    {
      get { return packet.NumConns; }
    }

    /// <summary>
    /// Does this host require a spectator password?
    /// </summary>
    public bool RequiresSpectatorPassword
    {
      get { return (packet.Flags & Flags.HOS.SpecPass) == Flags.HOS.SpecPass; }
    }
		#endregion
	}
}
