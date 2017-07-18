namespace Mos6510.Instructions
{
  public class Cld : Instruction
  {
    public Result Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      model.DecimalModeFlag = false;

      return Result.Success;
    }
  }
}
