using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    float speed;
    //float maxSpeed = 20f;
    Rigidbody rb;
    public GameObject chargeParticles, dashParticles;
    public GameObject focus;
    public Transform ft;
    float jumpForce = 8.0f;
    bool jumping = false;
    public Vector3 velocity, startPos, endPos = new Vector3(0, 0, 0);
    public bool dashing, cameraFovSwitch = false;
    public float dashDist = 20f;
    float dashSpeed = 0.5f;
    float dashTimer = 0.5f;
    float dashCooldown = 0;
    float anothaone = 0;
    void Start()
    {
        speed = 20.0f;
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        ft = focus.GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        dashCooldown -= Time.deltaTime;
        //Debug.Log(dashCooldown);
        if (!dashing)
        {
            velocity = new Vector3(0, 0, 0);
            if (Input.GetKey(KeyCode.W))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                    speed *= 2f;
                velocity += Vector3.forward * speed * Time.deltaTime;
                speed = 20f;
            }
            if (Input.GetKey(KeyCode.A))
            {
                velocity += Vector3.left * speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.S))
            {
                velocity += Vector3.back * speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.D))
            {
                velocity += Vector3.right * speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.Q)) // for roll
            {
                transform.Rotate(0, 0, -180f * Time.deltaTime, Space.Self);
            }
            if (Input.GetKey(KeyCode.E))
            {
                transform.Rotate(0, 0, 180f * Time.deltaTime, Space.Self);
            }
            if (Input.GetButtonDown("Jump"))
            {
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), 0.5f))
                {
                    jumping = false;
                }
                if (!jumping)
                {
                    rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                    jumping = true;
                }
            }
        }
        //Dash();
        transform.Translate(velocity, Space.Self);
        
    }

    void FixedUpdate() 
    {
        // float x = 5 * Input.GetAxis("Mouse X");
        //float y = 5 * Input.GetAxis("Mouse Y");
        //float oldZ = transform.eulerAngles.z;
        transform.LookAt(ft, Vector3.up);//(-y * sensitivityCoefficient, 0, x * sensitivityCoefficient, Space.World);

    }

    public void Dash() 
    {
        //float dashSpeed = 0.5f;
        if (!dashing && Input.GetKey(KeyCode.R) && jumping && dashCooldown - Time.deltaTime <= 0) 
        {
            dashDist = 10f;
           // startPos = transform.position;
           // endPos = transform.position + Vector3.forward * dashDist;
            dashing = true;
            cameraFovSwitch = true;
            GameObject blackHole = Instantiate(dashParticles, this.transform.position, this.transform.rotation);
            blackHole.transform.parent = gameObject.transform;
        }
        else if (dashing)
        {
            // if (transform.position.magnitude < endPos.magnitude) 
            // {
            anothaone -= 0.01f;
            //Debug.Log(dashTimer - Time.deltaTime);
            if (dashTimer + anothaone <= 0)
            { 
                cameraFovSwitch = false;
                transform.Translate(Vector3.forward * dashDist);
                dashTimer = 0.5f;
                anothaone = 0;
                Debug.Log("no more FOV shirt");
                // dashSpeed += 0.3f;
                //}
                //else 
                // {
                Instantiate(chargeParticles, this.transform.position, this.transform.rotation);
                dashing = false;
                dashCooldown = 0.75f;
                //  dashSpeed = 0.5f;
                // }
            }
        }
    }
}
