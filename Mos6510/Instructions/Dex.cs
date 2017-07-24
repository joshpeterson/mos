namespace Mos6510.Instructions
{
  public class Dex : Instruction
  {
    public Result Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      var x = model.GetRegister(RegisterName.X);
      var value = x.GetValue();
      var newValue = (byte)(value - 1);
      x.SetValue(newValue);

      RegisterUtils.SetNegativeFlag(model, newValue);
      RegisterUtils.SetZeroFlag(model, newValue);

      return Result.Success;
    }
  }
}
