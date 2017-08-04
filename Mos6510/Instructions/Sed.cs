namespace Mos6510.Instructions
{
  public class Sed : Instruction
  {
    public Result Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      model.DecimalModeFlag = true;
      return Result.Success;
    }
  }
}
