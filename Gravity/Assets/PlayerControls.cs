using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    CharacterController controller;
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    private Vector3 moveDirection = Vector3.zero;

    public bool canRotate = false;
    public bool flip = false;
    public CamController camController;
    public Camera cam;
    private Vector3 groundNormal = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        if (Input.GetKeyDown("g"))
        {
            flip = !flip;
            StartCoroutine(Rotate(5));
        }
        //transform.Rotate(-Vector3.forward, 10);
    }

    IEnumerator rotate()
    {
        while (canRotate)
        {
            if (transform.rotation.z > -90)
            {
                transform.Rotate(-Vector3.forward, 1);
            }
            else
            {
                canRotate = false;
            }
            yield return null;
        }
        
    }


    IEnumerator Rotate(float duration)
    {
        Quaternion startRot = transform.rotation;
        float t = 0.0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            transform.rotation = startRot * Quaternion.AngleAxis(t / duration * 180f, -Vector3.forward);
            yield return null;
        }
        if (flip)
        {
            Quaternion rot = transform.rotation;
            rot.z = -180;
            transform.rotation = rot;
        }
        else
        {
            Quaternion rot = transform.rotation;
            rot.z = 0;
            transform.rotation = rot;
        }
        
        //transform.rotation = startRot;
    }


    public void Movement()
    {
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed;

        if (Input.GetButton("Jump")) {
            moveDirection.y = jumpSpeed;

        }
        moveDirection.y -= gravity* Time.deltaTime;
        controller.Move(moveDirection* Time.deltaTime);
    }
}
