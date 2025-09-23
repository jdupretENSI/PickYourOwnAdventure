using System;
using System.Collections.Generic;
using JetBrains.Annotations;

[Serializable]
public class Story {
    public string StartingThumbnailId;
    public List<Thumbnail> Thumbnails;

    public Story([NotNull] string startingThumbnailId, [NotNull] List<Thumbnail> thumbnails)
    {
        StartingThumbnailId = startingThumbnailId ?? throw new ArgumentNullException(nameof(startingThumbnailId));
        Thumbnails = thumbnails ?? throw new ArgumentNullException(nameof(thumbnails));
    } 
}



