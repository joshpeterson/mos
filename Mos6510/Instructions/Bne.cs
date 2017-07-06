namespace Mos6510.Instructions
{
  public class Bne : Instruction
  {
    public Result Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      return BranchUtils.BranchIf(m => !m.ZeroFlag, model, argument);
    }
  }
}
