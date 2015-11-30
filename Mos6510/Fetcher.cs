namespace Mos6510
{
  public class Fetcher
  {
    private readonly ProgrammingModel _model;
    private readonly Memory _memory;

    public Fetcher(ProgrammingModel model, Memory memory)
    {
      _model = model;
      _memory = memory;
    }

    public byte Fetch()
    {
      var address = _model.GetRegister(RegisterName.PC).GetValue();
      return _memory.GetValue((ushort)address);
    }
  }
}
