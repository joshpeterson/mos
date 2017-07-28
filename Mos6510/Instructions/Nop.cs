namespace Mos6510.Instructions
{
  public class Nop : Instruction
  {
    public Result Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      return Result.Success;
    }
  }
}
