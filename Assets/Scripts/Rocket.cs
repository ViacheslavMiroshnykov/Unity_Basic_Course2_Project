using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Rocket : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI energyText;
    [SerializeField] float energyTotal = 100;
    [SerializeField] int energyApply = 10;
    [SerializeField] float rotSpeed = 100f;
    [SerializeField] float flySpeed = 100f;
    [SerializeField] AudioClip flySounds;
    [SerializeField] AudioClip boomSounds;
    [SerializeField] AudioClip finishSounds;
    [SerializeField] ParticleSystem flyParticles;
    [SerializeField] ParticleSystem boomParticles;
    [SerializeField] ParticleSystem finishParticles;
    Rigidbody rigidBody;
    AudioSource audioSourse;
    bool collisionOff = false;
    public bool IsDead()
{
    return state == State.Dead;
}

    enum State {Playing,Dead,NextLevel};
    State state = State.Playing;

    // Start is called before the first frame update
    void Start()
    {
        state = State.Playing;
        rigidBody = GetComponent<Rigidbody>();
        audioSourse = GetComponent<AudioSource>();
        energyText.text = energyTotal.ToString();
    }

    // Update is called once per frame
    void Update()
    {   
        if (Input.GetKeyDown(KeyCode.Escape))
        {
        Application.Quit();
        }
        if  (state == State.Playing)
        {
        Launch(); 
        Rotation();
        }
        DebugKeys();
    }

    void DebugKeys()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if(Input.GetKeyDown(KeyCode.O))
        {
            collisionOff = !collisionOff;
        }
    }

    void OnCollisionEnter(Collision collision)
    {

      if(state == State.Dead || state == State.NextLevel || collisionOff)
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
                PlusEnergy(100, collision.gameObject);
                break;
            default:
                Lose();
                break;
        }
    }

    void PlusEnergy(int energyToAdd, GameObject batteryObj)
    {
        batteryObj.GetComponent<SphereCollider>().enabled = false;
        energyTotal += energyToAdd;
        energyText.text = energyTotal.ToString();
        Destroy(batteryObj);
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
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        int nextLevelIndex = currentLevelIndex+1;
        SceneManager.LoadScene(nextLevelIndex);
    }

    void LoadFirstLevel() // Lose
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentLevelIndex);
    }

    void Launch()
    {
        if(Input.GetKey(KeyCode.Space) && energyTotal > 1)
        {
            energyTotal -= energyApply * Time.deltaTime; // Не округляем сразу
            energyText.text = Mathf.FloorToInt(energyTotal).ToString(); // Округляем только для отображения целого значения

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
