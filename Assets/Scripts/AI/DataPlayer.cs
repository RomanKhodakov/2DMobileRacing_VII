using System.Collections.Generic;
using UnityEngine;

public class DataPlayer
{
    private readonly List<IEnemy> _enemies = new List<IEnemy>();

    public void Attach(IEnemy enemy)
    {
        _enemies.Add(enemy);
        Debug.Log($"Attached");
    }

    public void Detach(IEnemy enemy)
    {
        _enemies.Remove(enemy);
    }

    protected void Notifier(DataType dataType)
    {
        foreach(var enemy in _enemies)
            enemy.Update(this, dataType);
    }
}