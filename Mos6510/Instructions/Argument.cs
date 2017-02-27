namespace Mos6510.Instructions
{
  public struct Argument
  {
    public byte value;
    public ushort address;

    public Argument(byte value, ushort address)
    {
      this.value = value;
      this.address = address;
    }
  }
}
