using System;
using JetBrains.Annotations;

[Serializable]
public class Choice {
    public string Description;
    public string ThumbnailLinkId;

    public Choice([NotNull] string description, [NotNull] string thumbnailLinkId)
    {
        Description = description ?? throw new ArgumentNullException(nameof(description));
        ThumbnailLinkId = thumbnailLinkId ?? throw new ArgumentNullException(nameof(thumbnailLinkId));
    }
}