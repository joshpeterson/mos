namespace Mos6510.Instructions
{
  public class Tax : Instruction
  {
    public Result Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      var value = model.GetRegister(RegisterName.A).GetValue();
      model.GetRegister(RegisterName.X).SetValue(value);

      RegisterUtils.SetNegativeFlag(model, (byte)value);
      RegisterUtils.SetZeroFlag(model, (byte)value);

      return Result.Success;
    }
  }
}
