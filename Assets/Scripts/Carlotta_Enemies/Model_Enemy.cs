using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyModel
{
    public float health;
    public Vector3 position;
    public float speed;
    public string name;
    public bool isAlive;

    public EnemyModel(float health, Vector3 position, float speed, bool isAlive, string name)
    {
        this.health = health;
        this.position = position;
        this.speed = speed;
        this.isAlive = isAlive;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            MarkAsDead();
        }
    }

    private void MarkAsDead()
    {
        isAlive = false;
    }
}
