using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace MainPart
{
    class Program
    {
        static void Main(string[] args)
        {
            var pathToProject = "D:\\5 семестр\\СПП\\4\\TestGenerator\\MainPart";
            var pathToFolder = pathToProject+"\\"+"Files";
            var pathToGenerated = pathToProject + "\\" + "GeneratedFiles";
            
            if (!Directory.Exists(pathToFolder))
            {
                Console.WriteLine($"Couldn't find directory {pathToFolder}");
                return;
            }
            if (!Directory.Exists(pathToGenerated))
            {
                Directory.CreateDirectory(pathToGenerated);
            }

            var allFiles = Directory.GetFiles(pathToFolder);

            var files = from file in allFiles
                    where file.Substring(file.Length - 3) == ".cs"
                    select file;

            new Pipeline().Generate(files, pathToGenerated);
        }
    }
}
