namespace Mos6510.Instructions
{
  public class And : Instruction
  {
    public int Execute(ProgrammingModel model, Memory memory, AddressingMode mode,
                        ushort operand)
    {
      byte andValue = 0;
      int numberOfCycles = 0;
      if (mode == AddressingMode.Immediate)
      {
        andValue = (byte) operand;
        numberOfCycles = 2;
      }
      else if (mode == AddressingMode.Absolute)
      {
        andValue = memory.GetValue(operand);
        numberOfCycles = 4;
      }

      var accumulator = model.GetRegister(RegisterName.A);
      var previousValue = (byte)accumulator.GetValue();
      accumulator.SetValue(previousValue & andValue);

      return numberOfCycles;
    }
  }
}
