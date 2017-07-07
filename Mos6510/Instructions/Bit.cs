namespace Mos6510.Instructions
{
  public class Bit : Instruction
  {
    public Result Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      var value = memory.GetValue(argument.address);
      var accumulator = model.GetRegister(RegisterName.A);

      model.NegativeFlag = value.IsSet(0x80);
      model.OverflowFlag = value.IsSet(0x40);
      model.ZeroFlag = (value & accumulator.GetValue()) == 0;

      return Result.Success;
    }
  }
}
