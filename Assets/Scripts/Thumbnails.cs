using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[Serializable]
public class Thumbnail {
    public string Id;
    public Sprite Image;
    public string Description;
    public List<Choice> Choices;

    public Thumbnail([NotNull] string id, [NotNull] Sprite image, [NotNull] string description,
        [NotNull] List<Choice> choices)
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));
        Image = image ?? throw new ArgumentNullException(nameof(image));
        Description = description ?? throw new ArgumentNullException(nameof(description));
        Choices = choices ?? throw new ArgumentNullException(nameof(choices));
    }
}
