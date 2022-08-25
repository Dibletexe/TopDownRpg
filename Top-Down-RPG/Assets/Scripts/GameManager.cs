using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private BoardManager BM;
    private int level = 1;

    public bool YourTurn = true;
    public float WaitTime = 0.5f;
    public bool EnemysMoving = false;

    public static GameManager instance;
    public Text LvlTxt;

    private List<EnemyController> Enemies = new List<EnemyController>();
    // Start is called before the first frame update
    void Awake()
    {
        BM = GetComponent<BoardManager>();
        if (instance == null)
        {
            InitilizeLevel();
            instance = this;
        }else if(instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
}

    // Update is called once per frame
    void Update()
    {
        if (YourTurn || EnemysMoving)
            return;
            StartCoroutine(EnemiesTurn(WaitTime));
    }

    public IEnumerator EnemiesTurn(float WaitTime)
    {
            EnemysMoving = true;
            yield return new WaitForSeconds(WaitTime);
            for (int i = 0; i < Enemies.Count; i++)
            {
                Enemies[i].MoveEnemy();
                yield return new WaitForSeconds(1);
            }
            YourTurn = true;
            EnemysMoving = false;
    }

    public void AddEnemy(EnemyController enemy)
    {
        Enemies.Add(enemy);
    }

    private void InitilizeLevel()
    {
        LvlTxt.text = $"Level - {level}";
        Enemies.Clear();
        BM.SetUpScene(level);
    }

    private static void OnSceneLoaded(Scene scene, LoadSceneMode load)
    {
        instance.level++;
        instance.InitilizeLevel();
    }

    //this is called only once, and the paramter tell it to be called only after the scene was loaded
    //(otherwise, our Scene Load callback would be called the very first load, and we don't want that)
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static public void CallbackInitialization()
    {
        //register the callback to be called everytime the scene is loaded
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
}
