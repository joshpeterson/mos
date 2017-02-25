using Mos6510.Instructions;

namespace Mos6510
{
public class Fetcher
{
    private readonly ProgrammingModel model;
    private readonly Memory memory;

    public Fetcher(ProgrammingModel model, Memory memory)
    {
        this.model = model;
        this.memory = memory;
    }

    public byte Fetch()
    {
        return ReadPCAndIncrement();
    }

    public ushort OperandFor(AddressingMode mode)
    {
        ushort operand = ReadPCAndIncrement();
        if (mode == AddressingMode.Absolute ||
                mode == AddressingMode.AbsoluteX ||
                mode == AddressingMode.AbsoluteY)
        {
            byte operandHigh = ReadPCAndIncrement();
            operand = (ushort)((operandHigh << 8) | operand);
        }

        return operand;
    }

    private byte ReadPCAndIncrement()
    {
        var pc = model.GetRegister(RegisterName.PC);
        var address = pc.GetValue();
        pc.SetValue(address + 1);

        return memory.GetValue((ushort)address);
    }
}
}
