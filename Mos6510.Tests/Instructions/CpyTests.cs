using NUnit.Framework;
using Mos6510.Instructions;

namespace Mos6510.Tests.Instructions
{
  [TestFixture]
  public class CpyTests : CompareTestUtils<Cpy>
  {
    public CpyTests() : base(RegisterName.Y) {}
  }
}
