namespace Mos6510.Instructions
{
  public class Ldx : Instruction
  {
    public Result Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      RegisterUtils.Load(model, RegisterName.X, argument.value);

      return Result.Success;
    }
  }
}
