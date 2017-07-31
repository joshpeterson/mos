namespace Mos6510.Instructions
{
  public class Pla : Instruction
  {
    public Result Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      RegisterUtils.Pull(model, memory, RegisterName.A);
      return Result.Success;
    }
  }
}
