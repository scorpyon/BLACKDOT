using System;
using System.IO;

namespace FileManagement
{
    public class FileManager : IFileManager
    {
        // Normally we would make this a config or something similar and it wouldn't be hard coded like this! 
        //But this is for the purpose of this prototype
        private const string FileStoragePath = @"C:\JsonFile.txt";

        // For the purposes of this exercise, I am storing the file directly to a predecided place on the C drive. 
        // A preferred method would be to implement a Save-Dialog box to allow the user to decide where to save / load, etc.

        public void SaveToFile(string json)
        {
            try
            {
                // Check the file is in the right place
                if (!File.Exists(FileStoragePath))
                {
                    File.Create(FileStoragePath);
                }
                // Read the file data
                File.WriteAllText(FileStoragePath, json);
            }
            catch (Exception e)
            {
                // We would bubble up error to the Error Handler (if we had one) - but for now, let's just output if anything went wrong!
                Console.WriteLine(e.Message);
            }
        }

        public string LoadFile()
        {
            try
            {
                // Check that the file exists
                if (File.Exists(FileStoragePath))
                {
                    return File.ReadAllText(FileStoragePath);
                }
                //In case we haven't made the file, let's let the user know here.
                Console.WriteLine("Error - the file doesn not appear to currently exist!");
            }
            catch (Exception e)
            {
                // As with the above Error handling
                Console.WriteLine(e.Message);
            }
            return null;
        }
    }
}
