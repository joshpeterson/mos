namespace Mos6510.Instructions
{
  public class Cpy : Instruction
  {
    public Result Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      return CompareUtils.Compare(RegisterName.Y, model, memory, argument);
    }
  }
}
