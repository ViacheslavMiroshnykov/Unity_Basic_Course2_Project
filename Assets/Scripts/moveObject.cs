using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class moveObject : MonoBehaviour
{
    [SerializeField] Vector3 movePosition;
    [SerializeField] float moveSpeed;
    [SerializeField] [Range (0,1)] float moveProgress; 

    Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        moveProgress = Mathf.PingPong (Time.time*moveSpeed, 1);
        Vector3 offset = movePosition * moveProgress;
        transform.position = startPosition + offset;
    }
}
