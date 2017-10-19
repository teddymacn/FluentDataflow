#echo off
del *.nupkg
cd FluentDataFlow
del bin\Release\*.nupkg
dotnet pack -c Release --include-symbols
copy bin\Release\*.nupkg ..\
cd ..