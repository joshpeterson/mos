namespace Mos6510.Instructions
{
  public class Php : Instruction
  {
    public Result Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      var value = (byte)model.GetRegister(RegisterName.P).GetValue();
      Stack.Push(model, memory, value);
      return Result.Success;
    }
  }
}
