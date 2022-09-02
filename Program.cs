using System;

namespace bakf
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("You need to specify file/folder path");
            } 
            else
            {
                if (File.GetAttributes(args[0]).HasFlag(FileAttributes.Directory) || Path.GetExtension(args[0]) == ".fbak")
                {
                    Bakf.BackupFolder(args[0]);
                }
                else
                {
                    Bakf.BackupFile(args[0]);
                }
            }
        }
    }
}