using System.Collections.Generic;
using System.IO;

namespace Readers
{
    using System;
    using Ionic.Zip;
    using System.IO.Compression;

   public static class FileUnziper
    {
       public static List<string> Files { get; set; }
 
       public static void UnzipFile(string filePath, string extractionFolderPath)
       {
           Files = new List<string>();

           using (Ionic.Zip.ZipFile zip = Ionic.Zip.ZipFile.Read(filePath))
           {
               foreach (ZipEntry zippedFile in zip)
               {
                 Files.Add(zippedFile.FileName);
                   zippedFile.Extract(extractionFolderPath, ExtractExistingFileAction.OverwriteSilently);
               }
           }
       }

       public static void ReadDirectly()
       {
           using (ZipArchive archive = System.IO.Compression.ZipFile.OpenRead("W2.zip"))
           {
               foreach (ZipArchiveEntry entry in archive.Entries)
               {
                   var reader = entry.Open();
                   while (reader.CanRead)
                   {
                       Console.WriteLine(reader.ToString());
                   }
               }
           } 
       }
    }
}
