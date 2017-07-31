namespace Mos6510.Instructions
{
  public class Pha : Instruction
  {
    public Result Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      RegisterUtils.Push(model, memory, RegisterName.A);
      return Result.Success;
    }
  }
}
