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
using FullMotion.LiveForSpeed.Util;

namespace FullMotion.LiveForSpeed.InSim.Events
{
  /// <summary>
  /// A text message sent from LFS
  /// </summary>
  public class Message : EventArgs
  {
    #region Static Members ########################################################################
    private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    #endregion

    #region Member Variables ######################################################################
    private Packets.IS_MSO packet;
    private bool isSplitMessage = false;
    private string message;
    private string player;
    #endregion

    #region Constructors ##########################################################################
    internal Message(Packets.IS_MSO packet, bool isSplitMessage)
    {
      this.packet = packet;
      this.isSplitMessage = isSplitMessage;
    }
    #endregion

    #region Properties ############################################################################
    /// <summary>
    /// Is the Message a Split Message, i.e. the User Name is accessible via <see cref="Playername"/>
    /// and not part of <see cref="MessageText"/>
    /// </summary>
    public bool IsSplitMessage
    {
      get { return isSplitMessage; }
    }
    /// <summary>
    /// DEPRECATED: The name of the player sending the message. Empty string if this is not a Split Message
    /// </summary>
    [Obsolete("Replaced with PlayerName in 0.18+")]
    public string Playername
    {
      get { return PlayerName; }
    }
    /// <summary>
    /// The name of the player sending the message. Empty string if this is not a Split Message
    /// </summary>
    public string PlayerName
    {
      get
      {
        if (string.IsNullOrEmpty(player))
        {
          if (isSplitMessage)
          {
            byte[] usernameBytes = new byte[packet.TextStart];
            Array.Copy(packet.Msg, 0, usernameBytes, 0, packet.TextStart);
            player = CharHelper.GetString(usernameBytes);
          }
          else
          {
            player = string.Empty;
          }
        }
        return player;
      }
    }
    /// <summary>
    /// DEPRECATED: The name of the player sending the message as received in bytes. Empty array if this is not a Split Message
    /// </summary>
    [Obsolete("Replaced with RawPlayerName in 0.18+")]
    public byte[] RawPlayername
    {
      get { return RawPlayerName; }
    }
    /// <summary>
    /// The name of the player sending the message as received in bytes. Empty array if this is not a Split Message
    /// </summary>
    public byte[] RawPlayerName
    {
      get
      {
        if (isSplitMessage)
        {
          byte[] playerBytes = new byte[packet.TextStart];
          Array.Copy(packet.Msg, 0, playerBytes, 0, packet.TextStart);
          return playerBytes;
        }
        else
        {
          return new byte[0];
        }
      }
    }
    /// <summary>
    /// The text of the message
    /// </summary>
    public string MessageText
    {
      get
      {
        if (string.IsNullOrEmpty(message))
        {
          if (isSplitMessage)
          {
            int msgLength = packet.Msg.Length - packet.TextStart;
            byte[] msgBytes = new byte[msgLength];
            Array.Copy(packet.Msg, packet.TextStart, msgBytes, 0, msgLength);
            message = CharHelper.GetString(msgBytes);
          }
          else
          {
            message = CharHelper.GetString(packet.Msg);
          }
        }
        return message;
      }
    }
    /// <summary>
    /// The text of the message in bytes
    /// </summary>
    public byte[] RawMessageText
    {
      get
      {
        if (isSplitMessage)
        {
          int msgLength = packet.Msg.Length - packet.TextStart;
          byte[] msgBytes = new byte[msgLength];
          Array.Copy(packet.Msg, packet.TextStart, msgBytes, 0, msgLength);
          return msgBytes;
        }
        else
        {
          return packet.Msg;
        }
      }
    }
    /// <summary>
    /// The sending player's Id. If 0 check <see cref="ConnectionId"/> instead
    /// </summary>
    public byte PlayerId
    {
      get { return packet.PLID; }
    }
    /// <summary>
    /// The sending connection's Id. If 0 then the message came from the host
    /// </summary>
    public byte ConnectionId
    {
      get { return packet.UCID; }
    }
    /// <summary>
    /// The user type of the sender 
    /// </summary>
    public Enums.UserType UserType
    {
      get { return packet.UserType; }
    }
    #endregion

    #region Methods ###############################################################################
    #endregion

    #region Private Methods #######################################################################
    #endregion
  }
}
