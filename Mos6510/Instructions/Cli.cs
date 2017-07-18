namespace Mos6510.Instructions
{
  public class Cli : Instruction
  {
    public Result Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      model.IrqDisableFlag = false;

      return Result.Success;
    }
  }
}
