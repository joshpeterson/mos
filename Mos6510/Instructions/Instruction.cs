using Mos6510;
using System;

namespace Mos6510.Instructions
{
  [Flags]
  public enum Result
  {
    Success = 0,
    BranchTaken = 1,
    BranchAcrossPageBoundary = 2,
  }

  public interface Instruction
  {
    Result Execute(ProgrammingModel model, Memory memory, Argument argument);
  }
}
