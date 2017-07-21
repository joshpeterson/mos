namespace Mos6510.Instructions
{
  public class Cpy : Instruction
  {
    public Result Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      var register = model.GetRegister(RegisterName.Y);
      var previousValue = (byte)register.GetValue();
      var newValue = (byte)(previousValue - argument.value);

      RegisterUtils.SetZeroFlag(model, newValue);
      RegisterUtils.SetNegativeFlag(model, newValue);
      RegisterUtils.SetCarryFlag(model, previousValue, argument.value);

      return Result.Success;
    }
  }
}
