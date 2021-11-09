#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat
#这种模式是直接在构建镜像的内部编译发布dotnet项目。
#注意下容器内输出端口是8081
#如果你想先手动dotnet build成可执行的二进制文件，然后再构建镜像，请看.Api层下的dockerfile。
# FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
FROM registry.cn-hangzhou.aliyuncs.com/songtaojie/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 5002
ENV LANG zh_CN.UTF-8  
ENV LANGUAGE zh_CN.UTF-8 
ENV LC_ALL zh_CN.UTF-8  

#FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
FROM registry.cn-hangzhou.aliyuncs.com/songtaojie/dotnet_sdk:3.1 AS build
WORKDIR /src
COPY ["Hx.IdentityServer/Hx.IdentityServer.csproj", "Hx.IdentityServer/"]
COPY ["Hx.IdentityServer.Extensions/Hx.IdentityServer.Extensions.csproj", "Hx.IdentityServer.Extensions/"]
COPY ["Hx.IdentityServer.Model/Hx.IdentityServer.Model.csproj", "Hx.IdentityServer.Model/"]
COPY ["Hx.IdentityServer.Common/Hx.IdentityServer.Common.csproj", "Hx.IdentityServer.Common/"]
RUN dotnet restore "Hx.IdentityServer/Hx.IdentityServer.csproj"
COPY . .
WORKDIR "/src/Hx.IdentityServer"
RUN dotnet build "Hx.IdentityServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Hx.IdentityServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Hx.IdentityServer.dll"]