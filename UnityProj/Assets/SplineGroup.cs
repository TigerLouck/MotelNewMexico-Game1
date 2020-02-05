using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineGroup : MonoBehaviour
{
    #region Spline Arrays
    public GameObject[] splineStraights;
    public GameObject[] splineRights;
    public GameObject[] splineLefts;

    #endregion

    private int straightCounter, leftCounter, rightCounter;

    public PieceType previousPieceType;

    public List<GameObject> currentGroup;

    public int allSplinesLength;

    // Start is called before the first frame update
    void Awake ()
    {
        straightCounter = 3;
        leftCounter = rightCounter = 0;
        currentGroup = new List<GameObject> ();
        SetSplineArrays ();
    }

    private void SetSplineArrays ()
    {
        splineStraights = GameObject.FindGameObjectsWithTag ("SplineStraight");
        splineRights = GameObject.FindGameObjectsWithTag ("SplineRight");
        splineLefts = GameObject.FindGameObjectsWithTag ("SplineLeft");

        for (int i = 0; i < 3; i++)
        {
            currentGroup.Add (splineStraights[i]);
        }
        List<GameObject> temp = new List<GameObject> ();
        foreach (GameObject g in splineStraights)
        {
            temp.Add (g);
        }
        foreach (GameObject g in splineRights)
        {
            temp.Add (g);
        }
        foreach (GameObject g in splineStraights)
        {
            temp.Add (g);
        }
        allSplinesLength = temp.Count;
    }

    private PieceType GetRandomPieceType ()
    {
        Array values = Enum.GetValues (typeof (PieceType));
        int rand = UnityEngine.Random.Range (0, values.Length);
        PieceType piece = (PieceType) values.GetValue (rand);

        if (previousPieceType == piece && piece == PieceType.Left)
        {
            previousPieceType = PieceType.Right;
            return PieceType.Right;
        }
        else if (previousPieceType == piece && piece == PieceType.Right)
        {
            previousPieceType = PieceType.Left;
            return PieceType.Left;
        }
        previousPieceType = piece;
        return piece;
    }

    public GameObject GetRandomSpline ()
    {
        PieceType randPiece = GetRandomPieceType ();

        GameObject returnPiece = null;

        switch (randPiece)
        {
            case PieceType.Straight:
                if (straightCounter > splineStraights.Length)
                    straightCounter = 0;
                returnPiece = splineStraights[straightCounter];
                straightCounter++;
                break;
            case PieceType.Right:
                if (rightCounter > splineRights.Length)
                    rightCounter = 0;
                returnPiece = splineRights[rightCounter];
                rightCounter++;
                break;
            case PieceType.Left:
                if (leftCounter > splineLefts.Length)
                    leftCounter = 0;
                returnPiece = splineLefts[leftCounter];
                leftCounter++;
                break;
            default:
                break;
        }
        return returnPiece;
    }
}