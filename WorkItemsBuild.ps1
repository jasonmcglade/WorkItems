$framework = '4.0'

Properties {
	$build_dir = Split-Path $psake.build_script_file	
	$build_artifacts_dir = "$build_dir\build\"
	$solution_file = ".\WorkItems.sln"
    $nunit_path = "$build_dir\packages\NUnit.2.5.10.11092\tools\nunit-console.exe"
}

Properties {
    $nunit_module = "$build_dir\tools\psake\nunit.psm1"
}

# Database Properties
Properties {
    $sqlite_exe = "tools\sqlite\sqlite3.exe"
    $database_directory = "$build_dir\src\Web\App_Data"
    $database_name = "$database_directory\work_items.db"

    $database_migrations_path = "$build_dir\src\Database\Migrations"
    $database_data_path = "$build_dir\src\Database\Data"
}

FormatTaskName {
   param($taskName)
   write-host (("-"*25) + "[$taskName]" + ("-"*25))  -foregroundcolor Green
}

Task default -Depends Test

Task Test -Depends Compile, Clean {
    $test_assemblies = (Get-ChildItem "$build_dir" -Recurse -Include *Tests.dll -Name | Select-String "bin")
    
    Import-Module $nunit_module -argument $nunit_path

    foreach($assembly_name in $test_assemblies) {
        Log "Executing tests for $assembly_name"
        [array]$results += nunit "$assembly_name"
    }
    
    foreach ($result in $results) {
        Write-NunitRes $result
    
        if ($result.Failed) {
            throw "Unit test failures"
        }
    }
}

Task Compile -Depends Clean {
    Log "Building solution"
	Exec { msbuild "$solution_file" /t:Build /p:TreatWarningsAsErrors=true } 
}

Task Clean {
	Log "Creating BuildArtifacts directory"
	if (Test-Path $build_artifacts_dir) 
	{	
		rd $build_artifacts_dir -rec -force | out-null
	}
	
	mkdir $build_artifacts_dir | out-null
	
	Log "Cleaning Solution"
	Exec { msbuild $solution_file /t:Clean } 
}

Task DB {
    if ((Test-Path $database_directory) -eq $false) {
        New-Item $database_directory -type directory
    }

    if (Test-Path $database_name) {
        Log "Deleting existing database file: $database_name"
        Remove-Item $database_name
    }

    $migration_files = (Get-ChildItem "$database_migrations_path" -Include *.sql -Name)

    foreach($file in $migration_files) {
        Log "Running migration file: $file"
        Exec { cmd.exe /C "$sqlite_exe $database_name < $database_migrations_path\$file" } 
    }
}

Task Populate_DB -Depends DB {
    $test_data_files = (Get-ChildItem "$database_data_path" -Include test*.sql -Name)

    foreach($file in $test_data_files) {
        Log "Running test data file: $file"
        Exec { cmd.exe /C "$sqlite_exe $database_name < $database_data_path\$file" } 
    }

}

Function Log ($message)
{
    Write-Host $message -ForegroundColor Blue
}