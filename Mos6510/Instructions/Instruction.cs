using Mos6510;

namespace Mos6510.Instructions
{
  public interface Instruction
  {
    void Execute(ProgrammingModel model);
  }
}
