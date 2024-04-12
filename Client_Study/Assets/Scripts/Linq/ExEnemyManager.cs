using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExEnemyManager : MonoBehaviour
{
    public List<ExEnemy> enemies = new List<ExEnemy>();


    private void Start()
    {
        var sortedEnemies = enemies.OrderBy(enemy => enemy.damage);

        foreach (var enemy in sortedEnemies)
        {
            print("Sorted Enemy : " + enemy.gameObject.name + " Damage : " + enemy.damage);
        }

        float maxDistance = 10f;
        var closeEnemies = enemies.Where(enemy =>
            Vector3.Distance(enemy.transform.position, transform.position) < maxDistance);

        foreach (var enemy in closeEnemies)
        {
            print("Close Enemies : " + enemy.gameObject.name);
        }
    }
}
