namespace Mos6510.Instructions
{
  public class Stx : Instruction
  {
    public Result Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      RegisterUtils.StoreRegister(model, memory, RegisterName.X, argument.address);

      return Result.Success;
    }
  }
}
