namespace Mos6510.Instructions
{
  public class Clc : Instruction
  {
    public Result Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      model.CarryFlag = false;

      return Result.Success;
    }
  }
}
