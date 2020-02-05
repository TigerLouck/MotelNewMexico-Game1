using System;
using System.Collections;
using System.Collections.Generic;
using SplineMesh;
using UnityEngine;

public class SplineConnecter : MonoBehaviour
{
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
        //PlaceNewPiece();

    }

    // Update is called once per frame
    void Update ()
    {
        rotTimerDelta += Time.deltaTime;
        if (rotTimerDelta >= pieceTimer)
        {
            PlaceNewPiece ();
            rotTimerDelta = 0f;
        }
    }

    private void FixedUpdate ()
    {
        UpdateRotationDelta ();
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
        Vector3 newDir = (newPos - diff).normalized;

        switch (start.gameObject.tag)
        {
            case "SplineLeft":
                Debug.Log ("previous piece was spline left");
                if (newDir.x % 1f == 0)
                    end.transform.Rotate (new Vector3 (0f, 90f, 0f));
                break;
            case "SplineRight":
                Debug.Log ("previous piece was spline right");
                if (newDir.x % 1f == 0)
                    end.transform.Rotate (new Vector3 (0f, -90f, 0f));
                break;
            case "SplineStraight":
                Debug.Log ("previous piece was spline straight");
                if (newDir.x == -1f)
                    end.transform.Rotate (new Vector3 (0f, -90f, 0f));
                else if (newDir.x == 1f)
                    end.transform.Rotate (new Vector3 (0f, 90f, 0f));
                break;
            default:
                break;

        }

        // Quaternion tempQ = Quaternion.Euler(newDir);
        // Quaternion newRotation = new Quaternion(tempQ.x,tempQ.y,tempQ.z,0f);
        // end.transform.rotation = newRotation;

    }

    int count = 2;
    int lastCount = 0;
    private void PlaceNewPiece ()
    {

        if (count > allSplines.allSplinesLength)
        {
            count = 0;
        }
        if (lastCount > allSplines.allSplinesLength)
        {
            lastCount = 0;
        }

        GameObject newSpline = allSplines.GetRandomSpline ();

        MoveToEndpoint (allSplines.currentGroup[2], newSpline);
        allSplines.currentGroup.Add (newSpline);
        allSplines.currentGroup[lastCount].transform.position = new Vector3(0f,0f,-30000f);
        count++;
        allSplines.currentGroup.Remove(allSplines.currentGroup[0]);
        allSplines.currentGroup.TrimExcess();
    }

    private void UpdateRotationDelta ()
    {
        rotTimerDelta += Time.deltaTime;
        if (rotTimerDelta >= pieceTimer)
        {
            rotTimerDelta = 0;

        }
    }

}

public enum PieceType
{
    Straight,
    Right,
    Left,

}