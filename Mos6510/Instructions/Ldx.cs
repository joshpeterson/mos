using System.Collections.Generic;

namespace Mos6510.Instructions
{
  public class Ldx : Instruction
  {
    public void Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      RegisterUtils.LoadRegister(model, RegisterName.X, argument.value);
    }
  }
}
