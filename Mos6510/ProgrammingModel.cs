using System.Collections.Generic;

namespace Mos6510
{
  public enum RegisterName
  {
    A,
    Y,
    X,
    PCH,
    PCL,
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
        { RegisterName.PCH, new Register(RegisterSize) },
        { RegisterName.PCL, new Register(RegisterSize) },
        { RegisterName.S, new Register(RegisterSize) },
        { RegisterName.P, new Register(RegisterSize) },
      };

    public Register GetRegister(RegisterName name)
    {
      return registers[name];
    }
  }
}
