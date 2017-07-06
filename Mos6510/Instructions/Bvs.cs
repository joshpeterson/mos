namespace Mos6510.Instructions
{
  public class Bvs : Instruction
  {
    public Result Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      return BranchUtils.BranchIf(m => m.OverflowFlag, model, argument);
    }
  }
}
