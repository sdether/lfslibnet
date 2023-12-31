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

namespace FullMotion.LiveForSpeed.InSim
{
  /// <summary>
  /// Information about the passenger load for a car
  /// </summary>
  public class Passengers
  {
    Flags.Passengers passengers;

    internal Passengers(Flags.Passengers passengers)
    {
      this.passengers = passengers;
    }

    /// <summary>
    /// Front Passenger
    /// </summary>
    public Enums.Passenger Front
    {
      get { return CalcPassenger(Flags.Passengers.FrontOccupied, Flags.Passengers.FrontFemale); }
    }

    /// <summary>
    /// Rear Left Passenger
    /// </summary>
    public Enums.Passenger RearLeft
    {
      get { return CalcPassenger(Flags.Passengers.RearLeft, Flags.Passengers.RearLeftFemale); }
    }

    /// <summary>
    /// Rear Middle Passenger
    /// </summary>
    public Enums.Passenger RearMiddle
    {
      get { return CalcPassenger(Flags.Passengers.RearMiddle, Flags.Passengers.RearMiddleFemale); }
    }

    /// <summary>
    /// Rear Right Passenger
    /// </summary>
    public Enums.Passenger RearRight
    {
      get { return CalcPassenger(Flags.Passengers.RearRight, Flags.Passengers.RearRightFemale); }
    }

    private Enums.Passenger CalcPassenger(Flags.Passengers occupied, Flags.Passengers female)
    {
      if ((passengers & occupied) == occupied)
      {
        if ((passengers & female) == female)
        {
          return Enums.Passenger.Female;
        }
        return Enums.Passenger.Male;
      }
      else
      {
        return Enums.Passenger.Empty;
      }
    }

  }
}
