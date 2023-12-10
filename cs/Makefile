new-project:
	dotnet new console --use-program-main --name Advent2023.Day$(day).Part1
	dotnet new console --use-program-main --name Advent2023.Day$(day).Part2
	cp Advent2023.Common/stylecop.json Advent2023.Day$(day).Part1/
	cp Advent2023.Common/stylecop.json Advent2023.Day$(day).Part2/
	cp Advent2023.Day01.Part1/Advent2023.Day01.Part1.csproj Advent2023.Day$(day).Part1/Advent2023.Day$(day).Part1.csproj
	cp Advent2023.Day01.Part1/Advent2023.Day01.Part1.csproj Advent2023.Day$(day).Part2/Advent2023.Day$(day).Part2.csproj
	m4 --define=xDAY=$(day) --define=xPART=1 Program.cs.m4 > Advent2023.Day$(day).Part1/Program.cs
	m4 --define=xDAY=$(day) --define=xPART=2 Program.cs.m4 > Advent2023.Day$(day).Part2/Program.cs
	dotnet sln add Advent2023.Day$(day).Part1/Advent2023.Day$(day).Part1.csproj
	dotnet sln add Advent2023.Day$(day).Part2/Advent2023.Day$(day).Part2.csproj
