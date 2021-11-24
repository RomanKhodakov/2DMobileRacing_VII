using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilitiesDataSource", menuName = "AbilitiesDataSource")]
public class AbilitiesDataSource : ScriptableObject
{
    [SerializeField] private AbilityItemConfig[] abilitiesItemsConfigs;

    public IEnumerable<AbilityItemConfig> AbilitiesItemsConfigs => abilitiesItemsConfigs;
}