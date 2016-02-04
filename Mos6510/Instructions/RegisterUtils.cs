using Mos6510;

namespace Mos6510.Instructions
{
  public static class RegisterUtils
  {
    public static void IncrementRegister(ProgrammingModel model, RegisterName name)
    {
      var register = model.GetRegister(name);
      var previousValue = register.GetValue();
      var newValue = previousValue + 1;
      register.SetValue(newValue);

      model.NegativeFlag = (sbyte)newValue < 0;
      model.ZeroFlag = (sbyte)newValue == 0;
    }
  }
}
