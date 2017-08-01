namespace Mos6510.Instructions
{
  public class Lsr : Instruction
  {
    public Result Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      int previousValue;
      int newValue;

      if (argument is AccumulatorArgument)
      {
        var accumulator = model.GetRegister(RegisterName.A);
        previousValue = accumulator.GetValue();
        newValue = ShiftRight(previousValue);
        model.GetRegister(RegisterName.A).SetValue(newValue);
      }
      else
      {
        previousValue = argument.value;
        newValue = ShiftRight(previousValue);
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
