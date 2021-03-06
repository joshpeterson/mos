namespace Mos6510.Instructions
{
  public static class ArgumentUtils
  {
    public static Argument ArgumentFor(ProgrammingModel model, Memory memory,
                                       AddressingMode mode, ushort operand)
    {
      ushort address = 0;
      if (mode == AddressingMode.Accumulator)
        return new AccumulatorArgument();
      else if (mode == AddressingMode.Immediate)
        return new Argument((byte)operand, address);
      else if (mode == AddressingMode.Absolute || mode == AddressingMode.Zeropage)
        address = operand;
      else if (mode == AddressingMode.AbsoluteX || mode == AddressingMode.ZeropageX)
        address = OffsetAddressFor(model, RegisterName.X, operand);
      else if (mode == AddressingMode.AbsoluteY || mode == AddressingMode.ZeropageY)
        address = OffsetAddressFor(model, RegisterName.Y, operand);
      else if (mode == AddressingMode.IndirectX)
        address = IndirectAddressFor(model, memory, RegisterName.X, operand);
      else if (mode == AddressingMode.IndirectY)
        address = IndirectAddressFor(model, memory, RegisterName.Y, operand);

      return new Argument(memory.GetValue(address), address);
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

    private static ushort IndirectAddressFor(ProgrammingModel model,
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
