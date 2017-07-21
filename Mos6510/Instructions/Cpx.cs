namespace Mos6510.Instructions
{
  public class Cpx : Instruction
  {
    public Result Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      return CompareUtils.Compare(RegisterName.X, model, memory, argument);
    }
  }
}
