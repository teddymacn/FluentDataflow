#echo off
cd FluentDataFlow
del *.nupkg
del bin\Release\*.nupkg
dotnet pack -c Release --include-symbols
copy bin\Release\*.nupkg ..\
cd ..