using Mos6510;

namespace Mos6510.Instructions
{
  public class Inx : Instruction
  {
    public virtual void Execute(ProgrammingModel model)
    {
      var registerX = model.GetRegister(RegisterName.X);
      var previousValue = registerX.GetValue();
      registerX.SetValue(previousValue + 1);
    }
  }
}
