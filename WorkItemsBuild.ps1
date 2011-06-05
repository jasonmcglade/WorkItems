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

FormatTaskName {
   param($taskName)
   write-host (("-"*25) + "[$taskName]" + ("-"*25))  -foregroundcolor Green
}

Task default -Depends Test

Task Test -Depends Compile, Clean {
    $test_assemblies = (Get-ChildItem "$build_dir" -Recurse -Include *Tests.dll -Name | Select-String "bin")
    
    Import-Module $nunit_module -argument $nunit_path

    foreach($assembly_name in $test_assemblies) {
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

Function Log ($message)
{
    Write-Host $message -ForegroundColor Blue
}