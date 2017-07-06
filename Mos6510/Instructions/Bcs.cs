using System;

namespace Mos6510.Instructions
{
  public class Bcs : Instruction
  {
    public Result Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      return BranchUtils.BranchIf(m => m.CarryFlag, model, argument);
    }
  }
}
