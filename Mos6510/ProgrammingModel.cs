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
    private const byte CarryFlagMask = 0x01;
    private const byte ZeroFlagMask = 0x02;
    private const byte IrqDisableFlagMask = 0x04;
    private const byte DecimalModeFlagMask = 0x08;
    private const byte BreakCommandFlagMask = 0x10;
    private const byte OverflowFlagMask = 0x40;
    private const byte NegativeFlagMask = 0x80;

    private const int RegisterSize = 8;

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

    public ProgrammingModel()
    {
      // Set the intial value to 00100000, with the unnused value at 1
      // and the rest at zero.
      GetRegister(RegisterName.P).SetValue(0x20);
      GetRegister(RegisterName.S).SetValue(0xFF);
    }

    public Register GetRegister(RegisterName name)
    {
      return registers[name];
    }

    public bool CarryFlag
    {
      get {
        return CheckStatusRegisterFlag(CarryFlagMask);
      }
      set {
        UpdateStatusRegisterFlag(CarryFlagMask, value);
      }
    }

    public bool ZeroFlag
    {
      get {
        return CheckStatusRegisterFlag(ZeroFlagMask);
      }
      set {
        UpdateStatusRegisterFlag(ZeroFlagMask, value);
      }
    }

    public bool IrqDisableFlag
    {
      get {
        return CheckStatusRegisterFlag(IrqDisableFlagMask);
      }
      set {
        UpdateStatusRegisterFlag(IrqDisableFlagMask, value);
      }
    }

    public bool DecimalModeFlag
    {
      get {
        return CheckStatusRegisterFlag(DecimalModeFlagMask);
      }
      set {
        UpdateStatusRegisterFlag(DecimalModeFlagMask, value);
      }
    }

    public bool BreakCommandFlag
    {
      get {
        return CheckStatusRegisterFlag(BreakCommandFlagMask);
      }
      set {
        UpdateStatusRegisterFlag(BreakCommandFlagMask, value);
      }
    }

    public bool OverflowFlag
    {
      get {
        return CheckStatusRegisterFlag(OverflowFlagMask);
      }
      set {
        UpdateStatusRegisterFlag(OverflowFlagMask, value);
      }
    }

    public bool NegativeFlag
    {
      get {
        return CheckStatusRegisterFlag(NegativeFlagMask);
      }
      set {
        UpdateStatusRegisterFlag(NegativeFlagMask, value);
      }
    }

    private bool CheckStatusRegisterFlag(byte mask)
    {
      return (GetRegister(RegisterName.P).GetValue() & mask) == mask;
    }

    private void UpdateStatusRegisterFlag(byte mask, bool value)
    {
      var p = GetRegister(RegisterName.P);
      if (value)
        p.SetValue(p.GetValue() | mask);
      else
        p.SetValue(p.GetValue() & ~mask);
    }
  }
}
