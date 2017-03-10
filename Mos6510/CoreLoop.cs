using Mos6510.Instructions;

namespace Mos6510
{
  public class CoreLoop
  {
    private readonly Fetcher fetcher;
    private readonly Decoder decoder;
    private readonly Executor executor;

    public CoreLoop(Fetcher fetcher, Decoder decoder, Executor executor)
    {
      this.fetcher = fetcher;
      this.decoder = decoder;
      this.executor = executor;
    }

    public bool SingleStep()
    {
      var instruction = fetcher.Fetch();

      OpcodeData data;
      if (!decoder.TryDecode(instruction, out data))
        return false;

      executor.Execute(data.Opcode, data.Mode, fetcher.OperandFor(data.Mode));

      return true;
    }
  }
}
