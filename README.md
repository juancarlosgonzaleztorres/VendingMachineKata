# Vending Machine Kata

Instructions for executing the program:
1) Using a Windows machine, install .NET Core 2 from: https://www.microsoft.com/net/learn/get-started/windows
(If you have a Mac or Linux, you can use those installer versions).
2) Clone or download this github repository.
3) Open a command prompt or powershell console (In Windows 10 is windows key + R, type powershell or cmd).
4) Go to PublishOutput folder in the downloaded github repository.
5) Type "dotnet VendingMachine.dll".
6) Follow the instructions of the program.
7) To finish the program type "exit".

Source Code:
For this coding exercise, I used Visual Studio 2017 Community Edition.
It can be downloaded from https://www.visualstudio.com/vs/whatsnew/
To open the source files with Visual Studio, go to your copy of the code and open the solution file 
VendingMachineKata.sln.

Running unit tests:
Unit tests are at VendingMachineUnitTests projects. All the unit tests are at TestVendingMachine.cs and if you 
open the solution with Visual Studio you can run them from Test Explorer, since they are made with MSTest. 
Alternative, you can run the tests from the command line. For this go to the folder of VendingMachineUnitTests
and type: 
dotnet test
