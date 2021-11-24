using UnityEngine;

public class BombAbility : IAbility
{
    public AbilityItemConfig Config { get; }

    public BombAbility(AbilityItemConfig config)
    {
        Config = config;
    }
    
    public void Apply()
    {
        var bombRigidbody = Object.Instantiate(Config.AbilityView).ObjectRigidbody2D;
        bombRigidbody.AddForce(Vector2.one, ForceMode2D.Force);
    }

}
