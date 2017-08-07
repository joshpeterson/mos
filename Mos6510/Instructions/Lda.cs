namespace Mos6510.Instructions
{
  public class Lda : Instruction
  {
    public Result Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      RegisterUtils.Load(model, RegisterName.A, argument.value);

      return Result.Success;
    }
  }
}
