namespace Mos6510.Instructions
{
  public class Adc : Instruction
  {
    public Result Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      var status = model.GetRegister(RegisterName.P);
      byte carry = (byte)(status.GetValue() & 0x01);
      var accumulator = model.GetRegister(RegisterName.A);
      var previousValue = (byte)accumulator.GetValue();
      accumulator.SetValue(previousValue + argument.value + carry);

      RegisterUtils.SetZeroFlag(model, RegisterName.A);
      RegisterUtils.SetNegativeFlag(model, RegisterName.A);
      RegisterUtils.SetOverflowFlag(model, previousValue);
      RegisterUtils.SetCarryFlag(model, previousValue);

      return Result.Success;
    }
  }
}
