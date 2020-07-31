Import-Module "$PSScriptRoot\MG.Core.PowerShell.Dhcp.dll" -ea 1
Push-Location $([System.Environment]::GetFolderPath("Desktop"))

Get-DhcpServerInDc