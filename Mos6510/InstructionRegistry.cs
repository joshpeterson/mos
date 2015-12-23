using Mos6510.Instructions;

namespace Mos6510
{
  public static class InstructionRegistry
  {
    public static Registry All = new Registry
    {
      { 0xE0, Opcode.Inx, new Inx(), AddressingMode.Implied },
      { 0xC0, Opcode.Iny, new Iny(), AddressingMode.Implied },
    };
  }
}
