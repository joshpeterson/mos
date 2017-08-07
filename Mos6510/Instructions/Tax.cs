namespace Mos6510.Instructions
{
  public class Tax : Instruction
  {
    public Result Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      RegisterUtils.Copy(model, RegisterName.A, RegisterName.X);
      return Result.Success;
    }
  }
}
