namespace Readers
{
    using System;
    using Ionic.Zip;

   public static class FileUnziper
    {
       public static void UnzipFile(string filePath, string extractionFolderPath)
       {
           using (ZipFile zip = ZipFile.Read(filePath))
           {
               foreach (ZipEntry zippedFile in zip)
               {
                   zippedFile.Extract(extractionFolderPath, ExtractExistingFileAction.OverwriteSilently);
               }
           }
       }
    }
}
