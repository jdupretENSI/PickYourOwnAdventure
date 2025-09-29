using System;
using System.Collections.Generic;
using JetBrains.Annotations;

[Serializable]
public class Thumbnail {
    public Thumbnail([NotNull] string id, [NotNull] string imageName, [NotNull] string sfxName,
        [NotNull] string musicName, [NotNull] string description, [NotNull] List<Choice> choices)
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));
        ImageName = imageName ?? throw new ArgumentNullException(nameof(imageName));
        SfxName = sfxName ?? throw new ArgumentNullException(nameof(sfxName));
        MusicName = musicName ?? throw new ArgumentNullException(nameof(musicName));
        Description = description ?? throw new ArgumentNullException(nameof(description));
        Choices = choices ?? throw new ArgumentNullException(nameof(choices));
    }

    // The Id of the thumbnail for reference
    public string Id;
    // The name of the image of the thumbnail inside the folder with the extension
    // For example : "toto.png"
    public string ImageName;
    // The name of the sfx of the thumbnail inside the folder with the extension
    // For example : "toto.flac"
    public string SfxName;
    // The name of the music of the thumbnail inside the folder with the extension
    // For example : "toto.ogg"
    public string MusicName;
    // The description of the thumbnail, scenery description
    public string Description;
    // The list of the choices for this thumbnail
    public List<Choice> Choices;
}