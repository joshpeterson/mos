namespace Mos6510.Instructions
{
  public class Lda : Instruction
  {
    public Result Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      RegisterUtils.LoadRegister(model, RegisterName.A, argument.value);

      return Result.Success;
    }
  }
}
