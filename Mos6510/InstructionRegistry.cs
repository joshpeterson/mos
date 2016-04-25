using Mos6510.Instructions;
using System.Collections.Generic;

namespace Mos6510
{
  public class InstructionRegistry
  {
    public Registry All = new Registry();

    private Dictionary<Opcode, Instruction> Ins
      = new Dictionary<Opcode, Instruction>
    {
      {Opcode.Inx, new Inx()},
      {Opcode.Iny, new Iny()},
      {Opcode.And, new And()},
      {Opcode.Ora, new Ora()},
    };

    public InstructionRegistry()
    {
      All.Add(0xE0, Opcode.Inx, Ins[Opcode.Inx], AddressingMode.Implied);
      All.Add(0xC0, Opcode.Iny, Ins[Opcode.Iny], AddressingMode.Implied);
      All.Add(0x29, Opcode.And, Ins[Opcode.And], AddressingMode.Immediate);
      All.Add(0x2D, Opcode.And, Ins[Opcode.And], AddressingMode.Absolute);
      All.Add(0x3D, Opcode.And, Ins[Opcode.And], AddressingMode.AbsoluteX);
      All.Add(0x39, Opcode.And, Ins[Opcode.And], AddressingMode.AbsoluteY);
      All.Add(0x25, Opcode.And, Ins[Opcode.And], AddressingMode.Zeropage);
      All.Add(0x35, Opcode.And, Ins[Opcode.And], AddressingMode.ZeropageX);
      All.Add(0x21, Opcode.And, Ins[Opcode.And], AddressingMode.IndexedIndirectX);
      All.Add(0x31, Opcode.And, Ins[Opcode.And], AddressingMode.IndexedIndirectY);
      All.Add(0x09, Opcode.Ora, Ins[Opcode.Ora], AddressingMode.Immediate);
      All.Add(0x0D, Opcode.Ora, Ins[Opcode.Ora], AddressingMode.Absolute);
      All.Add(0x1D, Opcode.Ora, Ins[Opcode.Ora], AddressingMode.AbsoluteX);
      All.Add(0x19, Opcode.Ora, Ins[Opcode.Ora], AddressingMode.AbsoluteY);
      All.Add(0x05, Opcode.Ora, Ins[Opcode.Ora], AddressingMode.Zeropage);
      All.Add(0x15, Opcode.Ora, Ins[Opcode.Ora], AddressingMode.ZeropageX);
      All.Add(0x01, Opcode.Ora, Ins[Opcode.Ora], AddressingMode.IndexedIndirectX);
      All.Add(0x11, Opcode.Ora, Ins[Opcode.Ora], AddressingMode.IndexedIndirectY);
    }
  }
}
