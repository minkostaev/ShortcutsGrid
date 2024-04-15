
# Copy index.html for GitHub pages
Copy-Item -Path 'Resources\ListCsv\Win Admin.csv' -Destination 'bin\Win Admin.csv'

# Read the .csproj file and extract the version number
#$xml = [xml](Get-Content "BlazorRadzenMls.csproj")
#$version = $xml.Project.PropertyGroup.Version

# Write content to the file
#$version | Out-File -FilePath "wwwroot\data\version.txt" -Encoding utf8
