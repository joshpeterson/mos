using Mos6510.Instructions;
using System.Collections.Generic;

namespace Mos6510
{
  public class Decoder
  {
    private readonly Registry registry;

    public Decoder(Registry registry)
    {
      this.registry = registry;
    }

    public bool TryDecode(byte instruction, out OpcodeData data)
    {
      data = registry.Get(instruction);
      return data != null;
    }
  }
}
