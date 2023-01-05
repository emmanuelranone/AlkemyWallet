namespace AlkemyWallet.Core.Helper;

public class LaunchUrl
{
    public static string GetApplicationUrl()
    {
        string launchProfile = "AlkemyWallet";

        string launchUrl = string.Empty;

        var launchSettings = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("Properties\\launchSettings.json")
            .Build();

        launchUrl = launchSettings.GetValue<string>($"Profiles:{launchProfile}:ApplicationUrl");
        string[] urls = launchUrl.Split(';');
        launchUrl = urls[0];
        return launchUrl;
    }
}
