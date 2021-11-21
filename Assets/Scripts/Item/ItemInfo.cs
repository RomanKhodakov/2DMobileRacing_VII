using UnityEngine;

public struct ItemInfo
{
    public string Title { get; set; }

    public string Description { get; set; }

    public UpgradeType UpgradeType { get; set; }

    public int ValueUpgrade { get; set; }
    
    public SpriteRenderer SpriteRenderer { get; set; }
}
