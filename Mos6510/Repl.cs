using System;

namespace Mos6510
{
  public class Repl
  {
    private readonly ProgrammingModel model;

    public Repl(ProgrammingModel model, Memory memory)
    {
      this.model = model;
    }

    public string PrintRegisters()
    {
      const string registerFormat = @"
        Registers:
        A:  0x{0:X2}
        Y:  0x{1:X2}
        X:  0x{2:X2}
        PC: 0x{3:X4}
        S:  0x{4:X2}
        P:  {5}b
            NVXBDIZC";

        return string.Format(registerFormat,
          model.GetRegister(RegisterName.A).GetValue(),
          model.GetRegister(RegisterName.Y).GetValue(),
          model.GetRegister(RegisterName.X).GetValue(),
          model.GetRegister(RegisterName.PC).GetValue(),
          model.GetRegister(RegisterName.S).GetValue(),
          Convert.ToString(model.GetRegister(RegisterName.P).GetValue(), 2).
            PadLeft(8, '0'));
    }
  }
}
