using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MovingController{

    private int food = 100;
    public int foodPerItem = 10;
    public Text foodText;
    public int FoodLost = 5;

    protected override void AttemptMove<T>(int x, int y)
    {
            LoseFood(FoodLost);
            base.AttemptMove<T>(x, y);
            GameManager.instance.YourTurn = false;
    }

    public void Update(){
        if (!GameManager.instance.YourTurn)
            return;
        var Horizontal = (int)Input.GetAxisRaw("Horizontal");
        var Vertical = (int)Input.GetAxisRaw("Vertical");
        

        if (Horizontal != 0){
            Vertical = 0;
        }

        if (Horizontal != 0 || Vertical != 0){
            if (food <= 0)
            {
                foodText.text = "Can't Move - Game Over!";
            }
            else
            {
                    AttemptMove<Transform>(Horizontal, Vertical);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Food")
        {
            food += foodPerItem;
            foodText.text = $"+ {foodPerItem} food gained - Food: {food}";
            Destroy(collision.gameObject);
        }else if(collision.tag == "Exit")
        {
            //Make new level
            enabled = false;
            Invoke("NewLvl", 1f);


        }
    }

    private void NewLvl()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public void LoseFood(int Amount)
    {
        food -= Amount;
        foodText.text = $"-{Amount} Food - {food}";
    }

    protected override void OnCantMove<T>(T Component)
    {
        throw new System.NotImplementedException();
    }
}
