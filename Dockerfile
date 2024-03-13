FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /TicketsRUs.WebApp
 
EXPOSE 8080
 
COPY TicketsRUs.WebApp/TicketsRUs.WebApp.csproj .
RUN dotnet restore
 
COPY . .
WORKDIR /TicketsRUs.WebApp/TicketsRUs.WebApp
RUN dotnet build -c Release -o /app/build
 
FROM build as publish
RUN dotnet publish -c Release -o /app/publish
 
FROM mcr.microsoft.com/dotnet/aspnet:8.0 as runtime
WORKDIR /app
COPY --from=publish /app/publish .
 
ENTRYPOINT ["dotnet", "TicketsRUs.WebApp.dll"]


# docker run -it --rm -v$(pwd):app -w /app mcr.microsoft.com/dotnet/sdk:8.0 dotnet format --verify-no-changes --severity
# docker run -it --rm -v$(pwd):app -w /app mcr.microsoft.com/dotnet/sdk:8.0 dotnet dotnet build -waraserror | 