using System;
using JetBrains.Annotations;

[Serializable]
public class Item
{
    public Item([NotNull] string id = null, [NotNull] string itemName = null, [NotNull] string iconName = null,
        [NotNull] string description = null)
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));
        ItemName = itemName ?? throw new ArgumentNullException(nameof(itemName));
        IconName = iconName ?? throw new ArgumentNullException(nameof(iconName));
        Description = description ?? throw new ArgumentNullException(nameof(description));
    }

    // The Id of the item for reference
    public string Id;
    // The name of the item (for label)
    public string ItemName;
    // The name of the icon representative of the item in the story folder (example : sword.png)
    public string IconName;
    // The description of the item (for label)
    public string Description;
    
}