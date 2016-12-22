using System;
using System.Collections;

namespace Mos6510
{
  public class Register
  {
    private BitArray bits;

    public Register(int numberOfBits)
    {
      bits = new BitArray(numberOfBits);
    }

    public int Length
    {
      get { return bits.Length; }
    }

    public int GetValue()
    {
      return ToInt();
    }

    public void SetValue(int value)
    {
      FromInt(value);
    }

    private int ToInt()
    {
      int[] array = new int[1];
      bits.CopyTo(array, 0);
      return array[0];
    }

    private void FromInt(int value)
    {
      var inputBits = new BitArray(new[] { value });
      for (var i = 0; i < bits.Length; ++i)
        bits[i] = inputBits[i];
    }
  }
}
