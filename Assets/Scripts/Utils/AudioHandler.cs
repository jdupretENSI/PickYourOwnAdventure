using System.IO;
using UnityEngine;

public class AudioHandler
{
    public static AudioType GetAudioType(string filePath)
    {

        string[] ExtentionTypes = new[]
        {
            ".wav",
            ".mp3",
            ".ogg",
            ".aiff",
            ".aif"
        };

        foreach (string ex in ExtentionTypes)
        {
            if (File.Exists(filePath + ex))
            {
                string extension = Path.GetExtension(filePath +ex).ToLower();
                return extension switch
                {
                    ".wav" => AudioType.WAV,
                    ".mp3" => AudioType.MPEG,
                    ".ogg" => AudioType.OGGVORBIS,
                    ".aiff" or ".aif" => AudioType.AIFF,
                    _ => AudioType.UNKNOWN
                };
            }
        }

        return default;
    }

    public static string GetAudioTypeString(string filePath)
    {
        string[] ExtentionTypes = new[]
        {
            ".wav",
            ".mp3",
            ".ogg",
            ".aiff",
            ".aif"
        };

        foreach (string ex in ExtentionTypes)
        {
            if (File.Exists(filePath + ex))
            {
                return ex;
            }
        }

        return null;
    }
    
};
