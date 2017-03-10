using System.Collections;
using System.Collections.Generic;

namespace Mos6510.Instructions
{
  public sealed class Registry : IEnumerable
  {
    private Dictionary<Opcode, Instruction> instructions =
      new Dictionary<Opcode, Instruction>();

    private Dictionary<byte, OpcodeData> opcodeData =
      new Dictionary<byte, OpcodeData>();

    public Instruction Get(Opcode opcode)
    {
      if (instructions.ContainsKey(opcode))
        return instructions[opcode];
      return null;
    }

    public OpcodeData Get(byte code)
    {
      if (opcodeData.ContainsKey(code))
        return opcodeData[code];
      return null;
    }

    public byte Get(Opcode opcode, AddressingMode mode)
    {
      foreach (var entry in opcodeData)
        if (entry.Value.Opcode == opcode && entry.Value.Mode == mode)
          return entry.Key;

      return 0x00;
    }

    // This method is necessary to get dictionary-style initialization.
    public void Add(byte code, Opcode opcode, Instruction instruction,
                    AddressingMode mode, int cycles)
    {
      if (!instructions.ContainsKey(opcode))
        instructions.Add(opcode, instruction);
      opcodeData.Add(code, new OpcodeData { Opcode = opcode,
                                            Mode = mode,
                                            Cycles = cycles
                                          });
    }

    // This method is necessary to get dictionary-style initialization.
    IEnumerator IEnumerable.GetEnumerator()
    {
      return instructions.GetEnumerator();
    }
  }
}
