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

      SetZeroFlag(model, name);
      SetNegativeFlag(model, name);
    }

    public static void SetZeroFlag(ProgrammingModel model, RegisterName name)
    {
      var register = model.GetRegister(name);
      var value = register.GetValue();

      model.ZeroFlag = (sbyte)value == 0;
    }

    public static void SetNegativeFlag(ProgrammingModel model, RegisterName name)
    {
      var register = model.GetRegister(name);
      var value = register.GetValue();

      model.NegativeFlag = (sbyte)value < 0;
    }

    public static void SetOverflowFlag(ProgrammingModel model, byte previousValue)
    {
      var currentValue = model.GetRegister(RegisterName.A).GetValue();
      if (((previousValue & 0x80) == 0 && (currentValue & 0x80) != 0) ||
          ((previousValue & 0x80) != 0 && (currentValue & 0x80) == 0))
        model.OverflowFlag = true;
      else
        model.OverflowFlag = false;
    }

    public static void SetCarryFlag(ProgrammingModel model, byte previousValue)
    {
      var currentValue = model.GetRegister(RegisterName.A).GetValue();
      model.CarryFlag = currentValue < previousValue;
    }

    public static void SetCarryFlag(ProgrammingModel model, byte previousValue,
                                    byte argument)
    {
      model.CarryFlag = previousValue < argument;
    }
  }
}
