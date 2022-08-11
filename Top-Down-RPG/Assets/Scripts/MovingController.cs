using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingController : MonoBehaviour{
    
    bool isMoveing = false;
    public float MoveTime = 0.5f;
    private BoxCollider2D Collider;
    public LayerMask BlockingLayer;

    protected virtual void Start(){
        Collider = GetComponent<BoxCollider2D>();
    }

    protected void Move(int x, int y, out RaycastHit2D hit){
        Vector2 StartPos = transform.position;
        Vector2 EndPos = StartPos + new Vector2(x, y);
        Collider.enabled = false;

        hit = Physics2D.Linecast(StartPos, EndPos, BlockingLayer);

        Collider.enabled = true;
        
        if (!isMoveing && hit.transform == null){
            StartCoroutine(SmoothMove(EndPos));
        }
    }

    protected IEnumerator SmoothMove(Vector3 EndPos){
        
        isMoveing = true;
        var RemainingDis = (transform.position - EndPos).sqrMagnitude;

        while(RemainingDis > float.Epsilon){
            var NewPos = Vector2.MoveTowards(transform.position, EndPos, Time.deltaTime/MoveTime);
            transform.position = NewPos;
            RemainingDis = (transform.position - EndPos).sqrMagnitude;
            yield return null;
        }

        isMoveing = false;
    }

    protected virtual void AttemptMove<T>(int x, int y)
    {
        RaycastHit2D hit;
        Move(x, y, out hit);   
    }

}
