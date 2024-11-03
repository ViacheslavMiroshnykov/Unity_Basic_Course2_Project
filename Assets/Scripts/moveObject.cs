using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DisallowMultipleComponent]
public class MoveObject : MonoBehaviour
{
    [SerializeField] Vector3 movePosition;
    [SerializeField] float moveSpeed;
    [SerializeField] [Range (0,1)] float moveProgress; 

    Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        moveProgress = 0; // Сброс значения
    }

    // Update is called once per frame
    void Update()
    {   
        Move();
    }

    private void Move()
    {
        moveProgress += Time.deltaTime * moveSpeed;
        
        if (moveProgress > 1)
        {
          moveProgress = 1;
        }
    
        Vector3 offset = movePosition * moveProgress;
        transform.position = startPosition + offset;
    }
}