using Mos6510;

namespace Mos6510.Instructions
{
  public class Inx : Instruction
  {
    public virtual void Execute(ProgrammingModel model, Memory memory,
                                Argument argument)
    {
      RegisterUtils.IncrementRegister(model, RegisterName.X);
    }
  }
}
