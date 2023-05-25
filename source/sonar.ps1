$sonarToken = [System.IO.File]::ReadAllText("sonarToken.txt")

dotnet sonarscanner begin /k:"Hybrid" /d:sonar.host.url="http://localhost:9000" /d:sonar.login=$sonarToken

dotnet build --no-incremental
dotnet test

dotnet sonarscanner end /d:sonar.login=$sonarToken