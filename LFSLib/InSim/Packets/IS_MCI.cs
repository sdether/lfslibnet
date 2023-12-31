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
  /// <summary>
  /// Multi Car Info - if more than 8 in race then more than one of these is sent
  /// </summary>
  struct IS_MCI : ILfsInSimPacket
  {
    public byte Size;		// 4 + NumP * 28
    public Enums.ISP Type;		// ISP_MCI
    public byte ReqI;		// 0 unless this is a reply to an TINY_MCI request
    public byte NumC;		// number of valid CompCar structs in this packet

    public Support.CompCar[] Info; //[8];	// car info for each player, 1 to 8 of these (NumC)

    public IS_MCI(byte[] bytes)
    {
      int position = 0;
      Size = bytes[position++];
      Type = (Enums.ISP)bytes[position++];
      ReqI = bytes[position++];
      NumC = bytes[position++];
      Info = new Support.CompCar[NumC];
      for (int i = 0; i < NumC; i++)
      {
        byte[] compCarBytes = new byte[Support.CompCar.SIZE];
        Array.Copy(bytes,position,compCarBytes,0,Support.CompCar.SIZE);
        position += Support.CompCar.SIZE;
        Info[i] = new Support.CompCar(compCarBytes);
      }
    }

    #region ILfsInSimPacket Members

    public Enums.ISP PacketType
    {
      get { return Type; }
    }

    #endregion
  }

  // You can change the rate of NLP or MCI after initialisation by sending this IS_SMALL :

  // ReqI : 0
  // SubT : SMALL_NLI		(Node Lap Interval)
  // UVal : interval      (0 means stop, otherwise interval in ms, 100 to 8000)
}
