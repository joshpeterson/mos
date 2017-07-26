namespace Mos6510
{
  public static class Stack
  {
    private const ushort stackOffset = 0x100;

    public static void Push(ProgrammingModel model, Memory memory, byte value)
    {
      var s = model.GetRegister(RegisterName.S);
      var stackPointer = s.GetValue();
      memory.SetValue((ushort)(stackOffset + stackPointer), value);
      stackPointer--;
      s.SetValue(stackPointer);
    }

    public static void Pop(ProgrammingModel model, Memory memory)
    {
      var s = model.GetRegister(RegisterName.S);
      var stackPointer = s.GetValue();
      stackPointer++;
      s.SetValue(stackPointer);
    }

    public static byte Top(ProgrammingModel model, Memory memory)
    {
      var s = model.GetRegister(RegisterName.S);
      var stackPointer = s.GetValue();
      return memory.GetValue((ushort)(stackOffset + stackPointer + 1));
    }
  }
}
