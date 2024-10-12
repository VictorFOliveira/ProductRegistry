# Usar a imagem base do .NET SDK 8.0
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

# Definir o diretório de trabalho
WORKDIR /app

# Copiar o arquivo csproj e restaurar as dependências
COPY *.sln ./
COPY SistemaDeCadastro/*.csproj ./SistemaDeCadastro/
RUN dotnet restore

# Copiar o restante dos arquivos e construir o projeto
COPY . ./
RUN dotnet publish -c Release -o out

# Criar a imagem final
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

# Expor a porta que o app vai rodar
EXPOSE 80

# Comando para rodar a aplicação
ENTRYPOINT ["dotnet", "SistemaDeCadastro.dll"]
