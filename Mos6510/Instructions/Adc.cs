using System.Collections.Generic;

namespace Mos6510.Instructions
{
  public class Adc : Instruction
  {
    private static Dictionary<AddressingMode, int> numberOfCycles =
    new Dictionary<AddressingMode, int> {
      { AddressingMode.Immediate, 2 },
      { AddressingMode.Absolute, 4 },
      { AddressingMode.AbsoluteX, 4 },
      { AddressingMode.AbsoluteY, 4 },
      { AddressingMode.Zeropage, 3 },
      { AddressingMode.ZeropageX, 4 },
      { AddressingMode.ZeropageY, 4 },
      { AddressingMode.IndexedIndirectX, 6 },
      { AddressingMode.IndexedIndirectY, 5 },
    };

    public void Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      var status = model.GetRegister(RegisterName.S);
      byte carry = (byte)(status.GetValue() & 0x01);
      var accumulator = model.GetRegister(RegisterName.A);
      var previousValue = (byte)accumulator.GetValue();
      accumulator.SetValue(previousValue + argument.value + carry);

      RegisterUtils.SetZeroFlag(model, RegisterName.A);
      RegisterUtils.SetNegativeFlag(model, RegisterName.A);
      RegisterUtils.SetOverflowFlag(model, previousValue);
      RegisterUtils.SetCarryFlag(model, previousValue);
    }

    public int CyclesFor(AddressingMode mode)
    {
      return numberOfCycles[mode];
    }
  }
}
