namespace Mos6510.Instructions
{
  public class Sec : Instruction
  {
    public Result Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      model.CarryFlag = true;
      return Result.Success;
    }
  }
}
