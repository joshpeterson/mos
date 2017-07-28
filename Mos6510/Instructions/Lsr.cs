namespace Mos6510.Instructions
{
  public class Lsr : Instruction
  {
    public Result Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      int previousValue;
      int newValue;

      var previousZeroFlagMask = model.ZeroFlag ? 0x80 : 0x00;

      if (argument is AccumulatorArgument)
      {
        var accumulator = model.GetRegister(RegisterName.A);
        previousValue = accumulator.GetValue();
        newValue = ShiftRight(previousValue);
        newValue |= previousZeroFlagMask;
        model.GetRegister(RegisterName.A).SetValue(newValue);
      }
      else
      {
        previousValue = argument.value;
        newValue = ShiftRight(previousValue);
        newValue |= previousZeroFlagMask;
        memory.SetValue(argument.address, (byte)newValue);
      }

      model.NegativeFlag = false;
      RegisterUtils.SetZeroFlag(model, (byte)previousValue);
      model.CarryFlag = ((byte)previousValue).IsSet(0x01);

      return Result.Success;
    }

    private int ShiftRight(int value)
    {
      return value >> 1;
    }
  }
}
