# Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["src/GoodHamburger.BlazorWasm/GoodHamburger.BlazorWasm.csproj", "src/GoodHamburger.BlazorWasm/"]
COPY ["src/GoodHamburger.Shared/GoodHamburger.Shared.csproj", "src/GoodHamburger.Shared/"]

RUN dotnet restore "src/GoodHamburger.BlazorWasm/GoodHamburger.BlazorWasm.csproj"

COPY . .
WORKDIR "/src/src/GoodHamburger.BlazorWasm"
RUN dotnet publish -c Release -o /app/publish

# Runtime (Nginx)
FROM nginx:alpine
WORKDIR /usr/share/nginx/html
COPY --from=build /app/publish/wwwroot .
EXPOSE 80