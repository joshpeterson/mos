using Mos6510.Instructions;

namespace Mos6510
{
  public class Executor
  {
    private readonly Registry _registry;
    private readonly ProgrammingModel _model;

    public Executor(Registry registry, ProgrammingModel model, Memory memory)
    {
      _registry = registry;
      _model = model;
    }

    public void Execute(Opcode opcode, AddressingMode mode)
    {
      var instruction = _registry.Get(opcode);
      instruction.Execute(_model);
    }
  }
}
