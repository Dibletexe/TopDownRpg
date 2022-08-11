using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MovingController{

    private int food = 100;
    public int foodPerItem = 10;
    public Text foodText;

    protected override void AttemptMove<T>(int x, int y)
    {
            food -= 5;
            foodText.text = $"Food - {food}";
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
        }
    }

    // Set YourTurn To true
}
