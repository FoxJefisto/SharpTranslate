FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SharpTranslate/SharpTranslate.csproj", "SharpTranslate/"]
RUN dotnet restore "SharpTranslate/SharpTranslate.csproj"
COPY . .
WORKDIR "/src/SharpTranslate"
RUN dotnet build "SharpTranslate.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SharpTranslate.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SharpTranslate.dll"]