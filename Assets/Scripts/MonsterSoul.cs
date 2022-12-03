using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSoul : MonoBehaviour
{
    private GameObject monsterBody;
    private string state;
    public Light surroundLight;
    public Light nuclearLight;
    public SphereCollider collider;
    private Rigidbody theRB;
    bool goBright = true;

    // Start is called before the first frame update
    void Start()
    {
        monsterBody = transform.parent.transform.GetChild(0).gameObject;
        updateMonsterState();
        theRB = GetComponent<Rigidbody>();
    }

    void setLight(float intensity){
        surroundLight.intensity = intensity;
        nuclearLight.intensity = intensity * 5;
    }

    void updateMonsterState(){
        state = monsterBody.GetComponent<Monster>().state;
    }

    float getLight(){
        return surroundLight.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        updateMonsterState();
        if(state == "dead"){
            // blinking
            if(getLight() > 2.0f) goBright = false;
            else if(getLight() < 1.0f) goBright = true;
            Debug.Log(goBright);
            // set intensity
            if(goBright) setLight(getLight()+0.01f);
            else setLight(getLight()-0.01f);
            // light pos
            transform.position = new Vector3(transform.position.x, 0.06f, transform.position.z);
            // check trigger
            Debug.Log(collider.isTrigger);
        }else{
            // turn off
            setLight(0.0f);
            // light pos
            transform.position = monsterBody.transform.position;
        }

    }
}
