using System.Collections;
using System.Collections.Generic;

namespace Mos6510.Instructions
{
  public sealed class Registry : IEnumerable
  {
    private Dictionary<Opcode, Instruction> instructions =
      new Dictionary<Opcode, Instruction>();

    private Dictionary<byte, OpcodeAddressModePair> opcodeAndAddressMode =
      new Dictionary<byte, OpcodeAddressModePair>();

    public Instruction Get(Opcode opcode)
    {
      if (instructions.ContainsKey(opcode))
        return instructions[opcode];
      return null;
    }

    public OpcodeAddressModePair Get(byte code)
    {
      if (opcodeAndAddressMode.ContainsKey(code))
        return opcodeAndAddressMode[code];
      return null;
    }

    public byte Get(Opcode opcode, AddressingMode mode)
    {
      foreach (var entry in opcodeAndAddressMode)
        if (entry.Value.Opcode == opcode && entry.Value.Mode == mode)
          return entry.Key;

      return 0x00;
    }

    // This method is necessary to get dictionary-style initialization.
    public void Add(byte code, Opcode opcode, Instruction instruction,
        AddressingMode mode)
    {
      if (!instructions.ContainsKey(opcode))
        instructions.Add(opcode, instruction);
      opcodeAndAddressMode.Add(code,
          new OpcodeAddressModePair { Opcode = opcode, Mode = mode });
    }

    // This method is necessary to get dictionary-style initialization.
    IEnumerator IEnumerable.GetEnumerator()
    {
      return instructions.GetEnumerator();
    }
  }
}
