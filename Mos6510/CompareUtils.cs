namespace Mos6510.Instructions
{
  public static class CompareUtils
  {
    public static Result Compare(RegisterName registerName, ProgrammingModel model,
                                 Memory memory, Argument argument)
    {
      var register = model.GetRegister(registerName);
      var previousValue = (byte)register.GetValue();
      var newValue = (byte)(previousValue - argument.value);

      RegisterUtils.SetZeroFlag(model, newValue);
      RegisterUtils.SetNegativeFlag(model, newValue);
      RegisterUtils.SetCarryFlag(model, previousValue, argument.value);

      return Result.Success;
    }
  }
}
