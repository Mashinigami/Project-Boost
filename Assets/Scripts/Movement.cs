using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // variables
    [SerializeField] float thrustRate = 1000f;
    [SerializeField] float rotationSensitivity = 100f;

    // cached reference
    Rigidbody rigidbodyCache;
    // Start is called before the first frame update
    void Start()
    {
        rigidbodyCache = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidbodyCache.AddRelativeForce(Vector3.up * Time.deltaTime * thrustRate);
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(Vector3.back);
        }
    }

    private void ApplyRotation(Vector3 axis)
    {
        rigidbodyCache.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(axis * Time.deltaTime * rotationSensitivity);
        rigidbodyCache.freezeRotation = false; // unfreezing rotation so the physics rotation can take over
    }
}
