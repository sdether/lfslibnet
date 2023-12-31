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
using System.Text;
using log4net;

namespace FullMotion.LiveForSpeed.InSim.Events
{
	/// <summary>
	/// The State Event
	/// </summary>
  public class State : EventArgs
	{
		#region Static Members ########################################################################
    private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    #endregion

		#region Member Variables ######################################################################
		private Packets.IS_STA packet;
		private DateTime stateTime = DateTime.Now;
		#endregion

		#region Constructors ##########################################################################
    internal State(Packets.IS_STA packet)
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
    /// <summary>
		/// The local Date and Time when the State event was received
		/// </summary>
		public DateTime DateTime
		{
			get { return stateTime; }
		}

		/// <summary>
		/// Speed of the Race Replay. 1.0 is normal speed, higher is faster, lower is slower
		/// </summary>
		public float ReplaySpeed
		{
			get { return packet.ReplaySpeed; }
		}

		/// <summary>
		/// Is the player in the FrontEndScreen
		/// </summary>
		public bool IsInFrontEndScreen
		{
			get { return (packet.Flags&Flags.ISS.FRONT_END)==Flags.ISS.FRONT_END; }
		}

		/// <summary>
		/// Is the player in the Game
		/// </summary>
		public bool IsInGame
		{
			get { return (packet.Flags&Flags.ISS.GAME)==Flags.ISS.GAME; }
		}

		/// <summary>
		/// Is the player in Multiplayer Mode
		/// </summary>
		public bool IsInMultiplayerMode
		{
			get { return (packet.Flags&Flags.ISS.MULTI)==Flags.ISS.MULTI; }
		}

		/// <summary>
		/// Is the Multiplayer Speed up option turned on
		/// </summary>
		public bool MultiplayerSpeedupOptionOn
		{
			get { return (packet.Flags&Flags.ISS.MPSPEEDUP)==Flags.ISS.MPSPEEDUP; }
		}

		/// <summary>
		/// Is the sound muted
		/// </summary>
		public bool IsMuted
		{
			get { return (packet.Flags&Flags.ISS.SOUND_MUTE)==Flags.ISS.SOUND_MUTE; }
		}

		/// <summary>
		/// Is the game paused
		/// </summary>
		public bool IsPaused
		{
			get { return (packet.Flags&Flags.ISS.PAUSED)==Flags.ISS.PAUSED; }
		}

		/// <summary>
		/// Is the player in a Replay
		/// </summary>
		public bool IsReplay
		{
			get { return (packet.Flags&Flags.ISS.REPLAY)==Flags.ISS.REPLAY; }
		}

		/// <summary>
		/// Is the user view in Free View/Autox view mode
		/// </summary>
		public bool InShiftUMode
		{
			get { return (packet.Flags&Flags.ISS.SHIFTU)==Flags.ISS.SHIFTU; }
		}

		/// <summary>
		/// Is the user camera in follow mode
		/// </summary>
		public bool IsFollowing
		{
			get { return (packet.Flags&Flags.ISS.SHIFTU_FOLLOW)==Flags.ISS.SHIFTU_FOLLOW; }
		}

		/// <summary>
		/// Is the user camera in High View mode
		/// </summary>
		public bool IsHighView
		{
			get { return (packet.Flags&Flags.ISS.SHIFTU_HIGH)==Flags.ISS.SHIFTU_HIGH; }
		}

		/// <summary>
		/// Are the Shift-U buttons hidden
		/// </summary>
		public bool ShiftUButtonsHidden
		{
			get { return (packet.Flags&Flags.ISS.SHIFTU_NO_OPT)==Flags.ISS.SHIFTU_NO_OPT; }
		}

		/// <summary>
		/// Is the user camera in 2D mode
		/// </summary>
		public bool IsIn2DView
		{
			get { return (packet.Flags&Flags.ISS.SHOW_2D)==Flags.ISS.SHOW_2D; }
		}

		/// <summary>
		/// Is the uesr view overridden by an external source
		/// </summary>
		public bool OverridingUserView
		{
			get { return (packet.Flags&Flags.ISS.VIEW_OVERRIDE)==Flags.ISS.VIEW_OVERRIDE; }
		}

    /// <summary>
    /// Is LFS running in a Window
    /// </summary>
    public bool IsWindowed
    {
      get { return (packet.Flags & Flags.ISS.WINDOWED) == Flags.ISS.WINDOWED; }
    }

    /// <summary>
    /// Are InSim Buttons visible
    /// </summary>
    public bool Visible
    {
      get { return (packet.Flags & Flags.ISS.VISIBLE) == Flags.ISS.VISIBLE; }
    }

    /// <summary>
		/// Type of Camera
		/// </summary>
		public Enums.View InGameCamera
		{
			get { return packet.InGameCam; }
		}

		/// <summary>
		/// Player ID of car the camera is looking at. 0 means no car
		/// </summary>
		public byte ViewedPlayerId
		{
			get { return packet.ViewPLID; }
		}

		/// <summary>
		/// Number of Players
		/// </summary>
		public byte NumberOfPlayers
		{
			get { return packet.NumP; }
		}

		/// <summary>
		/// Number of Connections
		/// </summary>
		public byte NumberOfConnections
		{
			get { return packet.NumConns; }
		}

		/// <summary>
		/// Number of Players who have finished or qualified
		/// </summary>
		public byte PlayersFinished
		{
			get { return packet.NumFinished; }
		}

		/// <summary>
		/// The state of the Race
		/// </summary>
		public Enums.RaceState RaceState
		{
			get { return packet.RaceInProg; }
		}

		/// <summary>
		/// Minutes for Qualifying
		/// </summary>
		public byte QualifyingMinutes
		{
			get { return packet.QualMins; }
		}

		/// <summary>
		/// Number of Laps in Race
		/// </summary>
		public LapInfo RaceLaps
		{
			get { return new LapInfo(packet.RaceLaps); }
		}

		/// <summary>
		/// The short name for the current track
		/// </summary>
		public string ShortTrackName
		{
			get { return packet.Track.Value; }
		}

		/// <summary>
		/// Current Weather
		/// </summary>
		public Enums.Weather Weather
		{
			get { return packet.Weather; }
		}

		/// <summary>
		/// Current Wind
		/// </summary>
		public Enums.Wind Wind
		{
			get { return packet.Wind; }
		}
		#endregion

		#region Methods ###############################################################################
		#endregion

		#region Private Methods #######################################################################
		#endregion
	}
}
