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
using FullMotion.LiveForSpeed.OutSim.PacketInfo;
using FullMotion.LiveForSpeed.Util;

namespace FullMotion.LiveForSpeed.OutSim.Events
{
  /// <summary>
  /// Every OutSim packet received by the handler generates a new PhysicsState object describing the current state of the car in the world
  /// </summary>
  public class PhysicsState
  {
		#region Static Members ########################################################################
    private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    #endregion

		#region Member Variables ######################################################################
    private OutSimPack packet;
		#endregion

		#region Constructors ##########################################################################
    internal PhysicsState(OutSimPack packet)
		{
      this.packet = packet;
    }
		#endregion

		#region Properties ############################################################################
    /// <summary>
    /// Time in milliseconds
    /// </summary>
    public uint Time
    {
      get { return packet.Time; }
    }
    /// <summary>
    /// Angular Velocity in the X direction
    /// </summary>
    public float AngularVelocityX
    {
      get { return packet.AngularVelocityX; }
    }
    /// <summary>
    /// Angular Velocity in the Y direction
    /// </summary>
    public float AngularVelocityY
    {
      get { return packet.AngularVelocityY; }
    }
    /// <summary>
    /// Angular Velocity in the Z direction
    /// </summary>
    public float AngularVelocityZ
    {
      get { return packet.AngularVelocityZ; }
    }
    /// <summary>
    /// Heading
    /// </summary>
    public float Heading
    {
      get { return packet.Heading; }
    }
    /// <summary>
    /// Pitch
    /// </summary>
    public float Pitch
    {
      get { return packet.Pitch; }
    }
    /// <summary>
    /// Roll
    /// </summary>
    public float Roll
    {
      get { return packet.Roll; }
    }
    /// <summary>
    /// Acceleration in the X direction
    /// </summary>
    public float AccelerationX
    {
      get { return packet.AccelerationX; }
    }
    /// <summary>
    /// Acceleration in the Y direction
    /// </summary>
    public float AccelerationY
    {
      get { return packet.AccelerationY; }
    }
    /// <summary>
    /// Acceleration in the Z direction
    /// </summary>
    public float AccelerationZ
    {
      get { return packet.AccelerationZ; }
    }
    /// <summary>
    /// Velocity in the X direction
    /// </summary>
    public float VelocityX
    {
      get { return packet.VelocityX; }
    }
    /// <summary>
    /// Velocity in the Y direction
    /// </summary>
    public float VelocityY
    {
      get { return packet.VelocityY; }
    }
    /// <summary>
    /// Velocity in the Z direction
    /// </summary>
    public float VelocityZ
    {
      get { return packet.VelocityZ; }
    }
    /// <summary>
    /// X world coordinate
    /// </summary>
    public int PositionX
    {
      get { return packet.PositionX; }
    }
    /// <summary>
    /// Y world coordinate
    /// </summary>
    public int PositionY
    {
      get { return packet.PositionY; }
    }
    /// <summary>
    /// Z world coordinate
    /// </summary>
    public int PositionZ
    {
      get { return packet.PositionZ; }
    }
    /// <summary>
    /// OutSim Id
    /// </summary>
    public int Id
    {
      get { return packet.Id; }
    }
    #endregion

		#region Methods ###############################################################################
		#endregion

		#region Private Methods #######################################################################
		#endregion
  }
}
