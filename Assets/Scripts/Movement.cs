using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // PARAMETERS
    [SerializeField] float thrustRate = 1000f;
    [SerializeField] float rotationSensitivity = 100f;
    [SerializeField] AudioClip mainEngineSFX;
    [SerializeField] ParticleSystem leftThrusterVFX;
    [SerializeField] ParticleSystem rightThrusterVFX;
    [SerializeField] ParticleSystem mainEngineVFX;

    // CACHE
    Rigidbody rigidbodyCache;
    AudioSource audioSourceCache;

    void Start()
    {
        rigidbodyCache = GetComponent<Rigidbody>();
        audioSourceCache = GetComponent<AudioSource>();
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

    void StartThrusting()
    {
        rigidbodyCache.AddRelativeForce(Vector3.up * Time.deltaTime * thrustRate);
        if (!audioSourceCache.isPlaying)
        {
            audioSourceCache.PlayOneShot(mainEngineSFX);
        }

        if (!mainEngineVFX.isPlaying)
        {
            mainEngineVFX.Play();
        }
    }

    void StopThrusting()
    {
        audioSourceCache.Stop();
        mainEngineVFX.Stop();
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }

    void RotateLeft()
    {
        ApplyRotation(Vector3.forward);

        if (!leftThrusterVFX.isPlaying)
        {
            leftThrusterVFX.Play();
        }
    }

    private void RotateRight()
    {
        ApplyRotation(Vector3.back);
        if (!rightThrusterVFX.isPlaying)
        {
            rightThrusterVFX.Play();
        }
    }

    void StopRotating()
    {
        leftThrusterVFX.Stop();
        rightThrusterVFX.Stop();
    }

    private void ApplyRotation(Vector3 axis)
    {
        rigidbodyCache.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(axis * Time.deltaTime * rotationSensitivity);
        rigidbodyCache.freezeRotation = false; // unfreezing rotation so the physics rotation can take over
    }
}
