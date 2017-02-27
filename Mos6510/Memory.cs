namespace Mos6510
{
  public class Memory
  {
    private byte[] values = new byte[ushort.MaxValue];

    public byte GetValue(ushort address)
    {
      return values[address];
    }

    public void SetValue(ushort address, byte value)
    {
      values[address] = value;
    }
  }
}
