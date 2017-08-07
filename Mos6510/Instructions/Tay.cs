namespace Mos6510.Instructions
{
  public class Tay : Instruction
  {
    public Result Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      RegisterUtils.Copy(model, RegisterName.A, RegisterName.Y);
      return Result.Success;
    }
  }
}
