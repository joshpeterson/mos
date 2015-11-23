using Mos6510;

namespace Mos6510.Instructions
{
  public class Iny : Instruction
  {
    public void Execute(ProgrammingModel model)
    {
      RegisterUtils.IncrementRegister(model, RegisterName.Y);
    }
  }
}
