function Get-CalibrationValue {
  [CmdletBinding()]
  param (
    [Parameter(ValueFromPipeline)]
    $line
  )
  process {
    $digits = $line | Select-String '(\d)' -AllMatches
    $first = $digits.Matches[0]
    $last = $digits.Matches[-1]
    $value = [int]::Parse("$first$last")
    Write-Output $value
  }
}

function Get-Digit {
  param (
    [Parameter(ValueFromPipeline)]
    $s
  )
  process {
    switch -Regex ($s) {
      '1|one|eno' { Write-Output '1'; break }
      '2|two|owt' { Write-Output '2'; break }
      '3|three|eerht' { Write-Output '3'; break }
      '4|four|ruof' { Write-Output '4'; break }
      '5|five|evif' { Write-Output '5'; break }
      '6|six|xis' { Write-Output '6'; break }
      '7|seven|neves' { Write-Output '7'; break }
      '8|eight|thgie' { Write-Output '8'; break }
      '9|nine|enin' { Write-Output '9'; break }
      '0|zero|orez' { Write-Output '0'; break }
    }
  }
}

function Get-BetterCalibrationValue {
  param (
      [Parameter(ValueFromPipeline)]
      $line
  )
  process {
    $forwardMatches = $line | Select-String -AllMatches '(\d|one|two|three|four|five|six|seven|eight|nine|zero)'
    $first = $forwardMatches.Matches[0].Value | Get-Digit
    $reverseLine = $line[-1..-$line.Length] -Join ''
    $backwardMatches = $reverseLine | Select-String -AllMatches '(\d|eno|owt|eerht|ruof|evif|xis|neves|thgie|enin|orez)'
    $last = $backwardMatches.Matches[0].Value | Get-Digit
    $value = [int]::Parse("$first$last")
    Write-Output $value
  }
}
