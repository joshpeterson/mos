using System;

namespace Mos6510
{
  public class Register
  {
    public Register(int numberOfBits)
    {
      length = numberOfBits;
    }

    public int Length
    {
      get { return length; }
    }

    public int GetValue()
    {
      return value;
    }

    public void SetValue(int value)
    {
      this.value = value;
    }

    private readonly int length;
    private int value;
  }
}
