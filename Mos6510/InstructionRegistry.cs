using Mos6510.Instructions;

namespace Mos6510
{
  public static class InstructionRegistry
  {
    public static Registry All = new Registry
    {
      { 0xE0, Opcode.Inx, null, AddressingMode.Implied }
    };
  }
}
