using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bakf
{
    internal class Bakf
    {
        static public bool BackupFile(string originalFilePath)
        {
            try
            {
                if (!File.Exists(originalFilePath))
                {
                    Console.WriteLine("Specified file do not exists!");

                    return false;
                }

                string filePath = Path.GetExtension(originalFilePath) == ".bak" ? originalFilePath.Substring(0, originalFilePath.Length - 4) : $"{originalFilePath}.bak";

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                File.Copy(originalFilePath, filePath);
                Console.WriteLine($"Success! {originalFilePath} --> {filePath}");

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"The process failed: {e.ToString()}");

                return false;
            }
        }

        static public bool BackupFolder(string originalFolderPath)
        {
            try
            {
                if (Path.GetExtension(originalFolderPath) == ".fbak")
                {
                    if (!File.Exists(originalFolderPath))
                    {
                        Console.WriteLine("Specified file do not exists!");

                        return false;
                    }

                    if (Directory.Exists(Path.Combine(Path.GetDirectoryName(originalFolderPath) + Path.DirectorySeparatorChar, Path.GetFileName(originalFolderPath).Replace(".fbak", ""))))
                        Directory.Delete(Path.Combine(Path.GetDirectoryName(originalFolderPath) + Path.DirectorySeparatorChar, Path.GetFileName(originalFolderPath).Replace(".fbak", "")), true);

                    ZipFile.ExtractToDirectory(Path.Combine(Path.GetDirectoryName(originalFolderPath) + Path.DirectorySeparatorChar, Path.GetFileName(originalFolderPath)), Path.GetDirectoryName(originalFolderPath) + Path.DirectorySeparatorChar);
                    Console.WriteLine($"Success! {Path.Combine(Path.GetDirectoryName(originalFolderPath) + Path.DirectorySeparatorChar, Path.GetFileName(originalFolderPath))} --> {Path.GetDirectoryName(originalFolderPath) + Path.DirectorySeparatorChar}");
                }
                else
                {
                    if (!Directory.Exists(originalFolderPath))
                    {
                        Console.WriteLine("Specified folder do not exists!");

                        return false;
                    }

                    if (File.Exists($"{Path.GetDirectoryName(originalFolderPath)}.fbak"))
                        File.Delete($"{Path.GetDirectoryName(originalFolderPath)}.fbak");

                    ZipFile.CreateFromDirectory(originalFolderPath, $"{Path.GetDirectoryName(originalFolderPath)}.fbak", CompressionLevel.NoCompression, true);
                    Console.WriteLine($"Success! {originalFolderPath} --> {Path.GetDirectoryName(originalFolderPath)}.fbak");
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"The process failed: {e.ToString()}");

                return false;
            }
        }
    }
}
