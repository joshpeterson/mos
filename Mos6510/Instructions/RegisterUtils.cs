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

    public static void LoadRegister(ProgrammingModel model, RegisterName name,
                                    byte value)
    {
      model.GetRegister(name).SetValue(value);

      RegisterUtils.SetZeroFlag(model, name);
      RegisterUtils.SetNegativeFlag(model, name);
    }

    public static void StoreRegister(ProgrammingModel model, Memory memory,
                                     RegisterName name, ushort address)
    {
      var value = (byte)model.GetRegister(name).GetValue();
      memory.SetValue(address, value);
    }

    public static void Copy(ProgrammingModel model, RegisterName source,
                            RegisterName destination)
    {
      var value = model.GetRegister(source).GetValue();
      model.GetRegister(destination).SetValue(value);

      RegisterUtils.SetNegativeFlag(model, (byte)value);
      RegisterUtils.SetZeroFlag(model, (byte)value);
    }

    public static void SetZeroFlag(ProgrammingModel model, RegisterName name)
    {
      var register = model.GetRegister(name);
      var value = register.GetValue();
      SetZeroFlag(model, (byte)value);
    }

    public static void SetZeroFlag(ProgrammingModel model, byte value)
    {
      model.ZeroFlag = (sbyte)value == 0;
    }

    public static void SetNegativeFlag(ProgrammingModel model, RegisterName name)
    {
      var register = model.GetRegister(name);
      var value = register.GetValue();
      SetNegativeFlag(model, (byte)value);
    }

    public static void SetNegativeFlag(ProgrammingModel model, byte value)
    {
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

    public static void Push(ProgrammingModel model, Memory memory,
                            RegisterName name)
    {
      Stack.Push(model, memory, (byte)model.GetRegister(name).GetValue());
    }

    public static void Pull(ProgrammingModel model, Memory memory,
                            RegisterName name)
    {
      var value = Stack.Top(model, memory);
      model.GetRegister(name).SetValue(value);
      Stack.Pop(model, memory);
    }
  }
}
