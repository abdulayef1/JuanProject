using WebUI.Areas.Admin.ViewModels;

namespace WebUI.Utilities
{
	public static class Extenstion
	{
		public static bool CheckFileSize(this IFormFile file,int fileSize)
		{
			return file.Length/1024<fileSize;
		}

		public static bool CheckFileFormat(this IFormFile file,string fileFormat)
		{
			return file.ContentType.Contains(fileFormat);
		}

		public static string CopyFilee(this IFormFile file,string wwwroot,params string[] folders)
		{

            string path = wwwroot;
			foreach (var folder in folders)
			{
				path = Path.Combine(path, folder);
			}

            string uniquefileName = Guid.NewGuid().ToString() + file.FileName;
			string fulPath = Path.Combine(path, uniquefileName);
            using (FileStream fileStream = File.Open(fulPath, FileMode.Create))
			{
                file.CopyTo(fileStream);
            }
            return uniquefileName;
		}


	}
}
