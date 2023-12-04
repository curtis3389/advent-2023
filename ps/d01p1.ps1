Import-Module $PSScriptRoot\Common.psm1

$file = $args[0]
Get-Content $file | Get-CalibrationValue | Measure-Object -Sum | Select-Object -Expand Sum
