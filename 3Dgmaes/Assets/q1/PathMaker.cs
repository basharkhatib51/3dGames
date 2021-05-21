using UnityEngine;

public class PathMaker : MonoBehaviour
{
    public LineRenderer lineRenderer { get { return GetComponent<LineRenderer>(); } }
    public Transform[] waypoints;
    public Transform[] otherWaypoints;

    Vector3[] waypointsPositions;
    Vector3[] currentWaypointPositions;
    Vector3[] otherWaypointPositions;
    // Start is called before the first frame update
    void Awake()
    {
        waypointsPositions = new Vector3[waypoints.Length];
        otherWaypointPositions = new Vector3[otherWaypoints.Length];

        lineRenderer.positionCount = waypoints.Length;

        for (int i = 0; i < otherWaypoints.Length; i++) { 
            waypointsPositions[i] = waypoints[i].position;
            otherWaypointPositions[i] = otherWaypoints[i].position;
        }

        currentWaypointPositions = waypointsPositions;
    }

    public Vector3 getPoint(int index)
    {
        return currentWaypointPositions[index];
    }


    public void ChangeLane() {

            if (currentWaypointPositions == waypointsPositions)
                currentWaypointPositions = otherWaypointPositions;
            else
                currentWaypointPositions = waypointsPositions;

            foreach (var wp in waypoints)
                lineRenderer.SetPositions(currentWaypointPositions);
    }
    public int Length { get { return waypoints.Length; } }

}
