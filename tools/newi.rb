#!/usr/bin/env ruby

templateCs = %{using System.Collections.Generic;

namespace Mos6510.Instructions
{
  public class <class> : Instruction
  {
    public Result Execute(ProgrammingModel model, Memory memory, Argument argument)
    {
      throw new System.NotImplementedException();
    }
  }
}
}

templateTest = %{using NUnit.Framework;
using Mos6510.Instructions;

namespace Mos6510.Tests.Instructions
{
  [TestFixture]
  public class <class>Tests
  {
    [Test]
    public void TheTruth()
    {
      Assert.That(true, Is.True);
    }
  }
}
}

def replaceClass(klass, template)
  template.gsub("<class>", klass)
end

if (ARGV.length != 1)
  puts "Usage: newi.rb instruction\n"
  exit
end

klass = ARGV[0]

codeDirectory = "Mos6510/Instructions"
testDirectory = "Mos6510.Tests/Instructions"

codeFile = "#{codeDirectory}/#{klass}.cs"

if (File.exists?(codeFile))
  puts "Error: The file '#{codeFile}' already exists.\n"
  exit
end

testFile = "#{testDirectory}/#{klass}Tests.cs"

if (File.exists?(testFile))
  puts "Error: The file '#{testFile}' already exists.\n"
  exit
end

File.open(codeFile, 'w') { |file|
  file.write(replaceClass(klass, templateCs))
}

File.open(testFile, 'w') { |file|
  file.write(replaceClass(klass, templateTest))
}
