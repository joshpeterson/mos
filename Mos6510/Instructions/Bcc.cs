using System;

namespace Mos6510.Instructions
{
  public class Bcc : Instruction
  {
    public Result Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      Result result = Result.Success;
      if (!model.CarryFlag)
      {
        var pc = model.GetRegister(RegisterName.PC);
        var previousValue = pc.GetValue();
        pc.SetValue(argument.address);
        result |= Result.BranchTaken;
        if (Math.Abs(previousValue - (int)argument.address) > 256)
          result |= Result.BranchAcrossPageBoundary;
      }

      return result;
    }
  }
}
