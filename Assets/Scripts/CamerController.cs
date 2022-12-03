using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerController : MonoBehaviour
{

    public GameObject player;
    public float speed;
    public Vector3 distance;
    private bool isFollow;

    // Start is called before the first frame update
    void Start()
    {
        isFollow = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.V)){
            isFollow = !isFollow;
        }
    }

    private void LateUpdate() {
        if(isFollow){
            Vector3 tragetPos = new Vector3(player.transform.position.x, player.transform.position.y + distance.y, player.transform.position.z + distance.z);
            transform.position = Vector3.Lerp(transform.position, tragetPos, Time.deltaTime * speed); 
        }
        else{
            Vector3 tragetPos = new Vector3(-1, 45, -75);
            transform.position = Vector3.Lerp(transform.position, tragetPos, Time.deltaTime * speed);
        }
    }
}
