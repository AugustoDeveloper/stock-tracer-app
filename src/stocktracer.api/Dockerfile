FROM mcr.microsoft.com/dotnet/runtime-deps:5.0-buster-slim AS base
WORKDIR /api

FROM mcr.microsoft.com/dotnet/sdk:5.0.202 AS build
COPY . .
RUN dotnet publish /src/stocktracer.api/stocktracer.api.csproj -c Release -o /api -r linux-x64 --self-contained

FROM base AS final
WORKDIR /app
COPY --from=build /api .
#ENV ASPNETCORE_URLS=http://*:80

CMD ASPNETCORE_URLS=http://*:$PORT  ./stocktracer.api