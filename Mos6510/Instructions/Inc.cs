namespace Mos6510.Instructions
{
  public class Inc : Instruction
  {
    public Result Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      var value = memory.GetValue(argument.address);
      var newValue = (byte)(value + 1);
      memory.SetValue(argument.address, newValue);

      RegisterUtils.SetNegativeFlag(model, newValue);
      RegisterUtils.SetZeroFlag(model, newValue);

      return Result.Success;
    }
  }
}
