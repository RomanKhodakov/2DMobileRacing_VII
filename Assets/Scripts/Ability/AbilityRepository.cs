using System.Collections.Generic;
using UnityEngine;

public class AbilityRepository : BaseController, IRepository<int, IAbility>
{
    private readonly Dictionary<int, IAbility> _abilitiesMapById = new Dictionary<int, IAbility>();
    
    public IReadOnlyDictionary<int, IAbility> Collection => _abilitiesMapById;

    public AbilityRepository(List<AbilityItemConfig> abilityItemConfigs)
    {
        PopulateItems(abilityItemConfigs);
    }

    private void PopulateItems(List<AbilityItemConfig> configs)
    {
        foreach (var config in configs)
        {
            if (_abilitiesMapById.ContainsKey(config.Id))
                continue;

            _abilitiesMapById.Add(config.Id, CreateAbility(config));
        }
    }

    private IAbility CreateAbility(AbilityItemConfig config)
    {
        switch (config.AbilityType)
        {
            case AbilityType.Bomb:
                return new BombAbility(config);
            default:
                Debug.Log("Not type ability");
                return null;
        }
    }

    protected override void OnDispose()
    {
        _abilitiesMapById.Clear();
    }
}
