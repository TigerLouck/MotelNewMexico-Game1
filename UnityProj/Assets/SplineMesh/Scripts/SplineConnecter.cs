using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SplineMesh;

public class SplineConnecter : MonoBehaviour
{
    [SerializeField]
    private float rotationCounter = 5f;

    private float rotTimerDelta;
    private int straightCounter;
    private GameObject[] splineStraights;
    private GameObject[] splineLeft;
    private GameObject[] splineRight;
    private GameObject[] splineSideways;

    private GameObject currentSpline;

    private PieceType lastPiecePlaced;
    private bool isFirstFrame = false;
    // Start is called before the first frame update
    void Start()
    {
        rotTimerDelta = 0;
        straightCounter = 0;
        currentSpline = new GameObject();
        lastPiecePlaced = PieceType.None;
        GetAllSplines();
        StartConnection();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() 
    {
        
        //UpdateRotationDelta();
        //ResetStraightCounter();
    }

    private void GetAllSplines()
    {
        splineStraights = GameObject.FindGameObjectsWithTag("SplineStraight");
        splineSideways = GameObject.FindGameObjectsWithTag("SplineSideways");
        splineLeft = GameObject.FindGameObjectsWithTag("SplineLeft");
        splineRight = GameObject.FindGameObjectsWithTag("SplineRight");
    }

    // connects 3 straight pieces together to start out
    private void StartConnection()
    {
        if(isFirstFrame)
            return;
        else
            isFirstFrame = true;
            
        for(int i=1;i<=splineStraights.Length-1;i++)
        {
            // get all nodes for the starting and ending pieces
            List<SplineNode> fNodes = splineStraights[i-1].GetComponent<Spline>().nodes;
            List<SplineNode> sNodes = splineStraights[i].GetComponent<Spline>().nodes;

            // get pos and dir for both nodes of Starting piece
            Vector3 fStartPos = fNodes[0].Position;
            Vector3 fStartDir = fNodes[0].Direction;

            Vector3 fEndPos = fNodes[1].Position;
            Vector3 fEndDir = fNodes[1].Direction;

            // get pos and dir for both nodes of Ending piece
            // Vector3 sStartPos = sNodes[0].Position;
            // Vector3 sStartDir = sNodes[0].Direction;

            // Vector3 sEndPos = sNodes[1].Position;
            // Vector3 sEndDir = sNodes[1].Direction;

            splineStraights[i].GetComponent<Spline>().nodes[0].Position = new Vector3 (fEndPos.x,fEndPos.y,fEndPos.z);
            splineStraights[i].GetComponent<Spline>().nodes[0].Direction = new Vector3 (fEndDir.x,fEndDir.y,fEndDir.z);
            splineStraights[i].GetComponent<Spline>().nodes[1].Position = new Vector3 (fEndPos.x,fEndPos.y,fEndPos.z+100);
            splineStraights[i].GetComponent<Spline>().nodes[1].Direction = new Vector3 (fEndDir.x,fEndDir.y,fEndDir.z+100);
        }
    }

    private void UpdateConnection()
    {

    }

    private void UpdateRotationDelta()
    {
        rotTimerDelta += Time.deltaTime;
            if(rotTimerDelta>=rotationCounter)
            {
                rotTimerDelta = 0;
                straightCounter++;
        }
    }

    private void ResetStraightCounter()
    {
        if(straightCounter > splineStraights.Length)
        {
            straightCounter =0;
        }
    }

    private enum PieceType 
    {
        Straight,
        Horizontal,
        Right,
        Left,
        None
    }
}
