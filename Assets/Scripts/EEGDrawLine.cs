using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta;
using System.Linq;

public class EEGDrawLine : MonoBehaviour {

    //Plot the EEG readings from a given channel in a circular graph,
    //using cylinder meshes to connect points in the graph

    //Arrays of points and edges 
    private Vector3[] points;
    private GameObject[] cylinders;

    public EEGData.EEG_CHANNELS channelToRecordFrom = EEGData.EEG_CHANNELS.TP9;

    //Offset of current object from (0,0,0)
    private Vector3 offset;

    public const int maxLineSegments = 50;
    public const float circleRadius = 0.2f;

    //Min and max height of the points in the graph 
    public const float maxHeight = 0.1f; 
    public const float minHeight = -0.1f;

    //Min and max EEG value, all readings are clamped between these 
    //TODO: consider changing this to current max / current min 
    public const float maxEEG = 2500f;
    public const float minEEG = -2500f;

    public const float lineThickness = 0.002f;

    public Material material;

    public Mesh cylinderMesh;

    //Number of segments drawn so far;
    private int currentPosition;
    

	// Use this for initialization
	void Start () {

        points = new Vector3[maxLineSegments];
        cylinders = new GameObject[maxLineSegments - 1];
        offset = gameObject.transform.position;
        currentPosition = 0;
        points[0] = offset;

        GrabInteraction grabInteraction = GetComponent<GrabInteraction>();
        

	}
	
	// Update is called once per frame
	void Update () {

        offset = gameObject.transform.position;
        float eegReading = EEGData.eegData[(int)channelToRecordFrom];

        //Normalize the eeg data to lie between 0 and 1 
        float normalizedReading = (Mathf.Clamp(eegReading, minEEG, maxEEG) - minEEG) / (maxEEG - minEEG);
        Debug.Log(eegReading.ToString() + " " + normalizedReading.ToString());

        float currentY = normalizedReading * (maxHeight - minHeight) + minHeight + offset.y;

        //The graph will move around in a circle of circleRadius
        float currentX = Mathf.Sin(((float) currentPosition/ (float) maxLineSegments)*2*Mathf.PI) * circleRadius + offset.x;
        float currentZ = Mathf.Cos(((float)currentPosition / (float)maxLineSegments) * 2*Mathf.PI) * circleRadius + offset.z;

        Vector3 newPoint = new Vector3(currentX, currentY, currentZ);
        Vector3 oldPoint = points[(currentPosition) % maxLineSegments];

        points[(currentPosition + 1) % maxLineSegments] = newPoint;

        GameObject connectingCylinder;

        if (cylinders[currentPosition % (maxLineSegments - 1)] == null)
        {
            // Create a new line segment 
            connectingCylinder = new GameObject();

            MeshFilter ringMesh = connectingCylinder.AddComponent<MeshFilter>();
            ringMesh.mesh = cylinderMesh;

            MeshRenderer ringRenderer = connectingCylinder.AddComponent<MeshRenderer>();
            ringRenderer.material.EnableKeyword("_EMISSION");
            ringRenderer.material = material;

            MeshCollider meshCollider = connectingCylinder.AddComponent<MeshCollider>();
            meshCollider.sharedMesh = cylinderMesh;
            meshCollider.convex = true;


        }

        else
        {
            connectingCylinder = cylinders[currentPosition % (maxLineSegments - 1)];
        }

        float lineDistance = Vector3.Distance(newPoint, oldPoint);

        //Rescale the cylinder to match distance
        connectingCylinder.transform.localScale = new Vector3(lineThickness, lineDistance, lineThickness);

        //Rotate the cylinder to go from old point to new point 
        connectingCylinder.transform.position = oldPoint;
       
        connectingCylinder.transform.LookAt(newPoint, Vector3.up);
        connectingCylinder.transform.rotation *= Quaternion.Euler(90, 0, 0);
        connectingCylinder.transform.position = oldPoint;


        cylinders[currentPosition % (maxLineSegments - 1)] = connectingCylinder;

        currentPosition++;
	}
}
