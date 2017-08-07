using Mos6510;

namespace Mos6510.Instructions
{
  public class Iny : Instruction
  {
    public Result Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      RegisterUtils.Increment(model, RegisterName.Y);

      return Result.Success;
    }
  }
}
