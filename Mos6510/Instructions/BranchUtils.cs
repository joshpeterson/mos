using System;

namespace Mos6510.Instructions
{
  public static class BranchUtils
  {
    public static Result BranchIf(Func<ProgrammingModel, bool> condition,
                                  ProgrammingModel model, Argument argument)
    {
      Result result = Result.Success;
      if (condition(model))
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
