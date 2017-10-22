#echo off
dotnet build -c Release FluentDataFlow
&"C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\MSBuild.exe" FluentDataFlowPortable\FluentDataFlowPortable.csproj /p:Configuration=Release
