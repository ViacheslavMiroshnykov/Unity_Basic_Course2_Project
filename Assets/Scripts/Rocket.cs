using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody;
    AudioSource audioSourse;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSourse = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    void ProcessInput()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up);
            if (audioSourse.isPlaying == false)
            {
                audioSourse.Play();
            }
        }
        else 
        {
            audioSourse.Pause();
        }
        if(Input.GetKey(KeyCode.A))
        {
            transform.Rotate(new Vector3(0,0,1));
        }
        else if(Input.GetKey(KeyCode.D))
        {
            transform.Rotate(new Vector3(0,0,-1));
        }
    }
}
