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

    public int Value
    {
      get;
      set;
    }

    private readonly int length;
  }
}
