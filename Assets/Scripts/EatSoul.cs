using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatSoul : MonoBehaviour
{
    private BoxCollider eatCollider;
    private MonsterBody monsterBody;
    private Timer timer;

    // Start is called before the first frame update
    void Start()
    {
        eatCollider = GetComponent<BoxCollider>();
        monsterBody = transform.parent.gameObject.GetComponent<MonsterBody>();
        timer = GameObject.Find("GameController").GetComponent<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // eat event
    private void OnTriggerEnter(Collider other) {
        if(timer.isDay){
            if(other.tag == "Player"){
                monsterBody.state = "dead";
            }
        }
    }
}
