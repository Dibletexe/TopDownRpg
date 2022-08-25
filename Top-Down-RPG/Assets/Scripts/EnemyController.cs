using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MovingController
{
    private Transform player;
    public bool SkipEnemyTurn = false;
    public int TakeFood = 10;
    protected override void Start()
    {
        GameManager.instance.AddEnemy(this);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        base.Start();
    }
    public void MoveEnemy()
    {
        if (!SkipEnemyTurn)
        {
            if (player.position.x > transform.position.x)
            {
                AttemptMove<PlayerController>(1, 0);
            }
            else if (player.position.x < transform.position.x)
            {
                AttemptMove<PlayerController>(-1, 0);
            }
            else if (player.position.y > transform.position.y)
            {
                AttemptMove<PlayerController>(0, 1);
            }
            else if (player.position.y < transform.position.y)
            {
                AttemptMove<PlayerController>(0, -1);
            }
        }
        SkipEnemyTurn = !SkipEnemyTurn;
    }

    protected override void OnCantMove<T>(T Component)
    {
        if (Component is PlayerController)
        {
            (Component as PlayerController).LoseFood(TakeFood);
        }
    }
}
