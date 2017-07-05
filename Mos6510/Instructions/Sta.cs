namespace Mos6510.Instructions
{
  public class Sta : Instruction
  {
    public Result Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      var value = (byte)model.GetRegister(RegisterName.A).GetValue();
      memory.SetValue(argument.address, value);

      return Result.Success;
    }
  }
}
