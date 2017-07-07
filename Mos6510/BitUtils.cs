public static class BitUtils
{
  public static bool IsSet(this byte value, byte mask)
  {
    return (value & mask) == mask;
  }
}
