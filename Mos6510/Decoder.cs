using Mos6510.Instructions;
using System.Collections.Generic;

namespace Mos6510
{
  public struct OpcodeAddressModePair
  {
    public Opcode Opcode;
    public AddressingMode Mode;
  }

  public class Decoder
  {
    private Dictionary<byte, OpcodeAddressModePair> pairs =
      new Dictionary<byte, OpcodeAddressModePair> {
        { 0xC0, new OpcodeAddressModePair { Opcode = Opcode.Iny,
                                            Mode = AddressingMode.Implied} },
        { 0xE0, new OpcodeAddressModePair { Opcode = Opcode.Inx,
                                            Mode = AddressingMode.Implied} },
        { 0xEA, new OpcodeAddressModePair { Opcode = Opcode.Nop,
                                            Mode = AddressingMode.Implied} },
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
