using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private BoardManager BM;
    private int level = 1;

    public bool YourTurn = true;
    public float WaitTime = 0.5f;

    public static GameManager instance;

    private List<EnemyController> Enemies = new List<EnemyController>();
    // Start is called before the first frame update
    void Awake()
    {
        BM = GetComponent<BoardManager>();
        BM.SetUpScene(level);
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (YourTurn)
            return;
        StartCoroutine(EnemiesTurn(WaitTime));
    }

    public IEnumerator EnemiesTurn(float WaitTime)
    {
        yield return new WaitForSeconds(WaitTime);
        for (int i = 0; i < Enemies.Count; i++)
        {
            Enemies[i].MoveEnemy();
            yield return new WaitForSeconds(1);
        }
        YourTurn = true;
    }

    public void AddEnemy(EnemyController enemy)
    {
        Enemies.Add(enemy);
    }

}
