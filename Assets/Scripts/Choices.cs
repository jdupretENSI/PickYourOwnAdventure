using System;
using System.Collections.Generic;
using JetBrains.Annotations;

[Serializable]
public class Choice {
    public Choice([NotNull] string description, [NotNull] string thumbnailLinkId, [NotNull] List<string> neededItemsId,
        [NotNull] List<string> givenItemsId, [NotNull] List<string> takenItemsId)
    {
        Description = description ?? throw new ArgumentNullException(nameof(description));
        ThumbnailLinkId = thumbnailLinkId ?? throw new ArgumentNullException(nameof(thumbnailLinkId));
        NeededItemsId = neededItemsId ?? throw new ArgumentNullException(nameof(neededItemsId));
        GivenItemsId = givenItemsId ?? throw new ArgumentNullException(nameof(givenItemsId));
        TakenItemsId = takenItemsId ?? throw new ArgumentNullException(nameof(takenItemsId));
    }

    // The content of the choice (clicked label)
    public string Description;
    // The id of the thumbnail this choice sends to
    public string ThumbnailLinkId;
    // The list of item need to be able to select this choice
    public List<string> NeededItemsId;
    // The list of item given when selecting this choice
    public List<string> GivenItemsId;
    // The list of item remove form the inventory when selecting this choice
    public List<string> TakenItemsId;
}