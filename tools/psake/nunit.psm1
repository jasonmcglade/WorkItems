param(
    [Parameter(Mandatory=$true)]
    [string]
    $nunitPath
)

$script:nunit = $nunitPath
Write-Host "Nunit path set to $nunitPath"

function nunit 
{
    param(
        [Parameter(Position=0,Mandatory=$true)]
        [string]
        $assembly,
        [Parameter(Position=1,Mandatory=$false)]
        [string[]]
        $Include = @(),
        [Parameter(Position=2,Mandatory=$false)]
        [string[]]
        $Exclude = @(),
        [Parameter()]
        [switch]
        $silent,
        [Parameter()]
        [switch]
        $NoShadow
    )
    $tempFile = "$env:TEMP\psake-nunit.xml"
    if (test-path $tempFile) { Get-Item $tempFile | Remove-Item  } #Remove-Item $tempFile doesn't work because my temp is C:\Users\J34EF~1.STE\AppData\Local\Temp
    
    $param = @()
    if ($Include.Count -gt 0) { $param += '/include:'+($Include -join ',') }
    if ($Exclude.Count -gt 0) { $param += '/exclude:'+($Exclude -join ',') }
    if ($NoShadow)            { $param += '/noshadow' }
    
    Write-Debug "output xml file: $tempFile"
    $stopwatch = [System.Diagnostics.Stopwatch]::StartNew()
    $output = & $nunit $assembly /nologo /xml:$tempFile @param
    if (!$silent) {
        $output | Out-Host
    }
    $stopwatch.stop()
    
    $ret = new-Object PsObject -prop @{
        Assembly   = $assembly
        Failed     = $false
        Total      = 0
        Failures   = 0
        Errors     = 0
        NotRun     = 0
        FailedTestCases = @()
        Duration   = $stopwatch.Elapsed
        Error      = ''
    }
    if ($lastExitCode -ne 0) {
        $ret.Failed = $true
        if (!(Test-Path $tempFile)) {
            if ($silent) { $output | Out-Host }               # give him chance to see at least something
            $ret.Error = 'No output file was created'
            return $ret
        }
        # when silent, normal output is not written, but then it is not clear what caused the error 
        # so we will parse the xml and show the failing test
        $res = [xml](gc $tempFile)
        $failed = $res | 
            Select-Xml -XPath '//test-case' | 
            Select-Object -ExpandProperty Node |
            ? { $_.success -eq "false" } |
            % { $ret.FailedTestCases += new-Object PsObject -prop @{
                    Name = $_.Name
                    Messsage = $_.failure.Message.InnerText
                    StackTrace = $_.failure.'stack-trace'.InnerText
                 }
              }
        $ret.Failures = $res.'test-results'.Failures
        $ret.Errors = $res.'test-results'.Errors
    }
    else {
        $res = ([xml](gc $tempFile)).'test-results'
        $ret.Total = $res.Total
        $ret.Failures = $res.Failures # should be 0
        $ret.Errors = $res.Errors # should be 0
        $ret.NotRun = $res.'Not-Run' 
    }
    $ret
}

function Write-NunitRes
{
    param([Parameter(Mandatory=$true)][PsObject]$res)
    
    if ($res.Failed) {
        Write-Host "Some Nunit tests failed:"
        $res.FailedTestCases | 
            % { Write-Host "------------------------------------"
                Write-Host Name:`n $_.Name 
                Write-Host Statk trace:`n $_.StackTrace
                Write-Host Message:`n $_.Messsage
                Write-Host
              }
    }
    Write-Host "Total:         " $res.Total 
    Write-Host "Errors         " $res.Errors
    Write-Host "Failures       " $res.Failures
    Write-Host "NotRun:        " $res.NotRun
    Write-Host "Duration:      " $res.Duration
    Write-Host
}