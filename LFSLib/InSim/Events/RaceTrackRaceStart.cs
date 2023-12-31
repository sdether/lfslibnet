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
	/// Event sent when a race starts
	/// </summary>
  public class RaceTrackRaceStart : RaceTrackEvent
	{
		#region Static Members ########################################################################
    private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    #endregion

		#region Member Variables ######################################################################
		private Packets.IS_RST packet;
		#endregion

		#region Constructors ##########################################################################
    internal RaceTrackRaceStart(Packets.IS_RST packet)
		{
      this.packet = packet;

			log.Debug("RaceTrackRaceStart event created");
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
		/// Race Lap Info
		/// </summary>
		public LapInfo LapInfo
		{
			get { return new LapInfo(packet.RaceLaps); }
		}
		/// <summary>
		/// Total minutes for Qualifying
		/// </summary>
		public byte QualifyingMinutes
		{
			get { return packet.QualMins; }
		}
		/// <summary>
		/// How many players in this race
		/// </summary>
		public byte NumberInRace
		{
			get { return packet.NumP; }
		}
    /// <summary>
    /// Current weather
    /// </summary>
    public Enums.Weather Weather
    {
      get { return packet.Weather; }
    }
    /// <summary>
    /// Current weather
    /// </summary>
    public Enums.Wind Wind
    {
      get { return packet.Wind; }
    }
    /// <summary>
		/// The short name for the current track
		/// </summary>
		public string ShortTrackName
		{
			get { return packet.Track.Value; }
		}

    /// <summary>
    /// Number of nodes in this race configuration
    /// </summary>
    public int NumberOfNodes
    {
      get { return packet.NumNodes; }
    }

    /// <summary>
    /// Index of the Start/Finish line node
    /// </summary>
    public int FinishNodeIndex
    {
      get { return packet.Finish; }
    }

    /// <summary>
    /// Index of the first split line node
    /// </summary>
    public int Split1NodeIndex
    {
      get { return packet.Split1; }
    }

    /// <summary>
    /// Index of the second split line node
    /// </summary>
    public int Split2NodeIndex
    {
      get { return packet.Split2; }
    }

    /// <summary>
    /// Index of the third split line node
    /// </summary>
    public int Split3NodeIndex
    {
      get { return packet.Split3; }
    }

    /// <summary>
    /// Users can vote
    /// </summary>
    public bool CanVote
    {
      get { return Flags.RaceFlags.CAN_VOTE == (Flags.RaceFlags.CAN_VOTE & packet.Flags); }
    }

    /// <summary>
    /// Users can select tracks
    /// </summary>
    public bool CanSelect
    {
      get { return Flags.RaceFlags.CAN_SELECT == (Flags.RaceFlags.CAN_SELECT & packet.Flags); }
    }

    /// <summary>
    /// Users can join mid race
    /// </summary>
    public bool MidRaceJoin
    {
      get { return Flags.RaceFlags.MID_RACE == (Flags.RaceFlags.MID_RACE & packet.Flags); }
    }

    /// <summary>
    /// Racers must make a pitstop for a valid race
    /// </summary>
    public bool MustPit
    {
      get { return Flags.RaceFlags.MUST_PIT == (Flags.RaceFlags.MUST_PIT & packet.Flags); }
    }

    /// <summary>
    /// Racers can reset their cars
    /// </summary>
    public bool CanReset
    {
      get { return Flags.RaceFlags.CAN_RESET == (Flags.RaceFlags.CAN_RESET & packet.Flags); }
    }

		#endregion

		#region Methods ###############################################################################
		#endregion

		#region Private Methods #######################################################################
		#endregion
	}
}
