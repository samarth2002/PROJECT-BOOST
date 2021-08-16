using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody R;
    AudioSource audioSource;

    [SerializeField] AudioClip ThrustEngine;
    [SerializeField] float ThrustForce=1000f,RRotate=100f;
    [SerializeField] ParticleSystem LeftParticle , RightParticle , MainParticle;
    
    void Start()
    {
        R = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }
    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();

        }
        else
        {
            StopThrusting();
        }
    }
    void ProcessRotation()
    {

        if (Input.GetKey(KeyCode.A))
        {
            StartRightThrusting();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            StartLeftThrusting();
        }
        else
        {
            StopSideThrusting();
        }

    }

    private void StartThrusting()
    {
        R.AddRelativeForce(Vector3.up * ThrustForce * Time.deltaTime);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(ThrustEngine);
        }
        if (!MainParticle.isPlaying)
        {
            MainParticle.Play();
        }
    }
    private void StopThrusting()
    {
        MainParticle.Stop();
        audioSource.Stop();
    }

    

    private void StartRightThrusting()
    {
        if (!RightParticle.isPlaying)
        {
            RightParticle.Play();
        }

        ApplyRotation(-RRotate);
    }
    
    private void StartLeftThrusting()
    {
        if (!LeftParticle.isPlaying)
        {
            LeftParticle.Play();
        }

        ApplyRotation(RRotate);
    }

    

    private void StopSideThrusting()
    {
        LeftParticle.Stop();
        RightParticle.Stop();
    }

    private void ApplyRotation(float Rotationer)
    {
        R.freezeRotation = true;
        transform.Rotate(Vector3.forward * Rotationer * Time.deltaTime);
        R.freezeRotation = false;
    }
}
