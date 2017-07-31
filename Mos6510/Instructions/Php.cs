namespace Mos6510.Instructions
{
  public class Php : Instruction
  {
    public Result Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      RegisterUtils.Push(model, memory, RegisterName.P);
      return Result.Success;
    }
  }
}
