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
    }
}
