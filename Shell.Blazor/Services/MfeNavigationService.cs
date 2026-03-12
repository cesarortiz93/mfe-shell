namespace Shell.Blazor.Services;

public record MfeConfig(string Url, string IframeUrl, string Name);

public class MfeNavigationService
{
    private readonly IConfiguration _config;

    public MfeNavigationService(IConfiguration config) => _config = config;


    // Retorna la config de un MFE por nombre
    public MfeConfig GetMfe(string mfeName)
    {
        var url = _config[$"Mfe:{mfeName}:Url"]
            ?? throw new InvalidOperationException(
                $"MFE {mfeName} no configurado");
        var iframeUrl = _config[$"Mfe:{mfeName}:IframeUrl"] ?? url;
        var name = _config[$"Mfe:{mfeName}:Name"] ?? mfeName;
        return new MfeConfig(url, iframeUrl, name);
    }

    // Retorna todos los MFEs configurados
    public IEnumerable<(string Key, MfeConfig Config)> GetAll()
        => new[] { "Inicio", "Portafolio", "Siniestros" }
               .Select(k => (k, GetMfe(k)));
}
