FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /src

# Get connection string argument from docker compose and set it as an environment variable
ARG connection_string
ENV ConnectionStrings__Database=${connection_string}

# Standard docker build
COPY . .

RUN dotnet restore "SocialNetwork.Console.IntegrationTests/SocialNetwork.Console.IntegrationTests.csproj"
COPY . .
WORKDIR "SocialNetwork.Console.IntegrationTests"

# Restore the dotnet-ef command
RUN dotnet tool install --global dotnet-ef
ENV PATH $PATH:/root/.dotnet/tools
RUN dotnet build "SocialNetwork.Console.IntegrationTests.csproj" -c Release -o /app/build

# Install docker-compose-wait to make sure the db server is up & running before moving on
ADD https://github.com/ufoscout/docker-compose-wait/releases/download/2.5.0/wait /wait
RUN chmod +x /wait

# Wait for sql server and then migrate the db and run tests
CMD /wait && dotnet ef database update --context AppDbContext && dotnet t