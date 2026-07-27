FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["VoiceAssistant.slnx", "./"]
COPY ["Directory.Packages.props", "./"] 
COPY ["src/VoiceAssistant.Api/VoiceAssistant.Api.csproj", "src/VoiceAssistant.Api/"]
COPY ["src/VoiceAssistant.Application/VoiceAssistant.Application.csproj", "src/VoiceAssistant.Application/"]
COPY ["src/VoiceAssistant.Domain/VoiceAssistant.Domain.csproj", "src/VoiceAssistant.Domain/"]
COPY ["src/VoiceAssistant.Infrastructure/VoiceAssistant.Infrastructure.csproj", "src/VoiceAssistant.Infrastructure/"]
RUN dotnet restore "src/VoiceAssistant.Api/VoiceAssistant.Api.csproj"
COPY . .
WORKDIR "/src/src/VoiceAssistant.Api"
RUN dotnet build "./VoiceAssistant.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./VoiceAssistant.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VoiceAssistant.Api.dll"]