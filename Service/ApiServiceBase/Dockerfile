#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ApiServiceBase.csproj", "."]
RUN dotnet restore "./ApiServiceBase.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "ApiServiceBase.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApiServiceBase.csproj" -c Release -o /app/publish

FROM base AS final

# .NET 6 Set Dump
# ENV COMPlus_DbgEnableMiniDump=1 
# ENV COMPlus_DbgMiniDumpType=4	
# ENV COMPlus_DbgMiniDumpName="/var/dumps/dump.dmp" 
# ENV COMPlus_CreateDumpDiagnostics=1 
# ENV COMPlus_EnableCrashReport=1

ENV DOTNET_DbgEnableMiniDump=1 
ENV DOTNET_DbgMiniDumpType=4	
# ENV DOTNET_DbgMiniDumpName="/var/dumps/dump.dmp" 
# ENV DOTNET_CreateDumpDiagnostics=1 
# ENV DOTNET_EnableCrashReport=1


WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiServiceBase.dll"]