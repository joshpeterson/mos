namespace Mos6510.Instructions
{
  public class Stx : Instruction
  {
    public Result Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      var value = (byte)model.GetRegister(RegisterName.X).GetValue();
      memory.SetValue(argument.address, value);

      return Result.Success;
    }
  }
}
