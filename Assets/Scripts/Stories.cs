using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;

[Serializable]
public class Story
{
    public Story([NotNull] string storyName = null, [NotNull] string imageName = null,
        [NotNull] string startingThumbnailId = null, [NotNull] List<Thumbnail> thumbnails = null,
        [NotNull] List<Item> items = null)
    {
        StoryName = storyName ?? throw new ArgumentNullException(nameof(storyName));
        ImageName = imageName ?? throw new ArgumentNullException(nameof(imageName));
        StartingThumbnailId = startingThumbnailId ?? throw new ArgumentNullException(nameof(startingThumbnailId));
        Thumbnails = thumbnails ?? throw new ArgumentNullException(nameof(thumbnails));
        Items = items ?? throw new ArgumentNullException(nameof(items));
    }

    // The named of the story displayed
    public string StoryName;
    // The name of the image representative of the story (banner-like)
    public string ImageName;
    // The starting id of the first thumbnail
    public string StartingThumbnailId;
    // The list containing all thumbnail of the story
    public List<Thumbnail> Thumbnails;
    // The list containing all items of the story
    public List<Item> Items;
}


