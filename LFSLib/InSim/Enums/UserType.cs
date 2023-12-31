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

namespace FullMotion.LiveForSpeed.InSim.Enums
{
  // User Values (for UserType byte)


  /// <summary>
  /// User Values
  /// </summary>
  public enum UserType : byte
  {
    /// <summary>
    /// System message
    /// </summary>
    System,			// 0 - system message
    /// <summary>
    /// Normal visible user message
    /// </summary>
    User,			// 1 - normal visible user message
    /// <summary>
    /// Hidden message starting with special prefix defined in InSim initialization
    /// </summary>
    Prefix,			// 2 - hidden message starting with special prefix (see ISI)
    /// <summary>
    /// Hidden message typed on local pc with /o command
    /// </summary>
    O,				// 3 - hidden message typed on local pc with /o command
    //Num
  }

  // NOTE : Typing "/o MESSAGE" into LFS will send an IS_MSO with UserType = MSO_O
}
