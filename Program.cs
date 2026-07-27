using System.Diagnostics;
using System.Windows.Forms;

string path = "";

if (args.Length == 0)
{
    MessageBox.Show("No URL argument provided.", "Local File Link Error");
    return 1;
}

string url = args[0];

try
{
    // Strip 'localfile://' or 'localfile:///' prefix (mirrors the -replace '^localfile:///?','' regex)
    path = System.Text.RegularExpressions.Regex.Replace(url, "^localfile:///?", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

    // URL-decode (mirrors [Uri]::UnescapeDataString)
    path = Uri.UnescapeDataString(path);

    // Convert forward slashes to backslashes
    path = path.Replace('/', '\\');

    var psi = new ProcessStartInfo
    {
        FileName = path,
        UseShellExecute = true // equivalent of Start-Process default behavior
    };
    Process.Start(psi);
}
catch (Exception ex)
{
    MessageBox.Show($"Could not open: {path}\n\n{ex}", "Local File Link Error");
    return 1;
}

return 0;