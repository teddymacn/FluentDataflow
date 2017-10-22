#echo off
del *.nupkg
.\.nuget\NuGet.exe pack .\FluentDataflow\FluentDataflow.nuspec -Prop Configuration=Release -Sym
