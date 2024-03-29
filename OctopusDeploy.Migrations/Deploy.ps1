﻿$msbuild = 'C:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe'
$migrationNumber = '-1'
$currentFolder = get-location
$target = '{0}\targets\migrations.targets' -f $currentFolder
$migratorMSBuild = '{0}\content\lib\Migrator.MSBuild.dll' -f $currentFolder
$migrationAssembly = '{0}\lib\net40\OctopusDeploy.Migrations.dll' -f $currentFolder
if($connectionString -eq $null)
{
    $connectionString = 'Database=Octopus_Dev;Data Source=localhost;Integrated Security=SSPI'
}

Write-Host "Using msbuild: " $msbuild
Write-Host "Connection string: " $ConnectionString
Write-Host "Current location: " $currentFolder
Write-Host "Target: " $target
Write-Host "Migrator msbuild: " $migratorMSBuild


# migrate
$arguments = '{0} /t:migrate /p:MigrationNumber={1} /p:MigratorMSBuild={2} /p:Migration={3} /p:ConnectionString="{4}"' -f $target, $migrationNumber, $migratorMSBuild, $migrationAssembly, $ConnectionString

Write-Host "Migration arguments: " $arguments

$migrationProcessInfo = New-Object System.Diagnostics.ProcessStartInfo
$migrationProcessInfo.FileName = $msbuild
$migrationProcessInfo.RedirectStandardError = $true
$migrationProcessInfo.RedirectStandardOutput = $true
$migrationProcessInfo.UseShellExecute = $false
$migrationProcessInfo.Arguments = $arguments
$migrationProcess = New-Object System.Diagnostics.Process
$migrationProcess.StartInfo = $migrationProcessInfo
$migrationProcess.Start() | Out-Null

Write-Host "Migration started"
# Must read before waiting for exit
$output = $migrationProcess.StandardOutPut.ReadToEnd()
$migrationProcess.WaitForExit()
Write-Host $output

$migrationProcess = Start-Process $msbuild $arguments -Wait -PassThru

#Start-Process -FilePath $msbuild -ArgumentList '/target:Deploy /p:UseSandboxSettings=false /p:TargetConnectionString="aConnectionWithSpacesAndSemiColons" "aDatabaseProjectPathWithSpaces"';

if($migrationProcess.ExitCode -ne 0) 
{ 
    Write-Host "Migration ended with exit code: " $migrationProcess.ExitCode 
}
if($migrationProcess.ExitCode -eq 0) { Write-Host "Migration succeded with exit code: " $migrationProcess.ExitCode }

exit $migrationProcess.ExitCode