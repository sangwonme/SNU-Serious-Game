using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{

    private Timer timer;
    private Rigidbody theRB;
    private BoxCollider boxCollider;
    private SpriteRenderer sprite;
    private float dayY;
    private float nightY;
    private float ySpeed;
    public float xSpeed;
    private string state;
    private float prevY;
    private float stateChangeTimeLeft;
    bool isDead = false;
    float timeCount = 0.0f;

    void setRandomTimeLeft(){
        stateChangeTimeLeft = Random.Range(3.0f, 5.0f);
    }

    void setRandomState(){
        // if monster was moving left or right -> set idle
        if(state == "left" || state == "right"){
            state = "idle";
        }
        // else choose random direction
        else{
            float tmp = Random.Range(0.0f, 1.0f);
            if(tmp < 0.5f){
                state = "left";
            }
            else{
                state = "right";
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        timer = GameObject.Find("GameController").GetComponent<Timer>();
        theRB = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        dayY = -2.06f;
        nightY = 0.0f;
        ySpeed = 0.0f;
        setRandomState();
        setRandomTimeLeft();
        prevY = transform.position.y;
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // day
        if(timer.isDay){
            theRB.useGravity = false;
            // go down
            if(transform.position.y > dayY){
                boxCollider.enabled = false;
                theRB.velocity = new Vector3(0, ySpeed, 0);
                ySpeed -= 0.05f;
            }else{
                theRB.velocity = new Vector3(0, 0, 0);
                ySpeed = 0.0f;
            }
        }
        // night
        else{
            theRB.useGravity = true;
            // go up
            if(transform.position.y < nightY){
                theRB.velocity = new Vector3(0, ySpeed, 0);
                ySpeed += 0.05f;
            }else{
                boxCollider.enabled = true;
                ySpeed = 0.0f;
            }
            // move
            if(!isDead && prevY == transform.position.y && transform.position.y > 0.2f){
                theRB.velocity = new Vector3(
                    state=="right" ? xSpeed : 
                    state=="left" ? -xSpeed : 0
                , 0, 0);
            }
        }

        // update prevY
        prevY = transform.position.y;

        // direction set
        if(!isDead && prevY == transform.position.y && transform.position.y > 0.2f && !timer.isDay){
            stateChangeTimeLeft -= Time.deltaTime;
            Debug.Log(stateChangeTimeLeft);
            if(stateChangeTimeLeft < 0){
                setRandomTimeLeft();
                setRandomState();
                if(state == "left" || state == "right"){
                    sprite.flipX = (state == "right");
                }
            }
        }

        // is dead
        if(isDead){
            Quaternion targetRot = Quaternion.Euler(new Vector3(90.0f, 0.0f, 0.0f));
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, timeCount * 3);
            timeCount += Time.deltaTime;
        }
    }

}
