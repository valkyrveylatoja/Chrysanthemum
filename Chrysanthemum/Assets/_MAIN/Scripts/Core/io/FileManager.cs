using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileManager
{
    public static List<string> ReadTextFile(string filepath, bool includeBlankLines = true)
    {
        if (filepath.StartsWith('/'))
            filepath = FilePaths.root + filepath;

        List<string> lines = new List<string>();
        try
        {
            using (StreamReader sr = new StreamReader(filepath))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (includeBlankLines || !string.IsNullOrWhiteSpace(line))
                        lines.Add(line);
                }
            }
        }
        catch (FileNotFoundException ex)
        {
            Debug.LogError($"File not found: '{ex.FileName}'");
        }

        return lines;
    }

    public static List<string> ReadTextAsset(string filepath, bool includeBlankLines = true)
    {
        return null;
    }
}
