using System;

namespace Mos6510.Instructions
{
  public static class ShiftUtils
  {
    public static void Rotate(ProgrammingModel model, Memory memory,
                              Argument argument, Func<int, int> shift,
                              byte carryInputMask, byte carryOutputMask)
    {
      int previousValue;
      int newValue;

      var previousCarryFlagMask = model.CarryFlag ? carryInputMask : 0x00;

      if (argument is AccumulatorArgument)
      {
        var accumulator = model.GetRegister(RegisterName.A);
        previousValue = accumulator.GetValue();
        newValue = shift(previousValue);
        newValue |= previousCarryFlagMask;
        model.GetRegister(RegisterName.A).SetValue(newValue);
      }
      else
      {
        previousValue = argument.value;
        newValue = shift(previousValue);
        newValue |= previousCarryFlagMask;
        memory.SetValue(argument.address, (byte)newValue);
      }

      RegisterUtils.SetZeroFlag(model, (byte)newValue);
      RegisterUtils.SetNegativeFlag(model, (byte)newValue);
      model.CarryFlag = ((byte)previousValue).IsSet(carryOutputMask);
    }

    public struct Values
    {
      public int PreviousValue;
      public int NewValue;
    }

    public static Values Shift(ProgrammingModel model, Memory memory,
                               Argument argument, Func<int, int> shift,
                               byte carryMask)
    {
      var values = new Values();

      if (argument is AccumulatorArgument)
      {
        var accumulator = model.GetRegister(RegisterName.A);
        values.PreviousValue = accumulator.GetValue();
        values.NewValue = shift(values.PreviousValue);
        model.GetRegister(RegisterName.A).SetValue(values.NewValue);
      }
      else
      {
        values.PreviousValue = argument.value;
        values.NewValue = shift(values.PreviousValue);
        memory.SetValue(argument.address, (byte)values.NewValue);
      }

      model.CarryFlag = ((byte)values.PreviousValue).IsSet(carryMask);

      return values;
    }
  }
}
