namespace Mos6510.Instructions
{
  public class And : Instruction
  {
    public int Execute(ProgrammingModel model, AddressingMode mode, ushort operand)
    {
      var accumulator = model.GetRegister(RegisterName.A);
      var previousValue = (byte)accumulator.GetValue();
      accumulator.SetValue(previousValue & (byte)operand);

      return 2;
    }
  }
}
