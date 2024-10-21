using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField]float rotSpeed = 100f;
    [SerializeField]float flySpeed = 100f;
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
        Launch(); 
        Rotation();
    }
    void Launch()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up*flySpeed*Time.deltaTime);
            if (audioSourse.isPlaying == false)
            {
                audioSourse.Play();
            }
        }
        else 
        {
            audioSourse.Pause();
        }
    }
    void Rotation()
    {
        float rotationSpeed = rotSpeed * Time.deltaTime;

        rigidBody.freezeRotation = true;
        if(Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationSpeed);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationSpeed);
        }
        rigidBody.freezeRotation = false;
    }
}
