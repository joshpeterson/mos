namespace Mos6510.Instructions
{
  public class Dey : Instruction
  {
    public Result Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      var y = model.GetRegister(RegisterName.Y);
      var value = y.GetValue();
      var newValue = (byte)(value - 1);
      y.SetValue(newValue);

      RegisterUtils.SetNegativeFlag(model, newValue);
      RegisterUtils.SetZeroFlag(model, newValue);

      return Result.Success;
    }
  }
}
