namespace Mos6510.Instructions
{
  public class Tya : Instruction
  {
    public Result Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      RegisterUtils.Copy(model, RegisterName.Y, RegisterName.A);
      return Result.Success;
    }
  }
}
