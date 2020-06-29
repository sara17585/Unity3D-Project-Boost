using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{

    [SerializeField] float rotateSpeed = 20f;
    [SerializeField] float thrustSpeed = 20f;
    [SerializeField] float levelLoadingTime = 2f;
    [SerializeField] AudioClip thrustSound;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip finishSound;


    [SerializeField] ParticleSystem thrustParticles;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem finishParticles;

   

    //float thrustSpeed = 20f;

    //GameObject thrustParticleInstantiate;
    //GameObject crashParticleInstantiate;
    //GameObject finishParticleInstantiate;


    Rigidbody myRigidbody;
    AudioSource myAudioSource;
    LevelHandle level;


    bool collisionisEnabled = true;
    enum State {Alive, Dying, Transcending }
    State state;
    

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myAudioSource = GetComponent<AudioSource>();
        level = FindObjectOfType<LevelHandle>();
        state = State.Alive;
       


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (state == State.Alive)
        {
            RespondToThrustInput();
            RepondToRotateInput();
        }

        if (Debug.isDebugBuild)
        {
            DebugKey();
        }
    }

    private void DebugKey()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            level.LoadNextLevel();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            //if (collisionisEnabled)
            //{
            //    collisionisEnabled = false;
            //}
            //else
            //{
            //    collisionisEnabled = true;
            //}

            collisionisEnabled = !collisionisEnabled;

        }
        
        
       

    }

    private void RespondToThrustInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();

        }
        else
        {

            myRigidbody.velocity += Vector3.up * Physics.gravity.y * 1.5f * Time.deltaTime;
            myAudioSource.Stop();
            thrustParticles.Stop();
            //if (thrustParticleInstantiate)
            //{
            //    Destroy(thrustParticleInstantiate);
            //}
        }


    }

    private void ApplyThrust()
    {
        float thrustThisFrame = thrustSpeed * Time.deltaTime;
        myRigidbody.AddRelativeForce(Vector3.up*thrustSpeed);
        if (!myAudioSource.isPlaying)
        {
            myAudioSource.PlayOneShot(thrustSound);
            thrustParticles.Play();
           // thrustParticleInstantiate = Instantiate(thrustParticles, thrustPro.transform.position, transform.rotation);
        }
        
       
    }

    private void RepondToRotateInput()
    {
        myRigidbody.freezeRotation = true;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(-Vector3.forward * rotateSpeed * Time.deltaTime);
        }

        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
        }
        myRigidbody.freezeRotation = false;
    }



    private void OnCollisionEnter(Collision collision)
    {
        if(state != State.Alive || !collisionisEnabled) { return; }


        switch(collision.gameObject.tag)
        {
            case "Friendly":
                break;

            case "Finished":
                StartCoroutine(LoadNextLevel());      
                break;

            default:
                StartCoroutine(LoadFirstLevel());
                //    FindObjectOfType<LevelHandle>().ManinMenuScene();
                break;



        }
    }

    IEnumerator LoadNextLevel()
    {
        state = State.Transcending;
        myAudioSource.Stop();
        thrustParticles.Stop();
        //Destroy(thrustParticleInstantiate);
        finishParticles.Play();
        //finishParticleInstantiate = Instantiate(finishParticles, transform.position, transform.rotation);
        //Destroy(finishParticleInstantiate, 2f);
        myAudioSource.PlayOneShot(finishSound, 0.2f);
        yield return new WaitForSeconds(levelLoadingTime);
        level.LoadNextLevel();
    }

    IEnumerator LoadFirstLevel()
    {
        state = State.Dying;
        myAudioSource.Stop();
        thrustParticles.Stop();
        //Destroy(thrustParticleInstantiate);
        crashParticles.Play();
        //crashParticleInstantiate = Instantiate(finishParticles, transform.position, Quaternion.identity);
        //Destroy(crashParticleInstantiate, 2f);
        myAudioSource.PlayOneShot(crashSound, 0.2f);
        yield return new WaitForSeconds(levelLoadingTime);
        level.LoadFirstLevel();

    }







}   
