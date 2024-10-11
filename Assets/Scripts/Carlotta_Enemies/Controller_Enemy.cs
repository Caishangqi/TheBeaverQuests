using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class EnemyController:MonoBehaviour
{
    private EnemyModel model;

    private EnemyView view;
    public float health = 100;

    public float speed = 20;
    
    // Start is called before the first frame update
    private void Start()
    {
        gameObject.tag = "Mushroom";
        //Initialize model and view
        //model = new EnemyModel(float health, speed);

        if (view == null)
        {
            Debug.LogError("EnemyView component is missing from the GameObject.");
        }
        }

    // //     private void Update()
    // {
    //     if (model.isAlive)
    //     {
    //         Move();
    //     }
    // }

    private void Move()
    {
        // Example movement logic
        model.position += Vector3.forward * model.speed * Time.deltaTime;
        view.UpdatePosition(model.position);

        // Play a movement animation (if available)
        view.PlayAnimation("Walk");
    }

    public void TakeDamage(float damage)
    {
        model.TakeDamage(damage);
        if (!model.isAlive)
        {
            view.MarkAsDead();
        }
    }

    // Example method to attack a target
    public void Attack(GameObject target)
    {
        // Your attack logic
        view.PlayAnimation("Attack");
    }
}
