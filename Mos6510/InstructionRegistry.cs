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
      {Opcode.Eor, new Eor()},
      {Opcode.Adc, new Adc()},
      {Opcode.Clc, new Clc()},
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
      All.Add(0x49, Opcode.Eor, Ins[Opcode.Eor], AddressingMode.Immediate);
      All.Add(0x4D, Opcode.Eor, Ins[Opcode.Eor], AddressingMode.Absolute);
      All.Add(0x5D, Opcode.Eor, Ins[Opcode.Eor], AddressingMode.AbsoluteX);
      All.Add(0x59, Opcode.Eor, Ins[Opcode.Eor], AddressingMode.AbsoluteY);
      All.Add(0x45, Opcode.Eor, Ins[Opcode.Eor], AddressingMode.Zeropage);
      All.Add(0x55, Opcode.Eor, Ins[Opcode.Eor], AddressingMode.ZeropageX);
      All.Add(0x41, Opcode.Eor, Ins[Opcode.Eor], AddressingMode.IndexedIndirectX);
      All.Add(0x51, Opcode.Eor, Ins[Opcode.Eor], AddressingMode.IndexedIndirectY);
      All.Add(0x69, Opcode.Adc, Ins[Opcode.Adc], AddressingMode.Immediate);
      All.Add(0x6D, Opcode.Adc, Ins[Opcode.Adc], AddressingMode.Absolute);
      All.Add(0x7D, Opcode.Adc, Ins[Opcode.Adc], AddressingMode.AbsoluteX);
      All.Add(0x79, Opcode.Adc, Ins[Opcode.Adc], AddressingMode.AbsoluteY);
      All.Add(0x65, Opcode.Adc, Ins[Opcode.Adc], AddressingMode.Zeropage);
      All.Add(0x75, Opcode.Adc, Ins[Opcode.Adc], AddressingMode.ZeropageX);
      All.Add(0x61, Opcode.Adc, Ins[Opcode.Adc], AddressingMode.IndexedIndirectX);
      All.Add(0x71, Opcode.Adc, Ins[Opcode.Adc], AddressingMode.IndexedIndirectY);
      All.Add(0x18, Opcode.Clc, Ins[Opcode.Clc], AddressingMode.Implied);
    }
  }
}
