namespace Mos6510.Instructions
{
  public class Tsx : Instruction
  {
    public Result Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      RegisterUtils.Copy(model, RegisterName.S, RegisterName.X);
      return Result.Success;
    }
  }
}
