using Mos6510.Instructions;

namespace Mos6510
{
  public static class InstructionRegistry
  {
    public static Registry All = new Registry
    {
      { 0xE0, Opcode.Inx, new Inx(), AddressingMode.Implied },
      { 0xC0, Opcode.Iny, new Iny(), AddressingMode.Implied },
      { 0x29, Opcode.And, new And(), AddressingMode.Immediate },
      { 0x2D, Opcode.And, new And(), AddressingMode.Absolute },
      { 0x3D, Opcode.And, new And(), AddressingMode.AbsoluteX },
      { 0x39, Opcode.And, new And(), AddressingMode.AbsoluteY },
      { 0x25, Opcode.And, new And(), AddressingMode.Zeropage },
      { 0x35, Opcode.And, new And(), AddressingMode.ZeropageX },
      { 0x21, Opcode.And, new And(), AddressingMode.IndexedIndirectX },
      { 0x31, Opcode.And, new And(), AddressingMode.IndexedIndirectY },
    };
  }
}
