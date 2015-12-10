using System.Collections;
using System.Collections.Generic;

namespace Mos6510.Instructions
{
  public class Registry : IEnumerable
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

    // This method is necessary to get dictionary-style initialization.
    public void Add(byte code, Opcode opcode, Instruction instruction,
        AddressingMode mode)
    {
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
