namespace Mos6510.Instructions
{
  public class Cmp : Instruction
  {
    public Result Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      return CompareUtils.Compare(RegisterName.A, model, memory, argument);
    }
  }
}
