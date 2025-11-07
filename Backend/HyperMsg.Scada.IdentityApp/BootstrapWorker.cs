using HyperMsg.Scada.IdentityApp.Data;
using OpenIddict.Abstractions;
using System.Security.Cryptography.X509Certificates;
using static OpenIddict.Abstractions.OpenIddictConstants;

public class BootstrapWorker(IServiceProvider serviceProvider) : IHostedService
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await context.Database.EnsureCreatedAsync();

        var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

        if (await manager.FindByClientIdAsync("admin@mail.com", cancellationToken) is not null)
            return;

        var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

        if (!env.IsDevelopment())
        {
            return;
        }

        await manager.CreateAsync(new()
        {
            ClientId = "admin@mail.com",
            ClientSecret = "Admin123!",
            Permissions =
                {
                    Permissions.Endpoints.Token,
                    Permissions.GrantTypes.ClientCredentials
                }
        }, cancellationToken);

        ExportOpenIddictDevelopmentPublicKeyIfAny(Path.Combine(AppContext.BaseDirectory, "openiddict_dev_pubkey.pem"));
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    static void ExportOpenIddictDevelopmentPublicKeyIfAny(string outputPath)
    {
        try
        {
            // Search CurrentUser\My for the development certificate created by OpenIddict.
            using var store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly);

            // Look for a certificate that OpenIddict typically creates with a subject containing "OpenIddict"
            var cert = store.Certificates
                .Cast<X509Certificate2>()
                .FirstOrDefault(c => c.Subject?.IndexOf("OpenIddict", StringComparison.OrdinalIgnoreCase) >= 0
                                     || c.FriendlyName?.IndexOf("OpenIddict", StringComparison.OrdinalIgnoreCase) >= 0);

            if (cert is null)
            {
                // Nothing found; nothing to export.
                return;
            }

            using var rsa = cert.GetRSAPublicKey();
            if (rsa is null)
                return;

            // Export SubjectPublicKeyInfo (SPKI) and convert to PEM
            var spki = rsa.ExportSubjectPublicKeyInfo();
            var base64 = Convert.ToBase64String(spki, Base64FormattingOptions.InsertLineBreaks);
            var pem = $"-----BEGIN PUBLIC KEY-----\n{base64}\n-----END PUBLIC KEY-----\n";

            File.WriteAllText(outputPath, pem);
        }
        catch
        {
            // Silent fail — this helper is convenience for local debugging only.
        }
    }
}