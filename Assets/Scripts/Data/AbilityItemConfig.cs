using UnityEngine;

[CreateAssetMenu(fileName = "AbilitiesItemsConfigs", menuName = "AbilitiesItemsConfigs")]
public class AbilityItemConfig : ScriptableObject
{
    [SerializeField] private ItemConfig _itemConfig;

    [SerializeField] private AbilityView _abilityView;

    [SerializeField] private AbilityType _abilityType;

    [SerializeField] private float _value;

    public int Id => _itemConfig.Id;

    public float Value => _value;

    public AbilityType AbilityType => _abilityType;

    public AbilityView AbilityView => _abilityView;

    public ItemConfig ItemConfig => _itemConfig;
}