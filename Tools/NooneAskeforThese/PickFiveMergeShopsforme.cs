/*
name: GrabFiveRandomMerges
description: This class provides functionality to randomly select up to five filenames from a specified directory within the user's "Documents" folder. It writes the selected filenames to a temporary markdown file and opens the file in the default associated application.
tags: file management, random selection, markdown, temporary file, user directory
*/
//cs_include Scripts/CoreBots.cs
//cs_include Scripts/CoreStory.cs
//cs_include Scripts/CoreAdvanced.cs
//cs_include Scripts/CoreFarms.cs
//cs_include Scripts/CoreDailies.cs
using Skua.Core.Interfaces;
using Skua.Core.Models.Items;
using Skua.Core.Models.Monsters;
using Skua.Core.Models.Quests;

public class PickFiveMergeShopsforme
{
    private IScriptInterface Bot => IScriptInterface.Instance;
    private CoreBots Core => CoreBots.Instance;

    public void ScriptMain(IScriptInterface Bot)
    {
        Core.SetOptions();

        Lottery();

        Core.SetOptions(false);
    }

    public void Lottery()
    {
        try
        {
            // Get the "Documents" folder path dynamically
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string sourceDirectory = Path.Combine(documentsPath, "Skua", "Scripts", "Other", "MergeShops");

            // Get all files from the source directory
            string[] files = Directory.GetFiles(sourceDirectory);

            // Log the total number of files
            Console.WriteLine($"Total number of merges in the folder: {files.Length}");

            // Check if the source directory contains any files
            if (files.Length == 0)
            {
                Console.WriteLine("No files found in the source directory.");
                return;
            }

            // If fewer than 5 files exist, just take what is available
            int fileCount = Math.Min(files.Length, 5);

            // Randomly select up to 5 files
            Random random = new Random();
            string[] randomFiles = files.OrderBy(x => random.Next()).Take(fileCount).ToArray();

            // Create a temporary file with .md extension
            string tempFilePath = Path.Combine(Path.GetTempPath(), "RandomFiles.md");

            // Write the selected filenames and total count to the temporary file
            using (StreamWriter writer = new StreamWriter(tempFilePath))
            {
                writer.WriteLine("# Randomly Selected Files");
                writer.WriteLine($"Total number of merges in the folder: {files.Length}");
                foreach (string file in randomFiles)
                {
                    writer.WriteLine(Path.GetFileName(file));
                }
            }

            // Log the location of the temp file
            Console.WriteLine($"File written to: {tempFilePath}");

            // Open the temporary file in the default associated application
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
            {
                FileName = tempFilePath,
                UseShellExecute = true
            });
        }
        catch (Exception ex)
        {
            // Log any exceptions
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

}



