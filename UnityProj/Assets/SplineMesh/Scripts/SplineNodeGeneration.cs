using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SplineMesh;

public class SplineNodeGeneration : MonoBehaviour
{

    private Spline currentSpine;

    private List<SplineNode> splineNodes;

    [SerializeField]
    private float nodeSpawnInterval;
    private float timeUntilSpawn;


    // Start is called before the first frame update
    void Start()
    {
        currentSpine = gameObject.GetComponentInParent<Spline>();
        splineNodes = currentSpine.nodes;
        timeUntilSpawn = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        timeUntilSpawn+= Time.deltaTime;
        if(timeUntilSpawn>nodeSpawnInterval)
        {
            NewRuntimeNode();
            timeUntilSpawn = 0;
        }
    }
    
    private void NewRuntimeNode()
    {
        SplineNode lastNode = splineNodes[splineNodes.Count-1];
        SplineNode newNode = new SplineNode(lastNode.Direction, lastNode.Direction + lastNode.Direction - lastNode.Position);
        var index = currentSpine.nodes.IndexOf(lastNode);
        if(index == currentSpine.nodes.Count - 1) {
            currentSpine.AddNode(newNode);
        } else {
            currentSpine.InsertNode(index + 1, newNode);
        }
        //SplineNode newNode = new SplineNode
    }

}
