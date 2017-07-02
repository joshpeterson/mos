using System.Collections.Generic;

namespace Mos6510.Instructions
{
  public class Ldy : Instruction
  {
    public Result Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      RegisterUtils.LoadRegister(model, RegisterName.Y, argument.value);

      return Result.Success;
    }
  }
}
