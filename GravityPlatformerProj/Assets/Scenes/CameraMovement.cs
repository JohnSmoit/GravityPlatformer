using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject moveArea;
    public SphereCollider sc;
    public Transform t;
    public GameObject player;
    public Transform playerTransform;
    public GameObject player2;
    public Transform player2Transform;
    public NewBehaviourScript playerScript;
    public float sensitivityCoefficient = 1f;
    float speed = 1f;
    const float speedMax = 7f;
    const float FOV = 60f;
    float fovEase = 4f;

    void Start()
    {
        t = moveArea.GetComponent<Transform>();
        sc = moveArea.GetComponent<SphereCollider>();
        playerTransform = player.GetComponent<Transform>();
        player2Transform = player2.GetComponent<Transform>();
        playerScript = player2.GetComponent<NewBehaviourScript>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distToCenter = transform.position - t.position;


        if (distToCenter.magnitude >= sc.radius && !playerScript.cameraFovSwitch) 
        {
            //Debug.Log(distToCenter.magnitude);
             Vector3 radiusVector = distToCenter.normalized * 2;
            // Debug.Log(radiusVector);
            //radiusVector.x = sc.radius * Math.Cos(t.rotation.x);
            if (speed < speedMax) 
            {
                speed += 1f * Time.deltaTime;
            }
            else if (distToCenter.magnitude * 0.2 <= sc.radius) 
            {
                speed -= 1f * Time.deltaTime;
            }

            Camera.main.fieldOfView = FOV;
            //transform.eulerAngles = transform.eulerAngles - new Vector3(0, 0, transform.eulerAngles.z);
            // if (player2Transform.eulerAngles.y < 180f)
            //{
            //Debug.Log(distToCenter);
            distToCenter -= radiusVector;
            fovEase = 4f;
            // Debug.Log("negative");
            //Debug.Log(player2Transform.eulerAngles.y);
            //Debug.Log(distToCenter);
            //Debug.Log(distToCenter);
           // Debug.Log("dist 2: " + distToCenter);
            //Debug.Log("pos - dist: " + (transform.position.x - distToCenter.x));
            transform.Translate(-distToCenter * Time.deltaTime * speed, Space.World);
        }
        
        if (playerScript.cameraFovSwitch)
        {
            Camera.main.fieldOfView += fovEase;
            //float d = fovEa
            transform.position += new Vector3(UnityEngine.Random.Range(-0.3f, 0.3f), UnityEngine.Random.Range(-0.3f, 0.3f), UnityEngine.Random.Range(-0.3f, 0.3f));
            // transform.eulerAngles = transform.eulerAngles - new Vector3(0, 0, fovEase);
            if (fovEase >= 0.03)
            {
                fovEase *= 0.86f;
            }
        }
        //transform.LookAt(playerTransform);
        
    }

    void FixedUpdate()
    {        
        float x = 5 * Input.GetAxis("Mouse X");
        float y = 5 * Input.GetAxis("Mouse Y");

        //transform.LookAt(ft);//(-y * sensitivityCoefficient, 0, x * sensitivityCoefficient, Space.World);
        transform.Rotate(-y * sensitivityCoefficient,  x * sensitivityCoefficient, 0, Space.Self);
    }



}
