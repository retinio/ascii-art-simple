ARG VERSION=3.1-alpine

FROM mcr.microsoft.com/dotnet/core/sdk:$VERSION AS build
WORKDIR /app
COPY /src/AsciiArtSimple/AsciiArtSimple.csproj .
RUN dotnet restore AsciiArtSimple.csproj

COPY . .
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/runtime:$VERSION AS runtime
WORKDIR /app
COPY --from=build /app/out ./
COPY --from=build /app/fonts/ ./fonts

ENTRYPOINT ["dotnet", "AsciiArtSimple.dll"]
