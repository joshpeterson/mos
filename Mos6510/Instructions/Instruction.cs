using Mos6510;

namespace Mos6510.Instructions
{
  public interface Instruction
  {
    /// <summary>Execute the given instruction.</summary>
    /// <param name="model">The programming model to use.</param>
    /// <returns>The number of cycles required to execute.</returns>
    int Execute(ProgrammingModel model, AddressingMode mode, ushort operand);
  }
}
