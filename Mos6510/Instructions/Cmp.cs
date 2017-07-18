namespace Mos6510.Instructions
{
  public class Cmp : Instruction
  {
    public Result Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      var accumulator = model.GetRegister(RegisterName.A);
      var previousValue = (byte)accumulator.GetValue();
      var newValue = (byte)(previousValue - argument.value);

      RegisterUtils.SetZeroFlag(model, newValue);
      RegisterUtils.SetNegativeFlag(model, newValue);
      RegisterUtils.SetCarryFlag(model, previousValue, argument.value);

      return Result.Success;
    }
  }
}
