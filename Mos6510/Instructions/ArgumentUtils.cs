namespace Mos6510.Instructions
{
  public static class ArgumentUtils
  {
    public static byte ArgumentFor(ProgrammingModel model, Memory memory,
                                   AddressingMode mode, ushort operand)
    {
      ushort address = 0;
      if (mode == AddressingMode.Immediate)
        return (byte)operand;
      else if (mode == AddressingMode.Absolute || mode == AddressingMode.Zeropage)
        address = operand;
      else if (mode == AddressingMode.AbsoluteX || mode == AddressingMode.ZeropageX)
        address = OffsetAddressFor(model, RegisterName.X, operand);
      else if (mode == AddressingMode.AbsoluteY || mode == AddressingMode.ZeropageY)
        address = OffsetAddressFor(model, RegisterName.Y, operand);
      else if (mode == AddressingMode.IndexedIndirectX)
        address = IndexedIndirectAddressFor(model, memory, RegisterName.X, operand);
      else if (mode == AddressingMode.IndexedIndirectY)
        address = IndexedIndirectAddressFor(model, memory, RegisterName.Y, operand);

      return memory.GetValue(address);
    }

    public static bool CrossesPageBoundary(ProgrammingModel model,
                                           AddressingMode mode, ushort operand)
    {
      if (mode == AddressingMode.AbsoluteX)
      {
        var address = OffsetAddressFor(model, RegisterName.X, operand);
        return (operand & 0xFF00) != (address & 0xFF00);
      }
      else if (mode == AddressingMode.AbsoluteY)
      {
        var address = OffsetAddressFor(model, RegisterName.Y, operand);
        return (operand & 0xFF00) != (address & 0xFF00);
      }

      return false;
    }

    private static ushort IndexedIndirectAddressFor(ProgrammingModel model,
        Memory memory,
        RegisterName register,
        ushort operand)
    {
      var zeroPageAddress = OffsetAddressFor(model, register, operand);
      var effectiveAddressLow = memory.GetValue(zeroPageAddress);
      var effectiveAddressHi = memory.GetValue((ushort)(zeroPageAddress + 1));
      return (ushort)(effectiveAddressHi << 8 | effectiveAddressLow);
    }

    private static ushort OffsetAddressFor(ProgrammingModel model,
                                           RegisterName register, ushort operand)
    {
      return (ushort)(operand + model.GetRegister(register).GetValue());
    }
  }
}
