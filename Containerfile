FROM mcr.microsoft.com/dotnet/runtime:10.0

WORKDIR /pig
COPY publish/ ./

ENTRYPOINT ["dotnet", "pig.dll"]
