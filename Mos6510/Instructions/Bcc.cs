using System.Collections.Generic;

namespace Mos6510.Instructions
{
  public class Bcc : Instruction
  {
    public Result Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      if (!model.CarryFlag)
        model.GetRegister(RegisterName.PC).SetValue(argument.address);

      return Result.Success;
    }
  }
}