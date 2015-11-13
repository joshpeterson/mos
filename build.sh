#!/bin/sh

# Exit the script if any of the command return an error condition.
set -e

xbuild /nologo /verbosity:quiet
nunit-console --framework=4.0 --labels --nologo Mos6510.Tests/bin/Debug/Mos6510.Tests.dll
