using Mos6510.Instructions;
using System;

namespace Mos6510
{
  public static class Utils
  {
    public static bool TryParseOpcode(string possibleOpcode, out Opcode opcode)
    {
      opcode = Opcode.Nop;
      foreach (var name in Enum.GetNames(typeof(Opcode)))
      {
        if (name == possibleOpcode)
        {
          opcode = (Opcode)Enum.Parse(typeof(Opcode), name);
          return true;
        }
      }

      return false;
    }
  }
}
