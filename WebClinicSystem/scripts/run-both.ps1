# Script para iniciar API e Web em processos separados
# Uso: powershell -ExecutionPolicy Bypass -File .\scripts\run-both.ps1

Write-Host "Procurando processos dotnet relacionados a WebClinicSystem..."
$procs = Get-CimInstance -ClassName Win32_Process | Where-Object { $_.CommandLine -and ($_.CommandLine -match 'WebClinicSystem.Api' -or $_.CommandLine -match 'WebClinicSystem.Web') }
if ($procs) {
    Write-Host "Processos encontrados e serão finalizados:" -ForegroundColor Yellow
    $procs | ForEach-Object { Write-Host "Id: $($_.ProcessId) CommandLine: $($_.CommandLine)" }
    $procs | ForEach-Object { Stop-Process -Id $_.ProcessId -Force -ErrorAction SilentlyContinue }
    Start-Sleep -Seconds 1
}

Write-Host "Iniciando WebClinicSystem.Api..." -ForegroundColor Green
Start-Process -FilePath "dotnet" -ArgumentList "run --project WebClinicSystem.Api/WebClinicSystem.Api.csproj" -WorkingDirectory (Resolve-Path '.') -NoNewWindow -WindowStyle Normal
Start-Sleep -Seconds 1
Write-Host "Iniciando WebClinicSystem.Web..." -ForegroundColor Green
Start-Process -FilePath "dotnet" -ArgumentList "run --project WebClinicSystem.Web/WebClinicSystem.Web.csproj" -WorkingDirectory (Resolve-Path '.') -NoNewWindow -WindowStyle Normal

Write-Host "Ambos os projetos foram iniciados em processos separados. Verifique as portas nos arquivos launchSettings se necessário." -ForegroundColor Cyan
