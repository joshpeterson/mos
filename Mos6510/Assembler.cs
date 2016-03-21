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
      var tokens = line.Split(new [] {' '});
      var disassembly = new List<byte>();

      Opcode opcode;
      if (Utils.TryParseOpcode(tokens[0], out opcode))
        disassembly.Add(registry.Get(opcode, AddressingMode.Implied));

      for (var i = 1; i < tokens.Length; ++i)
      {
        var token = tokens[i];
        if (IsLiteral(token))
        {
          byte result;
          if (TryParseLiteral(token, out result))
            disassembly.Add(result);
        }
      }

      return disassembly;
    }

    private bool TryParseLiteral(string token, out byte result)
    {
      token = token.Substring(1);
      var style = NumberStyles.None;
      if (IsHexValue(token))
      {
        token = token.Substring(1);
        style = NumberStyles.HexNumber;
      }

      return Byte.TryParse(token, style, null, out result);
    }

    private static bool IsHexValue(string token)
    {
      return token.StartsWith("$");
    }

    private static bool IsLiteral(string token)
    {
      return token.StartsWith("#");
    }
  }
}
