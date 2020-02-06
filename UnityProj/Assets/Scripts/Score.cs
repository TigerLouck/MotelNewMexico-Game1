using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public GameObject roadParent; //the road parent
    public GameObject car; //the car
    public float score; //tracks the score
    private bool firstScore; //without this, score increments immediately after starting to move
    // Start is called before the first frame update
    void Start()
    {
        score = 0.0f;
        roadParent = GameObject.Find("RoadParent");
        car = GameObject.Find("Car");
        firstScore = true;
    }

    // Update is called once per frame
    void Update()
    {
        //increase the score when the car passes the trigger collider
        //the -1 is there because the particles should be the last element of the list
        for(int i=0;i<roadParent.transform.childCount-1;i++)
        {
            if(roadParent.transform.GetChild(i).GetComponent<DetectTrigger>().triggered)
            {
                if(firstScore)
                {
                    score = 0.0f;
                    firstScore = false;
                }
                else
                {
                    score += 50.0f; //add the distance between the two nodes
                    roadParent.transform.GetChild(i).GetComponent<DetectTrigger>().triggered = false;
                }
            }
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
