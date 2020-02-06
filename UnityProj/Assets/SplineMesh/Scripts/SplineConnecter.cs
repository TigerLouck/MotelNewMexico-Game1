using System;
using System.Collections;
using System.Collections.Generic;
using SplineMesh;
using UnityEngine;

public class SplineConnecter : MonoBehaviour
{

    public GameObject chaser;

    #region Timers
    [SerializeField]
    private float pieceTimer = 5f;

    private float rotTimerDelta;
    #endregion

    #region Prefabs
    private GameObject straightPrefab;
    private GameObject leftPrefab;
    private GameObject rightPrefab;

    #endregion

    #region Debug
    private bool isFirstFrame = false;
    public bool isDebugLogOn = true;
    #endregion

    #region Splines
    private SplineGroup allSplines;

    private GameObject currentSpline;

    private PieceType lastPiecePlaced;

    #endregion

    // Start is called before the first frame update
    void Awake ()
    {
        rotTimerDelta = 0;
        currentSpline = new GameObject ();
        lastPiecePlaced = PieceType.Straight;
    }

    private void Start ()
    {
        allSplines = gameObject.GetComponent<SplineGroup> ();
        GetStartingSplines ();

    }

    // Update is called once per frame
    void Update ()
    {
        CurveSample FXSample = allSplines.currentGroup[0].GetComponent<Spline>().GetSample(rotTimerDelta / pieceTimer);
        chaser.transform.position = FXSample.location + allSplines.currentGroup[0].transform.position;
        chaser.transform.rotation = FXSample.Rotation * allSplines.currentGroup[0].transform.rotation;
    }

    private void FixedUpdate ()
    {
        UpdateRotationDelta();
    }

    // connects 3 straight pieces together to start out
    private void GetStartingSplines ()
    {
        if (isFirstFrame)
            return;
        else
            isFirstFrame = true;

        for (int i = 1; i < 3; i++)
        {
            // get all nodes for the starting and ending pieces
            List<SplineNode> fNodes = allSplines.splineStraights[i - 1].GetComponent<Spline> ().nodes;
            //List<SplineNode> sNodes = splineStraights[i].GetComponent<Spline>().nodes;

            // get pos and dir for both nodes of Starting piece
            Vector3 fStartPos = fNodes[0].Position;
            Vector3 fStartDir = fNodes[0].Direction;

            Vector3 fEndPos = fNodes[1].Position;
            Vector3 fEndDir = fNodes[1].Direction;

            Transform currentTransform = allSplines.splineStraights[i - 1].transform;
            Vector3 newPos = currentTransform.TransformPoint (fEndPos);
            allSplines.splineStraights[i].transform.position = newPos;

            //Vector3 direction = currentTransform.TransformDirection (fEndDir);
        }
    }

    private void MoveToEndpoint (GameObject start, GameObject end)
    {
        List<SplineNode> startNodes = start.GetComponent<Spline> ().nodes;
        Vector3 fEndPos = Vector3.zero;
        Vector3 fEndDir = Vector3.zero;

        if (start.gameObject.tag == "SplineLeft" || start.gameObject.tag == "SplineRight")
        {
            fEndPos = startNodes[2].Position;
            fEndDir = startNodes[2].Direction;
        }
        else
        {
            fEndPos = startNodes[1].Position;
            fEndDir = startNodes[1].Direction;
        }

        Transform currentTransform = start.transform;
        Vector3 newPos = currentTransform.TransformPoint (fEndPos);
        end.transform.position = newPos;

        Vector3 diff = currentTransform.TransformPoint (fEndDir);
        Vector3 newDir = (diff - newPos).normalized;
        

        // Orient Pieces accornding to the forward vector of the last piece in the group
        if(newDir.x==1f)
        {
            end.transform.Rotate(new Vector3(0f,-90f,0f));
        }
        else if(newDir.x==-1f)
        {
            end.transform.Rotate(new Vector3(0f,90f,0f));
        }
        else if(newDir.z==-1f)
        {
            end.transform.Rotate(new Vector3(0f,-180f,0f));
        }

    }

    
    private void PlaceNewPiece ()
    {
        GameObject newSpline = allSplines.GetRandomSpline ();
        MoveToEndpoint (allSplines.currentGroup[2], newSpline);
        allSplines.currentGroup.Add (newSpline);
        allSplines.currentGroup[0].transform.position = new Vector3(0f,10000f,-30000f);
        allSplines.currentGroup.Remove(allSplines.currentGroup[0]);
        allSplines.currentGroup.TrimExcess();
    }

    private void UpdateRotationDelta ()
    {
        rotTimerDelta += Time.deltaTime;
        if (rotTimerDelta >= pieceTimer)
        {
            if(isDebugLogOn)
                Debug.Log("New Piece should spawn NOW");
            PlaceNewPiece ();
            rotTimerDelta = 0f;
        }
    }

}

public enum PieceType
{
    Straight,
    Right,
    Left,

}