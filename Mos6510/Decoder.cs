using Mos6510.Instructions;
using System.Collections.Generic;

namespace Mos6510
{
  public struct OpcodeAddressModePair
  {
    public Opcode Opcode;
    public AddressingMode AddressingMode;
  }

  public class Decoder
  {
    private Dictionary<byte, OpcodeAddressModePair> pairs =
      new Dictionary<byte, OpcodeAddressModePair> {
        { 0xE0, new OpcodeAddressModePair { Opcode = Opcode.Inx,
                                            AddressingMode = AddressingMode.Implied} },
        { 0xC0, new OpcodeAddressModePair { Opcode = Opcode.Iny,
                                            AddressingMode = AddressingMode.Implied} },
      };

    public bool TryDecode(byte instruction, out OpcodeAddressModePair pair)
    {
      if (!pairs.ContainsKey(instruction))
      {
        pair = new OpcodeAddressModePair();
        return false;
      }

      pair = pairs[instruction];
      return true;
    }
  }
}
