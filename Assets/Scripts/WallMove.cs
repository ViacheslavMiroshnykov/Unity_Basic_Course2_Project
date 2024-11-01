using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DisallowMultipleComponent]
public class WallMove : MonoBehaviour
{
    [SerializeField] Vector3 movePosition;
    [SerializeField] float moveSpeed;
    [SerializeField] [Range (0,1)] float moveProgress;
    Vector3 stopMove = new Vector3 (-82.6999969f,-0.899999976f,53.5999985f);

    Vector3 startPosition;

    bool isActive = true;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {   
        if(isActive == true)
        {
          Move();
        } 
        
        if(transform.position.x >= stopMove.x )
        {
            isActive = false;

        }
        
    }

    private void Move()
    {
        moveProgress = Mathf.PingPong(Time.time * moveSpeed, 1);
        Vector3 offset = movePosition * moveProgress;
        transform.position = startPosition + offset;
    }
}
