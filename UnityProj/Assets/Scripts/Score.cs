using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public GameObject spline; //the spline
    public GameObject car; //the car
    public float score; //tracks the score
    // Start is called before the first frame update
    void Start()
    {
        score = 0.0f;
        spline = GameObject.Find("Spline");
        car = GameObject.Find("Car");
    }

    // Update is called once per frame
    void Update()
    {
        //increase the score when the car passes the trigger collider
        if(spline.GetComponent<DetectTrigger>().triggered)
        {
            //score += (new formula of tracking distance) //add the distance between the two nodes
            Debug.Log("Triggered!");
        }
    }
    
    //GUI box that displays the score
    private void OnGUI()
    {
        GUI.color = Color.blue;
        GUI.skin.box.fontSize = 20;
        GUI.Box(new Rect(Screen.width - 150, 0, 150, 50), "Score:" + score);
    }
}
