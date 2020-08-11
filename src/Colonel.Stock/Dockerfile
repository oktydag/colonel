FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /app
 
COPY *.csproj ./
RUN dotnet restore
 
COPY . ./
RUN dotnet publish -c Release -o out
 
FROM microsoft/dotnet:2.2-aspnetcore-runtime AS runtime
 
WORKDIR /app
 
COPY --from=build /app/out .

ENV ASPNETCORE_URLS=http://+:5002

EXPOSE 5002

ENTRYPOINT ["dotnet","Colonel.Stock.dll", "--port", "5002"]

