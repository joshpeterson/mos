#!/bin/sh

# Exit the script if any of the command return an error condition.
set -e

nuget restore Mos6510.sln
nuget install NUnit.Runners -Version 2.6.4 -OutputDirectory testrunner
xbuild /nologo /verbosity:quiet Mos6510.sln
mono ./testrunner/NUnit.Runners.2.6.4/tools/nunit-console.exe --nologo --labels ./Mos6510.Tests/bin/Debug/Mos6510.Tests.dll
