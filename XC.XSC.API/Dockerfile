#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["XC.XSC.API/XC.XSC.API.csproj", "XC.XSC.API/"]
COPY ["XC.XSC.Data/XC.XSC.Data.csproj", "XC.XSC.Data/"]
COPY ["XC.XSC.Domain/XC.XSC.Models/XC.XSC.Models.csproj", "XC.XSC.Domain/XC.XSC.Models/"]
COPY ["XC.XSC.Domain/XC.XSC.ViewModels/XC.XSC.ViewModels.csproj", "XC.XSC.Domain/XC.XSC.ViewModels/"]
COPY ["XC.XSC.Integrations/XC.CCMP.IAM/XC.CCMP.IAM.csproj", "XC.XSC.Integrations/XC.CCMP.IAM/"]
COPY ["XC.XSC.Integrations/XC.CCMP.KeyVault/XC.CCMP.KeyVault.csproj", "XC.XSC.Integrations/XC.CCMP.KeyVault/"]
COPY ["XC.XSC.Domain/XC.XSC.Model.Profiles/XC.XSC.ProfileMapping.csproj", "XC.XSC.Domain/XC.XSC.Model.Profiles/"]
COPY ["XC.XSC.Extentions/XC.XSC.Utilities.csproj", "XC.XSC.Extentions/"]
COPY ["XC.XSC.Integrations/XC.CCMP.Caching/XC.CCMP.Caching.csproj", "XC.XSC.Integrations/XC.CCMP.Caching/"]
COPY ["XC.XSC.Integrations/XC.CCMP.Logger/XC.CCMP.Logger.csproj", "XC.XSC.Integrations/XC.CCMP.Logger/"]
COPY ["XC.XSC.Integrations/XC.CCMP.Queue.MessageBroker/XC.CCMP.Queue.MessageBroker.csproj", "XC.XSC.Integrations/XC.CCMP.Queue.MessageBroker/"]
COPY ["XC.XSC.Integrations/XC.CCMP.Queue.MessageAdapter/XC.CCMP.Queue.MessageAdapter.csproj", "XC.XSC.Integrations/XC.CCMP.Queue.MessageAdapter/"]
COPY ["XC.XSC.Integrations/XC.CCMP.Queue/XC.CCMP.Queue.csproj", "XC.XSC.Integrations/XC.CCMP.Queue/"]
COPY ["XC.XSC.Integrations/XC.XSC.ValidateMail/XC.XSC.ValidateMail.csproj", "XC.XSC.Integrations/XC.XSC.ValidateMail/"]
COPY ["XC.XSC.Repositories/XC.XSC.Repositories.csproj", "XC.XSC.Repositories/"]
COPY ["XC.XSC.Integrations/XC.XSC.EmailService/XC.XSC.EmailSender.csproj", "XC.XSC.Integrations/XC.XSC.EmailService/"]
COPY ["XC.XSC.Integrations/XC.XSC.UAM/XC.XSC.UAM.csproj", "XC.XSC.Integrations/XC.XSC.UAM/"]
COPY ["XC.XSC.Integrations/XC.XSC.Workflow/XC.XSC.Workflow.csproj", "XC.XSC.Integrations/XC.XSC.Workflow/"]
COPY ["XC.XSC.Integrations/XSC.CCMP.DataStorage/XC.CCMP.DataStorage.csproj", "XC.XSC.Integrations/XSC.CCMP.DataStorage/"]
COPY ["XC.XSC.Service/XC.XSC.Service.csproj", "XC.XSC.Service/"]
COPY ["XC.XSC.Integrations/XC.XSC.EMS/XC.XSC.EMS.csproj", "XC.XSC.Integrations/XC.XSC.EMS/"]
RUN dotnet restore "XC.XSC.API/XC.XSC.API.csproj"
COPY . .
WORKDIR "/src/XC.XSC.API"
RUN dotnet build "XC.XSC.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "XC.XSC.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "XC.XSC.API.dll"]