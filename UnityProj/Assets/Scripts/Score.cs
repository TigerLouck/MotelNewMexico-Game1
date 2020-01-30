using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public SplineMesh.Spline spline; //the spline script
    private List<SplineMesh.SplineNode> nodes; //the list of nodes
    private SplineMesh.SplineNode current; //the node the car recently passed
    private SplineMesh.SplineNode previous; //the node the car passed previously
    public GameObject car; //the car
    public float score; //tracks the score
    // Start is called before the first frame update
    void Start()
    {
        score = 0.0f;
        spline = GameObject.Find("Spline").GetComponent<SplineMesh.Spline>(); 
        nodes = spline.nodes;
        car = GameObject.Find("Car");
        //set current to be the node closest to the car
        Vector3 lowestDiff= nodes[0].Position - car.transform.position;
        current = nodes[0];
        for(int i=1;i<nodes.Count;i++)
        {
            if((nodes[i].Position-car.transform.position).magnitude<lowestDiff.magnitude)
            {
                lowestDiff = nodes[i].Position - car.transform.position;
                current = nodes[i];
            }
        }
        previous = null; //no previous node on the start of the game
    }

    // Update is called once per frame
    void Update()
    {
        nodes = spline.nodes; //if the spline script's nodes list changes, the changes will be reflected in this list
        Vector3 lowestDiff = nodes[0].Position - car.transform.position;
        //set new current when the car is close enough to the next node
        for (int i = 0; i < nodes.Count; i++)
        {
            if ((car.transform.position.x-nodes[i].Position.x)<1&&(car.transform.position.x - nodes[i].Position.x) > -1&& 
                (car.transform.position.z - nodes[i].Position.z) < 1 && (car.transform.position.z - nodes[i].Position.x) > -1)
            {
                previous = current; //set the previous node to be the old current before assigning a new current
                current = nodes[i];
                score += (previous.Position - current.Position).magnitude; //add the distance between the two nodes
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
