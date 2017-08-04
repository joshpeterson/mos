namespace Mos6510.Instructions
{
  public class Sta : Instruction
  {
    public Result Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      RegisterUtils.StoreRegister(model, memory, RegisterName.A, argument.address);

      return Result.Success;
    }
  }
}
