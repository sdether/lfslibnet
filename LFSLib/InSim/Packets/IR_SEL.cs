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

namespace FullMotion.LiveForSpeed.InSim.Packets
{
  struct IR_SEL : IClientLfsInSimPacket
  {
    public byte Size;		// 68
    public Enums.ISP Type;		// IRP_SEL
    public byte ReqI;		// If non-zero Relay will send an IS_VER packet
    public byte Zero;		// 0

    public byte[] HName; //[32];	// Hostname to receive data from
    public byte[] Admin; //[16];	// Admin password (to gain admin access to host)
    public byte[] Spec; //[16];	// Spectator password (if host requires it)

    public IR_SEL(string hostname, string adminPass, string specPass)
    {
      Size = 68;
      Type = Enums.ISP.IRP_SEL;
      ReqI = PacketFactory.NextRequestId;
      Zero = 0;
      HName = new byte[32];
      byte[] hostBytes = Encoding.ASCII.GetBytes(hostname);
      Array.Copy(hostBytes, HName, Math.Min(32,hostBytes.Length));
      Admin = new byte[16];
      byte[] adminBytes = Encoding.ASCII.GetBytes((adminPass == null) ? string.Empty : adminPass);
      Array.Copy(adminBytes, Admin, Math.Min(16, adminBytes.Length));
      Spec = new byte[16];
      byte[] specBytes = Encoding.ASCII.GetBytes((specPass == null) ? string.Empty : specPass);
      Array.Copy(specBytes, Spec, Math.Min(16, specBytes.Length));
    }

    #region ILfsInSimPacket Members

    public Enums.ISP PacketType
    {
      get { return Type; }
    }

    #endregion

    #region IClientLfsInSimPacket Members

    public byte RequestId
    {
      get { return ReqI; }
    }

    public byte[] GetBytes()
    {
      byte[] bytes = new byte[Size];

      int position = 0;

      bytes[position++] = Size;
      bytes[position++] = (byte)Type;
      bytes[position++] = ReqI;
      bytes[position++] = Zero;

      Array.Copy(this.HName, 0, bytes, position, this.HName.Length);
      position += this.HName.Length;
      Array.Copy(this.Admin, 0, bytes, position, this.Admin.Length);
      position += this.Admin.Length;
      Array.Copy(this.Spec, 0, bytes, position, this.Spec.Length);
      position += this.Spec.Length;

      return bytes;
    }

    #endregion
  }
}
