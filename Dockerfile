#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# 在 Dockerfile 中添加以下行，安装字体
RUN apt-get update && apt-get install -y \
    libgdiplus \
    libc6-dev \
    libx11-dev \
    libfontconfig1 \
    libfreetype6 \
    libxrender1 \
    libxcb1 \
    libx11-6 \
    libx11-data \
    libxau6 \
    libxdmcp6 \
    xfonts-scalable \
    msttcorefonts \
    fontconfig-config \
    && rm -rf /var/lib/apt/lists/*


ENV TZ=Asia/Shanghai
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone

RUN sed -i 's/TLSv1.2/TLSv1/g' /etc/ssl/openssl.cnf
 
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["webapi/Webapi.csproj", "webapi/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["Respository/Repository.csproj", "Respository/"]
COPY ["Service/Service.csproj", "Service/"]
RUN dotnet restore "./webapi/./Webapi.csproj"
COPY . .
WORKDIR "/src/webapi"
RUN dotnet build "./Webapi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Webapi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Webapi.dll"]
