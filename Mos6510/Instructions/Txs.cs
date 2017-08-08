namespace Mos6510.Instructions
{
  public class Txs : Instruction
  {
    public Result Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      var value = model.GetRegister(RegisterName.X).GetValue();
      model.GetRegister(RegisterName.S).SetValue(value);
      return Result.Success;
    }
  }
}
