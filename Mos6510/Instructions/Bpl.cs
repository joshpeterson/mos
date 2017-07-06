namespace Mos6510.Instructions
{
  public class Bpl : Instruction
  {
    public Result Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      return BranchUtils.BranchIf(m => !m.NegativeFlag, model, argument);
    }
  }
}
