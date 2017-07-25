namespace Mos6510.Instructions
{
  public class Jmp : Instruction
  {
    public Result Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      model.GetRegister(RegisterName.PC).SetValue(argument.address);
      return Result.Success;
    }
  }
}
