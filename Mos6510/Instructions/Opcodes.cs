namespace Mos6510.Instructions
{
  public enum Opcode
  {
    Adc, // Add Memory to Accumulator with Carry
    And, // "AND" Memory with Accumulator
    Asl, // Shift Left One Bit (Memory or Accumulator)
    Bcc, // Branch on Carry Clear
    Bcs, // Branch on Carry Set
    Beq, // Branch on Result Zero
    Bit, // Test Bits in Memory with Accumulator
    Bmi, // Branch on Result Minus
    Bne, // Branch on Result not Zero
    Bpl, // Branch on Result Plus
    Brk, // Force Break
    Bvc, // Branch on Overflow Clear
    Bvs, // Branch on Overflow Set
    Clc, // Clear Carry Flag
    Cld, // Clear Decimal Mode
    Cli, // Clear interrupt Disable Bit
    Clv, // Clear Overflow Flag
    Cmp, // Compare Memory and Accumulator
    Cpx, // Compare Memory and Index X
    Cpy, // Compare Memory and Index Y
    Dec, // Decrement Memory by One
    Dex, // Decrement Index X by One
    Dey, // Decrement Index Y by One
    Eor, // "Exclusive-Or" Memory with Accumulator
    Inc, // Increment Memory by One
    Inx, // Increment Index X by One
    Iny, // Increment Index Y by One
    Jmp, // Jump to New Location
    Jsr, // Jump to New Location Saving Return Address
    Lda, // Load Accumulator with Memory
    Ldx, // Load Index X with Memory
    Ldy, // Load Index Y with Memory
    Lsr, // Shift Right One Bit (Memory or Accumulator)
    Nop, // No Operation
    Ora, // "OR" Memory with Accumulator
    Pha, // Push Accumulator on Stack
    Php, // Push Processor Status on Stack
    Pla, // Pull Accumulator from Stack
    Plp, // Pull Processor Status from Stack
    Rol, // Rotate One Bit Left (Memory or Accumulator)
    Ror, // Rotate One Bit Right (Memory or Accumulator)
    Rti, // Return from Interrupt
    Rts, // Return from Subroutine
    Sbc, // Subtract Memory from Accumulator with Borrow
    Sec, // Set Carry Flag
    Sed, // Set Decimal Mode
    Sei, // Set Interrupt Disable Status
    Sta, // Store Accumulator in Memory
    Stx, // Store Index X in Memory
    Sty, // Store Index Y in Memory
    Tax, // Transfer Accumulator to Index X
    Tay, // Transfer Accumulator to Index Y
    Tsx, // Transfer Stack Pointer to Index X
    Txa, // Transfer Index X to Accumulator
    Txs, // Transfer Index X to Stack Pointer
    Tya, // Transfer Index Y to Accumulator
  }
}
