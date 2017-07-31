namespace Mos6510.Instructions
{
  public class Pha : Instruction
  {
    public Result Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      var value = (byte)model.GetRegister(RegisterName.A).GetValue();
      Stack.Push(model, memory, value);
      return Result.Success;
    }
  }
}
