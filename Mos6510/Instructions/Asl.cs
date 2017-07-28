namespace Mos6510.Instructions
{
  public class Asl : Instruction
  {
    public Result Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      int previousValue;
      int newValue;

      var previousZeroFlagMask = model.ZeroFlag ? 0x01 : 0x00;

      if (argument is AccumulatorArgument)
      {
        var accumulator = model.GetRegister(RegisterName.A);
        previousValue = accumulator.GetValue();
        newValue = ShiftLeft(previousValue);
        newValue |= previousZeroFlagMask;
        model.GetRegister(RegisterName.A).SetValue(newValue);
      }
      else
      {
        previousValue = argument.value;
        newValue = ShiftLeft(previousValue);
        newValue |= previousZeroFlagMask;
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
