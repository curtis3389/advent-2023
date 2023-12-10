new-project:
	dotnet new console --use-program-main --name Advent2023.Day$(day).Part1
	dotnet new console --use-program-main --name Advent2023.Day$(day).Part2
	cp Advent2023.Common/stylecop.json Advent2023.Day$(day).Part1/
	cp Advent2023.Common/stylecop.json Advent2023.Day$(day).Part2/
	dotnet add ./Advent2023.Day$(day).Part1/Advent2023.Day$(day).Part1.csproj reference ./Advent2023.Common/Advent2023.Common.csproj
	dotnet add ./Advent2023.Day$(day).Part1/Advent2023.Day$(day).Part1.csproj package StyleCop.Analyzers
	dotnet add ./Advent2023.Day$(day).Part1/Advent2023.Day$(day).Part1.csproj package Microsoft.CodeAnalysis.Analyzers
	dotnet add ./Advent2023.Day$(day).Part2/Advent2023.Day$(day).Part2.csproj reference ./Advent2023.Common/Advent2023.Common.csproj
	dotnet add ./Advent2023.Day$(day).Part2/Advent2023.Day$(day).Part2.csproj package StyleCop.Analyzers
	dotnet add ./Advent2023.Day$(day).Part2/Advent2023.Day$(day).Part2.csproj package Microsoft.CodeAnalysis.Analyzers
	dotnet sln add Advent2023.Day$(day).Part1/Advent2023.Day$(day).Part1.csproj
	dotnet sln add Advent2023.Day$(day).Part2/Advent2023.Day$(day).Part2.csproj
