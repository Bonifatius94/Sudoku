FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env

WORKDIR /app/src
COPY ./src/Sudoku.sln ./Sudoku.sln
COPY ./src/Sudoku.Algorithms/Sudoku.Algorithms.csproj ./Sudoku.Algorithms/Sudoku.Algorithms.csproj
COPY ./src/Sudoku.Data/Sudoku.Data.csproj ./Sudoku.Data/Sudoku.Data.csproj
COPY ./src/Sudoku.UI/Sudoku.UI.csproj ./Sudoku.UI/Sudoku.UI.csproj
COPY ./src/Sudoku.UnitTests/Sudoku.UnitTests.csproj ./Sudoku.UnitTests/Sudoku.UnitTests.csproj
RUN dotnet restore

COPY ./src .
RUN dotnet test
RUN dotnet publish -c Release -o /app/out

#FROM mcr.microsoft.com/dotnet/runtime:5.0
#WORKDIR /app
#COPY --from=build-env /app/out .
# ENTRYPOINT ["dotnet", ""]
