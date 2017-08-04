namespace Mos6510.Instructions
{
  public class Sty : Instruction
  {
    public Result Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      RegisterUtils.StoreRegister(model, memory, RegisterName.Y, argument.address);

      return Result.Success;
    }
  }
}
