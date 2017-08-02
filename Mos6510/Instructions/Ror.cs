namespace Mos6510.Instructions
{
  public class Ror : Instruction
  {
    public Result Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      int previousValue;
      int newValue;

      var previousCarryFlagMask = model.CarryFlag ? 0x80 : 0x00;

      if (argument is AccumulatorArgument)
      {
        var accumulator = model.GetRegister(RegisterName.A);
        previousValue = accumulator.GetValue();
        newValue = ShiftRight(previousValue);
        newValue |= previousCarryFlagMask;
        model.GetRegister(RegisterName.A).SetValue(newValue);
      }
      else
      {
        previousValue = argument.value;
        newValue = ShiftRight(previousValue);
        newValue |= previousCarryFlagMask;
        memory.SetValue(argument.address, (byte)newValue);
      }

      RegisterUtils.SetZeroFlag(model, (byte)newValue);
      RegisterUtils.SetNegativeFlag(model, (byte)newValue);
      model.CarryFlag = ((byte)previousValue).IsSet(0x01);

      return Result.Success;
    }

    private int ShiftRight(int value)
    {
      return value >> 1;
    }
  }
}
