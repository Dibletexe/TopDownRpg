using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MovingController
{
    protected override void Start()
    {
        GameManager.instance.AddEnemy(this);
        base.Start();
    }

    void Update()
    {
        
    }

    public void MoveEnemy()
    {
        AttemptMove<PlayerController>(0, -1);
    }
}
