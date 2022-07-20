using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trajectory : MonoBehaviour
{
    public Vector3[] arcArray;

    private LineRenderer lineRenderer;

    public float velocity;
    public float angle;
    public int resolution = 10;

    private float g;

    private float radianAngle;//theta Θ

    [SerializeField] GameObject player;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();

        g = Mathf.Abs(Physics2D.gravity.y);

    }

    private void OnValidate()
    {
        //check linerenderer is not null and game is running
        if ((lineRenderer != null) && (Application.isPlaying))
        {
            RenderArc();
        }
    }

    void Start()
    {
        RenderArc();
    }

    private void Update()
    {

    }

    //Poppulating line renderer with appropriate settings
    private void RenderArc()
    {
        lineRenderer.positionCount = resolution + 1;
        lineRenderer.SetPositions(CalculateArcArray());
    }

    //create array of vector3 positions for the arc
    public Vector3[] CalculateArcArray()
    {
        arcArray = new Vector3[resolution + 1];

        radianAngle = /*Mathf.Rad2Deg **/ Mathf.Clamp(angle, 44.35f, 45.54f);
        float maxDistance = (velocity * velocity * Mathf.Sin(2 * radianAngle)) / g;

        for (int i = 1; i <= resolution; i++)
        {
            float t = (float)i / (float)resolution;
            arcArray[i] = CalculateArcPoint(t, maxDistance);
        }

        return arcArray;
    }

    //calculate hight and distance of each vertex
    private Vector3 CalculateArcPoint(float t, float maxDistance)
    {
        float x = t * maxDistance;
        float y = x * Mathf.Tan(radianAngle) - ((g * x * x) / (2 * velocity * velocity * Mathf.Cos(radianAngle) * Mathf.Cos(radianAngle)));

        return new Vector3(x, y);
    }

}
