namespace Mos6510.Instructions
{
  public class Rol : Instruction
  {
    public Result Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      int previousValue;
      int newValue;

      var previousCarryFlagMask = model.CarryFlag ? 0x01 : 0x00;

      if (argument is AccumulatorArgument)
      {
        var accumulator = model.GetRegister(RegisterName.A);
        previousValue = accumulator.GetValue();
        newValue = ShiftLeft(previousValue);
        newValue |= previousCarryFlagMask;
        model.GetRegister(RegisterName.A).SetValue(newValue);
      }
      else
      {
        previousValue = argument.value;
        newValue = ShiftLeft(previousValue);
        newValue |= previousCarryFlagMask;
        memory.SetValue(argument.address, (byte)newValue);
      }

      RegisterUtils.SetZeroFlag(model, (byte)newValue);
      RegisterUtils.SetNegativeFlag(model, (byte)newValue);
      model.CarryFlag = ((byte)previousValue).IsSet(0x80);

      return Result.Success;
    }

    private int ShiftLeft(int value)
    {
      return value << 1;
    }
  }
}
