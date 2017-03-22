using System.Collections.Generic;

namespace Mos6510.Instructions
{
  public class Asl : Instruction
  {
    public void Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      int previousValue;
      int newValue;

      if (argument is AccumulatorArgument)
      {
        var accumulator = model.GetRegister(RegisterName.A);
        previousValue = accumulator.GetValue();
        newValue = ShiftLeft(previousValue);
        model.GetRegister(RegisterName.A).SetValue(newValue);
      }
      else
      {
        previousValue = argument.value;
        newValue = ShiftLeft(previousValue);
        memory.SetValue(argument.address, (byte)newValue);
      }

      RegisterUtils.SetZeroFlag(model, (byte)newValue);
      RegisterUtils.SetNegativeFlag(model, (byte)newValue);
      model.CarryFlag = ((byte)previousValue & 0x80) == 0x80;
    }

    private int ShiftLeft(int value)
    {
      return value << 1;
    }
  }
}
