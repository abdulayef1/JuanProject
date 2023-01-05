namespace WebUI.Utilities;

public static class Helper
{
    public static bool DelteFile(string wwwroot, params string[] folders)
    {
        string path = wwwroot;
        foreach (var folder in folders)
        {
            path = Path.Combine(path, folder);
        }
        try
        {
            // Check if file exists with its full path    
            if (File.Exists(path))
            {
                // If file found, delete it    
                File.Delete(path);
                return true;
            }
        }
        catch (IOException ioExp)
        {
            throw;
        }
        return false;
    }
}
