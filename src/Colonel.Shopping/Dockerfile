FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /app
 
COPY *.csproj ./
RUN dotnet restore
 
COPY . ./
RUN dotnet publish -c Release -o out
 
FROM microsoft/dotnet:2.2-aspnetcore-runtime AS runtime
 
WORKDIR /app
 
COPY --from=build /app/out .

ENV ASPNETCORE_URLS=http://+:5004

EXPOSE 5004

ENTRYPOINT ["dotnet","Colonel.Shopping.dll", "--port", "5004"]

