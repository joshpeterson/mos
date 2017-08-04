namespace Mos6510.Instructions
{
  public class Sei : Instruction
  {
    public Result Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      model.IrqDisableFlag = true;
      return Result.Success;
    }
  }
}
