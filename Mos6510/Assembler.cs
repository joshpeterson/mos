using Mos6510.Instructions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

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
      var hasParantheses = line.Contains("(") && line.Contains(")");
      line = RemoveParantheses(line);

      var tokens = line.Split(new [] {' ', ','});

      IEnumerable<byte> arguments = Enumerable.Empty<byte>();
      if (tokens.Length > 1)
      {
        var token = StripAbsoluteIdentifier(tokens[1]);

        ushort result;
        if (token == "A")
          arguments = new byte[] { 0 };
        else if (TryParseNumber(token, out result))
          arguments = ConvertArgumentToBytes(result);
        else
          return Enumerable.Empty<byte>();
      }

      var mode = AddressingModeFor(tokens, arguments.Count(), hasParantheses);

      var disassembly = new List<byte>();

      Opcode opcode;
      if (Utils.TryParseOpcode(TitleCaseFor(tokens[0]), out opcode))
      {
        disassembly.Add(registry.Get(opcode, mode));
        disassembly.AddRange(arguments);
      }

      return disassembly;
    }

    private static AddressingMode AddressingModeFor(string[] tokens,
        int numberOfArguments,
        bool hasParantheses)
    {
      var mode = AddressingMode.Implied;
      if (tokens.Length == 3)
      {
        if (tokens[2] == "X" || tokens[2] == "x")
          if (numberOfArguments == 1)
            if (hasParantheses)
              mode = AddressingMode.IndirectX;
            else
              mode = AddressingMode.ZeropageX;
          else
            mode = AddressingMode.AbsoluteX;
        else if (numberOfArguments == 1)
          if (hasParantheses)
            mode = AddressingMode.IndirectY;
          else
            mode = AddressingMode.ZeropageY;
        else
          mode = AddressingMode.AbsoluteY;
      }
      else if (tokens.Length == 2)
      {
        if (IsAbsoluteIdentifier(tokens[1]))
          mode = AddressingMode.Immediate;
        else if (IsAccumulatorIdentifier(tokens[1]))
          mode = AddressingMode.Accumulator;
        else if (numberOfArguments == 1)
          mode = AddressingMode.Zeropage;
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

    private static bool IsAccumulatorIdentifier(string token)
    {
      return token == "A";
    }

    private static string RemoveParantheses(string line)
    {
      line = line.Replace("(", string.Empty);
      line = line.Replace(")", string.Empty);

      return line;
    }

    private static string TitleCaseFor(string input)
    {
      var chars = input.ToLower().ToCharArray();
      chars[0] = char.ToUpper(chars[0]);

      return new string(chars);
    }
  }
}
