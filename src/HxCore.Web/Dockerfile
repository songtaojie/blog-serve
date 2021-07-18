#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM registry.cn-hangzhou.aliyuncs.com/songtaojie_centos/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM registry.cn-hangzhou.aliyuncs.com/songtaojie_centos/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/HxCore.Entity/HxCore.Entity.csproj", "/src/HxCore.Entity/"]
COPY ["src/HxCore.Model/HxCore.Model.csproj", "/src/HxCore.Model/"]
COPY ["src/HxCore.IServices/HxCore.IServices.csproj", "/src/HxCore.IServices/"]
COPY ["src/HxCore.Services/HxCore.Services.csproj", "/src/HxCore.Services/"]
COPY ["src/HxCore.Extensions/HxCore.Extensions.csproj", "/src/HxCore.Extensions/"]
COPY ["src/HxCore.Web/HxCore.Web.csproj", "/src/HxCore.Web/"]

RUN dotnet restore "/src/HxCore.Web/HxCore.Web.csproj"
COPY . .
WORKDIR "/src/src/HxCore.Web"
RUN dotnet build "HxCore.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HxCore.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HxCore.Web.dll"]