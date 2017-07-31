namespace Mos6510.Instructions
{
  public class Plp : Instruction
  {
    public Result Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      RegisterUtils.Pull(model, memory, RegisterName.P);
      return Result.Success;
    }
  }
}
