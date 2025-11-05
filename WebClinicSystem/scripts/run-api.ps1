param()

# Para executar apenas a API em um processo separado
Write-Host "Parando processos dotnet que estejam rodando a API (se houver) ..."
# Não força matar todos dotnet; tenta localizar processos que tenham WebClinicSystem.Api no caminho de execução
Get-Process -Name dotnet -ErrorAction SilentlyContinue | Where-Object {
    try {
        $_.Path -and (Get-Command $_.Path -ErrorAction SilentlyContinue) -and (($_.Path) -match "dotnet")
    } catch { $false }
} | ForEach-Object {
    # Não matar aqui, apenas listar
    Write-Host "Processo dotnet encontrado (Id: $($_.Id))"
}

Write-Host "Iniciando WebClinicSystem.Api..."
dotnet run --project "WebClinicSystem.Api/WebClinicSystem.Api.csproj"