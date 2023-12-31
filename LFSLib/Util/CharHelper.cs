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
using log4net;

namespace FullMotion.LiveForSpeed.Util
{
  public class CharHelper
  {
    static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    public static string GetString(byte[] bytes)
    {
      string s = Encoding.ASCII.GetString(bytes);
      log.Debug("raw: " + s);

      Encoding nextEnc = EncodingHelper.encodingLatin1;
      Encoding currentEnc = EncodingHelper.encodingLatin1;

      StringBuilder sb = new StringBuilder();
      int endIndex = 0;
      int startIndex = 0;
      Boolean done = false;
      for (int i = 0; i < bytes.Length; i++)
      {
        if (bytes[i] == 0)
        {
          log.Debug("got null byte at " + i);
          endIndex = i;
          done = true;
        }
        else if (bytes[i] == '^')
        {
          log.Debug("got control sequence at " + i);
          // next char is new encoding

          // TODO: Need to handle special reset cases:
          // ^8 means : return to original colour and code page
          // ^: means : return to original colour and Latin-1
          bool encodingSequence = false;
          switch ((char)bytes[i + 1])
          {
            case 'B':
              encodingSequence = true;
              nextEnc = EncodingHelper.encodingBaltic;
              log.Debug("baltic encoding: " + nextEnc.EncodingName);
              break;
            case 'C':
              encodingSequence = true;
              nextEnc = EncodingHelper.encodingCyrillic;
              log.Debug("cyrillic encoding: " + nextEnc.EncodingName);
              break;
            case 'E':
              encodingSequence = true;
              nextEnc = EncodingHelper.encodingCentralEurope;
              log.Debug("central european encoding: " + nextEnc.EncodingName);
              break;
            case 'G':
              encodingSequence = true;
              nextEnc = EncodingHelper.encodingGreek;
              log.Debug("greek encoding: " + nextEnc.EncodingName);
              break;
            case 'J':
              encodingSequence = true;
              nextEnc = EncodingHelper.encodingJapanese;
              log.Debug("japanese encoding: " + nextEnc.EncodingName);
              break;
            case 'L':
              encodingSequence = true;
              nextEnc = EncodingHelper.encodingLatin1;
              log.Debug("latin-1 encoding: " + nextEnc.EncodingName);
              break;
            case 'T':
              encodingSequence = true;
              nextEnc = EncodingHelper.encodingTurkish;
              log.Debug("turkish encoding: " + nextEnc.EncodingName);
              break;
          }
          if (encodingSequence)
          {
            endIndex = i;
            i++;
          }
          else if (i == bytes.Length - 1)
          {
            log.Debug("End without a null");
            endIndex = i + 1;
          }
          else
          {
            continue;
          }
        }
        else if (i == bytes.Length - 1)
        {
          log.Debug("End without a null");
          endIndex = i + 1;
        }
        else
        {
          // we're not at the end yet
          continue;
        }

        int length = endIndex - startIndex;
        if (length > 0)
        {
          log.Debug("string segment using encoding " + currentEnc.EncodingName);
          sb.Append(currentEnc.GetString(bytes, startIndex, endIndex - startIndex));
        }
        if (done)
        {
          break;
        }
        currentEnc = nextEnc;
        startIndex = endIndex + 2;
      }

      return sb.ToString();
    }

    public static byte[] GetBytes(string str, int length, bool zeroPadded)
    {
      byte[] stringBytes = null;
      if (string.IsNullOrEmpty(str))
      {
        stringBytes = new byte[0];
      }
      else
      {
        //char[] chars = str.ToCharArray();
        List<byte> stringByteList = new List<byte>();
        Encoding current = EncodingHelper.encodingLatin1;
        for (int i = 0; i < str.Length; i++)
        {
          int unicode = (int)str[i];
          if (unicode > 127)
          {
            // need to consider changing encoding for high ASCII bytes only
            Encoding local = EncodingHelper.GetEncoding(unicode);
            if (local != null && local != current)
            {
              switch (local.CodePage)
              {
                case EncodingHelper.LATIN1:
                  current = local;
                  stringByteList.AddRange(new byte[] { (byte)'^', (byte)'L' });
                  break;
                case EncodingHelper.BALTIC:
                  current = local;
                  stringByteList.AddRange(new byte[] { (byte)'^', (byte)'B' });
                  break;
                case EncodingHelper.CYRILLIC:
                  current = local;
                  stringByteList.AddRange(new byte[] { (byte)'^', (byte)'C' });
                  break;
                case EncodingHelper.TURKISH:
                  current = local;
                  stringByteList.AddRange(new byte[] { (byte)'^', (byte)'T' });
                  break;
                case EncodingHelper.JAPANESE:
                  current = local;
                  stringByteList.AddRange(new byte[] { (byte)'^', (byte)'J' });
                  break;
                case EncodingHelper.CENTRAL_EUROPE:
                  current = local;
                  stringByteList.AddRange(new byte[] { (byte)'^', (byte)'E' });
                  break;
                case EncodingHelper.GREEK:
                  current = local;
                  stringByteList.AddRange(new byte[] { (byte)'^', (byte)'G' });
                  break;
              }
            }
          }
          //TODO: get bytes only as encoding changes
          stringByteList.Add(current.GetBytes(new char[] { str[i] })[0]);
        }
        stringBytes = stringByteList.ToArray();
      }
      byte[] bytes = new byte[length];
      if (zeroPadded)
      {
        // if the byte array is zero padded, we need to copy one less byte than our array is long
        length--;
      }
      int j = Math.Min(stringBytes.Length, length);
      for (int i = 0; i < j; i++)
      {
        bytes[i] = stringBytes[i];
      }
      return bytes;
    }

  }
}
