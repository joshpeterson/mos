using System;
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

    public byte GetByteCode(string line)
    {
      Opcode opcode;
      if (Utils.TryParseOpcode(line, out opcode))
        return registry.Get(opcode, AddressingMode.Implied);

      return 0x00;
    }
  }
}
