using System.Collections;
using System.Collections.Generic;

namespace Mos6510.Instructions
{
  public class Registry : IEnumerable
  {
    private Dictionary<Opcode, Instruction> _instructions =
      new Dictionary<Opcode, Instruction>();

    public Instruction Get(Opcode opcode)
    {
      if (_instructions.ContainsKey(opcode))
        return _instructions[opcode];
      return null;
    }

    // This method is necessary to get dictionary-style initialization.
    public void Add(Opcode opcode, Instruction instruction)
    {
      _instructions.Add(opcode, instruction);
    }

    // This method is necessary to get dictionary-style initialization.
    IEnumerator IEnumerable.GetEnumerator()
    {
      return _instructions.GetEnumerator();
    }
  }
}
