using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField]float rotSpeed = 100f;
    [SerializeField]float flySpeed = 100f;
    [SerializeField] AudioClip flySounds;
    [SerializeField] AudioClip boomSounds;
    [SerializeField] AudioClip finishSounds;
    [SerializeField] ParticleSystem flyParticles;
    [SerializeField] ParticleSystem boomParticles;
    [SerializeField] ParticleSystem finishParticles;
    Rigidbody rigidBody;
    AudioSource audioSourse;

    enum State {Playing,Dead,NextLevel};
    State state = State.Playing;

    // Start is called before the first frame update
    void Start()
    {
        state = State.Playing;
        rigidBody = GetComponent<Rigidbody>();
        audioSourse = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {   
        if  (state == State.Playing)
        {
        Launch(); 
        Rotation();
        }
    }

    void OnCollisionEnter(Collision collision)
    {

        if(state == State.Dead || state == State.NextLevel)
        {
            return;
        }

        switch(collision.gameObject.tag)
        {
            case "Friendly": 
                print("Friendly");
                break;
            case "Finish":
                Finish();
                break;
            case "Battery":
                print("Battery");
                break;
            default:
                Lose();
                break;
        }
    }
    void Finish()
    {
        state = State.NextLevel;
        audioSourse.Stop();
        audioSourse.PlayOneShot(finishSounds);
        finishParticles.Play();
        Invoke("LoadNextLevel",2f);
    }
    void Lose()
    {
        state = State.Dead;
        audioSourse.Stop();
        flyParticles.Stop();
        audioSourse.PlayOneShot(boomSounds, 0.2f);
        boomParticles.Play();
        Invoke("LoadFirstLevel",2f);
    }
    void LoadNextLevel() // Finish
    {
        SceneManager.LoadScene("Level 2");
    }

    void LoadFirstLevel() // Lose
    {
        SceneManager.LoadScene("Level 1");
    }

    void Launch()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up*flySpeed*Time.deltaTime);
            if (audioSourse.isPlaying == false)
            {
            audioSourse.PlayOneShot(flySounds);
            flyParticles.Play();
            }
        }
        else 
        {
            audioSourse.Pause();
            flyParticles.Stop();
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
