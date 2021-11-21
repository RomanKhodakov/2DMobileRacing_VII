using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeItem", menuName = "UpgradeItem")]
public class UpgradeItemConfig : ScriptableObject
{
    [SerializeField] private ItemConfig _itemConfig;

    [SerializeField] private UpgradeType _upgradeType;

    [SerializeField] private int _valueUpgrade;
    
    [SerializeField] private SpriteRenderer _itemSpriteRenderer;

    public ItemConfig ItemConfig => _itemConfig;

    public UpgradeType UpgradeType => _upgradeType;

    public int ValueUpgrade => _valueUpgrade;
    
    public SpriteRenderer ItemSpriteRenderer => _itemSpriteRenderer;
}