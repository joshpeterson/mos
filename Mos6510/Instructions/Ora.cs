namespace Mos6510.Instructions
{
  public class Ora : Instruction
  {
    public Result Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      var accumulator = model.GetRegister(RegisterName.A);
      accumulator.SetValue((byte)accumulator.GetValue() | argument.value);
      RegisterUtils.SetZeroFlag(model, RegisterName.A);
      RegisterUtils.SetNegativeFlag(model, RegisterName.A);

      return Result.Success;
    }
  }
}
