using NUnit.Framework;

namespace Mos6510.Tests
{
[TestFixture]
class MemoryTests
{
    [Test]
    public void InitialValueOfMemoryLocationIsZero()
    {
        var memory = new Memory();
        Assert.That(memory.GetValue(0xFF50), Is.EqualTo(0));
    }

    [Test]
    public void CanSetAndGetAtAnAddress()
    {
        const ushort address = 0x34FA;
        const byte value = 251;

        var memory = new Memory();

        memory.SetValue(address, value);
        Assert.That(memory.GetValue(address), Is.EqualTo(value));
    }
}
}
