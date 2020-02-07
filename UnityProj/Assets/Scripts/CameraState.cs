using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraState : MonoBehaviour
{
    // Interpolates rotation between the rotations
    // of from and to.
    // (Choose from and to not to be the same as
    // the object you attach this script to)
    public GameObject startCanvas;
    new public GameObject camera;
    public Transform from;
    public Transform to;
    private float speed = 0.1f;
    private float time = 0.5f;
    private bool timeToGo;

    // Next stuff to manage
    public GameObject nextCamera;
    public GameObject nextCanvas;
    public GameObject gameMaster;
    public GameObject carForStarting;
    public SplineConnecter roadMaster;


    // Start is called before the first frame update
    void Start()
    {
        timeToGo = false;
        roadMaster.enabled = false;
        startCanvas.SetActive(true);
        camera.SetActive(true);
        nextCamera.SetActive(false);
        nextCanvas.SetActive(false);
        gameMaster.SetActive(false);
        gameMaster.GetComponent<Score>().enabled = false;
        roadMaster.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Go when clicked
        if (timeToGo)
        {
            // Set our position as a fraction of the distance between the markers.
            camera.transform.position = Vector3.Lerp(from.position, to.position, 0.1f);
            camera.transform.rotation = Quaternion.Lerp(from.rotation, to.rotation, Time.time * speed);
            time -= Time.deltaTime;
        }

        // Transition time is over now, now get ready the game
        if (time <= 0)
        {
            startCanvas.SetActive(false);
            camera.SetActive(false);
            nextCamera.SetActive(true);
            nextCanvas.SetActive(true);
            gameMaster.SetActive(true);
            gameMaster.GetComponent<Score>().enabled = true;
            roadMaster.enabled = true;
        }
    }

    // Clicking button changes value of timeToGo
    // Gets rid of the current canvas as well
    public void GoTime()
    {
        timeToGo = true;
        startCanvas.GetComponent<Canvas>().enabled = false;
    }
}
