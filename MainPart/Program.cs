using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MainPart
{
    class Program
    {
        static void Main(string[] args)
        {
            var pathToFolder = "D:\\5 семестр\\СПП\\4\\TestGenerator\\MainPart\\Files";
            var pathToGenerated = @"D:\5 семестр\СПП\4\TestGenerator\GeneratedTests\GeneratedFiles";
            
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

            Task task =  new Pipeline().Generate(files, pathToGenerated);
            //task.Wait();
            Thread.Sleep(2000);
            Console.WriteLine("end.");
        }
    }
}
