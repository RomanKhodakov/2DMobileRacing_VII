using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeItemConfigDataSource", menuName = "UpgradeItemConfigDataSource")]
public class UpgradeItemConfigDataSource : ScriptableObject
{
    [SerializeField] private UpgradeItemConfig[] upgradeItemConfigs;

    public IEnumerable<UpgradeItemConfig> UpgradeItemConfigs => upgradeItemConfigs;
}
