namespace Mos6510.Instructions
{
  public class Jsr : Instruction
  {
    public Result Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      var pc = model.GetRegister(RegisterName.PC);

      var newValue = (ushort)(pc.GetValue() + 2);
      var highByte = (byte)(newValue >> 8);
      var lowByte = (byte)(newValue & 0xFF);

      Stack.Push(model, memory, highByte);
      Stack.Push(model, memory, lowByte);

      pc.SetValue(argument.address);

      return Result.Success;
    }
  }
}
