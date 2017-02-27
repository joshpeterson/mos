namespace Mos6510.Instructions
{
  public enum AddressingMode
  {
    Implied,
    Immediate,
    Absolute,
    AbsoluteX,
    AbsoluteY,
    Zeropage,
    ZeropageX,
    ZeropageY,
    IndexedIndirectX,
    IndexedIndirectY
  }
}
