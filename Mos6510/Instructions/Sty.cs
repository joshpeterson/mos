namespace Mos6510.Instructions
{
  public class Sty : Instruction
  {
    public Result Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      var value = (byte)model.GetRegister(RegisterName.Y).GetValue();
      memory.SetValue(argument.address, value);

      return Result.Success;
    }
  }
}
