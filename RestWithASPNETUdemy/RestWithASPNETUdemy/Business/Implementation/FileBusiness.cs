using RestWithASPNETUdemy.Model;
using System.IO;

namespace RestWithASPNETUdemy.Business.Implementation
{
    public class FileBusiness : IFileBusiness
    {
        public byte[] GetPDFFile()
        {
            string path = Directory.GetCurrentDirectory();
            var fullPath = path + "\\Other\\documento.pdf";
            return File.ReadAllBytes(fullPath);
        }
    }
}
