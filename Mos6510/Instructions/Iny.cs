using Mos6510;

namespace Mos6510.Instructions
{
  public class Iny : Instruction
  {
    public virtual Result Execute(ProgrammingModel model, Memory memory,
                                  Argument argument)
    {
      RegisterUtils.IncrementRegister(model, RegisterName.Y);

      return Result.Success;
    }
  }
}
