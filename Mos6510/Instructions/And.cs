namespace Mos6510.Instructions
{
  public class And : Instruction
  {
    public int Execute(ProgrammingModel model, Memory memory, AddressingMode mode,
                        ushort operand)
    {
      byte andValue = 0;
      int numberOfCycles = 0;
      if (mode == AddressingMode.Immediate)
      {
        andValue = (byte) operand;
        numberOfCycles = 2;
      }
      else if (mode == AddressingMode.Absolute)
      {
        andValue = memory.GetValue(operand);
        numberOfCycles = 4;
      }
      else if (mode == AddressingMode.AbsoluteX)
      {
        var x = model.GetRegister(RegisterName.X).GetValue();
        var address = (ushort)(operand + x);
        andValue = memory.GetValue(address);

        numberOfCycles = 4;
        if ((operand & 0xFF00) != (address & 0xFF00))
          numberOfCycles++;
      }
      else if (mode == AddressingMode.AbsoluteY)
      {
        var y = model.GetRegister(RegisterName.Y).GetValue();
        var address = (ushort)(operand + y);
        andValue = memory.GetValue(address);

        numberOfCycles = 4;
        if ((operand & 0xFF00) != (address & 0xFF00))
          numberOfCycles++;
      }
      else if (mode == AddressingMode.Zeropage)
      {
        andValue = memory.GetValue(operand);
        numberOfCycles = 3;
      }
      else if (mode == AddressingMode.ZeropageX)
      {
        var x = model.GetRegister(RegisterName.X).GetValue();
        var address = (ushort)(operand + x);
        andValue = memory.GetValue(address);

        numberOfCycles = 4;
      }
      else if (mode == AddressingMode.ZeropageY)
      {
        var y = model.GetRegister(RegisterName.Y).GetValue();
        var address = (ushort)(operand + y);
        andValue = memory.GetValue(address);

        numberOfCycles = 4;
      }
      else if (mode == AddressingMode.IndexedIndirectX)
      {
        var x = model.GetRegister(RegisterName.X).GetValue();
        var zeroPageAddress = (ushort)(operand + x);
        var effectiveAddressLow = memory.GetValue(zeroPageAddress);
        var effectiveAddressHi = memory.GetValue((ushort)(zeroPageAddress + 1));
        var effectiveAddress = (ushort)(effectiveAddressHi << 8 |
                                        effectiveAddressLow);
        andValue = memory.GetValue(effectiveAddress);

        numberOfCycles = 6;
      }
      else if (mode == AddressingMode.IndexedIndirectY)
      {
        var y = model.GetRegister(RegisterName.Y).GetValue();
        var zeroPageAddress = (ushort)(operand + y);
        var effectiveAddressLow = memory.GetValue(zeroPageAddress);
        var effectiveAddressHi = memory.GetValue((ushort)(zeroPageAddress + 1));
        var effectiveAddress = (ushort)(effectiveAddressHi << 8 |
                                        effectiveAddressLow);
        andValue = memory.GetValue(effectiveAddress);

        numberOfCycles = 5;
      }

      var accumulator = model.GetRegister(RegisterName.A);
      var previousValue = (byte)accumulator.GetValue();
      accumulator.SetValue(previousValue & andValue);

      return numberOfCycles;
    }
  }
}
