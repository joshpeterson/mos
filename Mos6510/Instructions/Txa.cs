namespace Mos6510.Instructions
{
  public class Txa : Instruction
  {
    public Result Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      RegisterUtils.Copy(model, RegisterName.X, RegisterName.A);
      return Result.Success;
    }
  }
}
