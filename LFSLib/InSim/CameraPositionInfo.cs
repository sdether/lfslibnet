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

namespace FullMotion.LiveForSpeed.InSim
{
  /// <summary>
  /// Camera Position Event
  /// </summary>
  public class CameraPositionInfo
  {
    #region Static Members ########################################################################
    private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    #endregion

    #region Member Variables ######################################################################
    private Packets.IS_CPP packet;
    #endregion

    #region Constructors ##########################################################################
    internal CameraPositionInfo()
    {
      this.packet = new Packets.IS_CPP();
      this.packet.Size = 32;
      this.packet.Type = Enums.ISP.CPP;
    }

    internal CameraPositionInfo(Packets.IS_CPP packet)
    {
      this.packet = packet;
    }
    #endregion

    #region Properties ############################################################################
    /// <summary>
    /// Camera Position in the world if in <see cref="InShiftUMode"/> and <see cref="IsFollowing"/>
    /// is false. If <see cref="IsFollowing"/> is true, the position is relative to the car.
    /// </summary>
    public Vector Position
    {
      get { return new Vector(packet.Pos); }
      set { packet.Pos = value.Vec; }
    }
    /// <summary>
    /// Heading of the camera. 0 points along Y axis
    /// </summary>
    public ushort Heading
    {
      get { return packet.H; }
      set { packet.H = value; }
    }
    /// <summary>
    /// Pitch of the camera. 0 means looking at horizon
    /// </summary>
    public ushort Pitch
    {
      get { return packet.P; }
      set { packet.P = value; }
    }
    /// <summary>
    /// Roll of the camera. 0 means no roll
    /// </summary>
    public ushort Roll
    {
      get { return packet.R; }
      set { packet.R = value; }
    }
    /// <summary>
    /// Player Id of car the camera is looking at. 0 is the pole car
    /// </summary>
    public byte ViewPlayerId
    {
      get { return packet.ViewPLID; }
      set { packet.ViewPLID = value; }
    }
    /// <summary>
    /// Type of Camera
    /// </summary>
    public Enums.View Camera
    {
      get { return packet.InGameCam; }
      set { packet.InGameCam = value; }
    }
    /// <summary>
    /// Field Of View in radians
    /// </summary>
    public float FOV
    {
      get { return packet.FOV; }
      set { packet.FOV = value; }
    }
    /// <summary>
    /// Time to get to position. 0 means instantaneously and reset
    /// </summary>
    public ushort Time
    {
      get { return packet.Time; }
      set { packet.Time = value; }
    }
    /// <summary>
    /// Camera in Free View/Autox view mode
    /// </summary>
    public bool InShiftUMode
    {
      get { return (packet.StateFlags & Flags.ISS.SHIFTU) == Flags.ISS.SHIFTU; }
      set { packet.StateFlags = (value) ? packet.StateFlags | Flags.ISS.SHIFTU : packet.StateFlags & ~Flags.ISS.SHIFTU; }
    }
    /// <summary>
    /// Camera in High View Mode (has no effect if <see cref="InShiftUMode"/> is false)
    /// </summary>
    public bool IsHighView
    {
      get { return (packet.StateFlags & Flags.ISS.SHIFTU_HIGH) == Flags.ISS.SHIFTU_HIGH; }
      set { packet.StateFlags = (value) ? packet.StateFlags | Flags.ISS.SHIFTU_HIGH : packet.StateFlags & ~Flags.ISS.SHIFTU_HIGH; }
    }
    /// <summary>
    /// Camera in Follow Mode (has no effect if <see cref="InShiftUMode"/> is false)
    /// </summary>
    public bool IsFollowing
    {
      get { return (packet.StateFlags & Flags.ISS.SHIFTU_FOLLOW) == Flags.ISS.SHIFTU_FOLLOW; }
      set { packet.StateFlags = (value) ? packet.StateFlags | Flags.ISS.SHIFTU_FOLLOW : packet.StateFlags & ~Flags.ISS.SHIFTU_FOLLOW; }
    }
    /// <summary>
    /// Camera is overriding User View
    /// </summary>
    public bool OverrideUserView
    {
      get { return (packet.StateFlags & Flags.ISS.VIEW_OVERRIDE) == Flags.ISS.VIEW_OVERRIDE; }
      set { packet.StateFlags = (value) ? packet.StateFlags | Flags.ISS.VIEW_OVERRIDE : packet.StateFlags & ~Flags.ISS.VIEW_OVERRIDE; }
    }
    #endregion

    #region Methods ###############################################################################
    internal Packets.IClientLfsInSimPacket GetPacket()
    {
      return packet;
    }
    #endregion

    #region Private Methods #######################################################################
    #endregion
  }
}
