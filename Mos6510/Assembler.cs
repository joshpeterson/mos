using System;
using System.Collections.Generic;
using System.Globalization;
using Mos6510.Instructions;

namespace Mos6510
{
  public class Assembler
  {
    private readonly Registry registry;

    public Assembler(Registry registry)
    {
      this.registry = registry;
    }

    public IEnumerable<byte> GetDisassembly(string line)
    {
      var tokens = line.Split(new [] {' ', ','});
      var mode = AddressingModeFor(tokens);

      var disassembly = new List<byte>();

      Opcode opcode;
      if (Utils.TryParseOpcode(tokens[0], out opcode))
        disassembly.Add(registry.Get(opcode, mode));

      if (tokens.Length > 1)
      {
        var token = StripAbsoluteIdentifier(tokens[1]);

        ushort result;
        if (TryParseNumber(token, out result))
          disassembly.AddRange(ConvertArgumentToBytes(result));
      }

      return disassembly;
    }

    private static AddressingMode AddressingModeFor(string[] tokens)
    {
      var mode = AddressingMode.Implied;
      if (tokens.Length == 3)
      {
        if (tokens[2] == "X" || tokens[2] == "x")
          mode = AddressingMode.AbsoluteX;
        else
          mode = AddressingMode.AbsoluteY;
      }
      else if (tokens.Length == 2)
      {
        if (IsAbsoluteIdentifier(tokens[1]))
          mode = AddressingMode.Immediate;
        else
          mode = AddressingMode.Absolute;
      }

      return mode;
    }

    private static string StripAbsoluteIdentifier(string token)
    {
      if (IsAbsoluteIdentifier(token))
        return token.Substring(1);
      return token;
    }

    private static bool TryParseNumber(string token, out ushort result)
    {
      var style = NumberStyles.None;
      if (IsHexValue(token))
      {
        token = token.Substring(1);
        style = NumberStyles.HexNumber;
      }

      return UInt16.TryParse(token, style, null, out result);
    }

    private static IEnumerable<byte> ConvertArgumentToBytes(ushort argument)
    {
      var bytes = new List<byte>();
      bytes.Add((byte)(argument & 0x00FF));
      if (argument > Byte.MaxValue)
        bytes.Add((byte)((argument & 0xFF00) >> 8));

      return bytes;
    }

    private static bool IsHexValue(string token)
    {
      return token.StartsWith("$");
    }

    private static bool IsAbsoluteIdentifier(string token)
    {
      return token.StartsWith("#");
    }
  }
}
