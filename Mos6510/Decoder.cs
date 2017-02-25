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

    public bool TryDecode(byte instruction, out OpcodeAddressModePair pair)
    {
        pair = registry.Get(instruction);
        return pair != null;
    }
}
}
