#!/snap/bin/pwsh
# NOTE. Change the above path to where powershell executable is found on your system.

<#
Create EF DB context from existing DB tables.
#>

# Define DBVS connection parameters
$DBConnStr = "Server=localhost;User=root;Password=;Database=karkasai";
$DBProvider = "Pomelo.EntityFrameworkCore.MySql"

# Define DB metadata
$DBContextName = "DB"
$DBContextNamespace = "modkaz.DBs"
$DBEntitiesNamespace = "modkaz.DBs.Entities"

$ContextDstDir = "./"
$EntitiesDstDir = "./Entities"

# Clean destination folders
Remove-Item ($EntitiesDstDir + "/*")
Remove-Item ($ContextDstDir + $DBContextName + ".Generated.cs")

#define tables to include
$Tables = `
	@(
		"DemoEntities"
	)
$Tables = $Tables | ForEach-Object {@("--table" , $_)}

# Build command line arguments and invoke the reverse engineering tool
$CmdLineArgs = `
	@(
		"dbcontext",
		"scaffold",
		$DBConnStr,
		$DBProvider,
		"--no-build",
		"--data-annotations",
		"--use-database-names",
		"--no-pluralize",
		"--context-dir", $ContextDstDir,
		"--context", ($DBContextName + "Generated"),
		"--context-namespace", $DBContextNamespace,
		"--output-dir", $EntitiesDstDir,
		"--namespace", $DBEntitiesNamespace,
		"--no-onconfiguring"
	)
$CmdLineArgs = $CmdLineArgs + $Tables

& "dotnet-ef" $CmdLineArgs

# Rename generated file to proper naming conventions
Rename-Item -Path ($ContextDstDir + $DBContextName + "Generated.cs") -NewName ($DBContextName + ".Generated.cs")

# Change generated class name to a proper one
(Get-Content -path ($ContextDstDir + $DBContextName + ".Generated.cs") -Raw) `
	-replace ($DBContextName + "Generated"), $DBContextName `
	| Set-Content -Path ($ContextDstDir + $DBContextName + ".Generated.cs")

# Hide generated constructors
(Get-Content -path ($ContextDstDir + $DBContextName + ".Generated.cs") -Raw) `
	-replace ("public " + $DBContextName), ("private " + $DBContextName) `
	| Set-Content -Path ($ContextDstDir + $DBContextName + ".Generated.cs")

# Unhide no-args constructor
(Get-Content -path ($ContextDstDir + $DBContextName + ".Generated.cs") -Raw) `
	-replace ("private " + $DBContextName + "\(\)"), ("public " + $DBContextName + "()") `
	| Set-Content -Path ($ContextDstDir + $DBContextName + ".Generated.cs")
