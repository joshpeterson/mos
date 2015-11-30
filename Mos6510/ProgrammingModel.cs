using System.Collections.Generic;

namespace Mos6510
{
  public enum RegisterName
  {
    A,
    Y,
    X,
    PC,
    S,
    P,
  }

  public class ProgrammingModel
  {
    const int RegisterSize = 8;
    private Dictionary<RegisterName, Register> registers =
      new Dictionary<RegisterName, Register>
      {
        { RegisterName.A, new Register(RegisterSize) },
        { RegisterName.Y, new Register(RegisterSize) },
        { RegisterName.X, new Register(RegisterSize) },
        { RegisterName.PC, new Register(16) },
        { RegisterName.S, new Register(RegisterSize) },
        { RegisterName.P, new Register(RegisterSize) },
      };

    public Register GetRegister(RegisterName name)
    {
      return registers[name];
    }
  }
}
