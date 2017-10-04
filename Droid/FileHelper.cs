using System;
using System.IO;
using ForeignKeyExample.Droid;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(FileHelper))]
namespace ForeignKeyExample.Droid
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}
