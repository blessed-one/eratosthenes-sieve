namespace ReshetoMAUI;

public static class ThemeManager
{
    private static readonly IDictionary<string, ResourceDictionary> _themes = new Dictionary<string, ResourceDictionary>()
    {
        [nameof(Resources.Themes.Default)] = new Resources.Themes.Default(),
        [nameof(Resources.Themes.Nature)] = new Resources.Themes.Nature(),
        [nameof(Resources.Themes.Fire)] = new Resources.Themes.Fire()
    };

    public static string? SelectedTheme { get; set; } = nameof(ReshetoMAUI.Resources.Themes.Default);

    public static async Task SetTheme(string themeName)
    {
        if (SelectedTheme == themeName)
        {
            return;
        }

        var themeToBeApplied = _themes[themeName];

        await Task.Run(() =>
        {
            Application.Current!.Dispatcher.Dispatch(() =>
            {
                Application.Current.Resources.MergedDictionaries.Clear();
                Application.Current.Resources.MergedDictionaries.Add(themeToBeApplied);

                SelectedTheme = themeName;
            });
        });
    }
}