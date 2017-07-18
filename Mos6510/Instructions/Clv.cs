namespace Mos6510.Instructions
{
  public class Clv : Instruction
  {
    public Result Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      model.OverflowFlag = false;

      return Result.Success;
    }
  }
}
