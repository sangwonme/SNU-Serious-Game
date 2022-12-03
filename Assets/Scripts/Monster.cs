using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{

    // public params
    public float xSpeed;
    public string state;
    // get other components
    private Timer timer;
    private Rigidbody theRB;
    private BoxCollider boxCollider;
    private SpriteRenderer sprite;
    // going up & down
    float dayY = -2.06f;
    float nightY = 0.0f;
    float ySpeed = 0.0f;
    float prevY = 0.0f;
    // state change
    float stateChangeTimeLeft = 0.0f;
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
        state = "idle";
        timer = GameObject.Find("GameController").GetComponent<Timer>();
        theRB = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        sprite = GetComponent<SpriteRenderer>();
        setRandomTimeLeft();
        prevY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        // going up down
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
        }
        // update prevY
        prevY = transform.position.y;


        switch(state){
            case "idle":
                break;
            case "right":
                if(!timer.isDay){
                    theRB.velocity = new Vector3(xSpeed, 0, 0);
                    sprite.flipX = true;
                }
                break;
            case "left":
                if(!timer.isDay){
                    theRB.velocity = new Vector3(-xSpeed, 0, 0);
                    sprite.flipX = false;
                }
                break;
            case "dead":
                boxCollider.enabled = false;
                sprite.enabled = false;
                break;
            default:
                break;
        }
        
        // direction set
        if(
            (state == "left" || state == "right" || state == "idle") &&
            prevY == transform.position.y && 
            transform.position.y > 0.2f && 
            !timer.isDay
        ){
            stateChangeTimeLeft -= Time.deltaTime;
            if(stateChangeTimeLeft < 0){
                setRandomTimeLeft();
                setRandomState();
            }
        }

    }

}
