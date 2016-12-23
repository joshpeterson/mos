#!/usr/bin/env ruby

templateCs = %{namespace <namespace>
{
  public class <class>
  {
  }
}
}

templateTest = %{using NUnit.Framework;

namespace <namespace>.Tests
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

def replaceNamespace(namespace, template)
  template.gsub("<namespace>", namespace)
end

def replaceClass(klass, template)
  template.gsub("<class>", klass)
end

if (ARGV.length != 2)
  puts "Usage: newcs.rb namespace class\n"
  exit
end

namespace = ARGV[0]
klass = ARGV[1]

codeDirectory = namespace;

if (!Dir.exists?(codeDirectory))
  puts "Error: The directory '#{codeDirectory}' does not exist.\n"
  exit
end

testDirectory = "#{namespace}.Tests"

if (!Dir.exists?(testDirectory))
  puts "Error: The directory '#{testDirectory}' does not exist.\n"
  exit
end

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
  file.write(replaceClass(klass, replaceNamespace(namespace, templateCs)))
}

File.open(testFile, 'w') { |file|
  file.write(replaceClass(klass, replaceNamespace(namespace, templateTest)))
}
