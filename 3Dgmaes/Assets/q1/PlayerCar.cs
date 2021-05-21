using UnityEngine;


public class PlayerCar : MonoBehaviour
{
    public  PathMaker path;
    Vector3 targetPosition;
    int index;
    public float moveSpeed = 5f;
    public bool looping;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = path.getPoint(index);
    }

    // Update is called once per frame
    void Update()
    {
       ChangeLane();
        float distance = Vector3.Distance(targetPosition, transform.position);

        if (distance < 0.2f)
            SetNewTargetPosition();
        SetLookRotation();
        GoToTarget();
    }

    private void SetLookRotation()
    {
        Vector3 dir = targetPosition - transform.position;
        dir.Normalize();
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
    }

    private void GoToTarget()
    {
        if (Vector3.Distance(targetPosition, transform.position) < 0.1f) return;

        Vector3 dir = (targetPosition - transform.position).normalized; //dir.Normalize();
        transform.position += dir * moveSpeed * Time.deltaTime;
    }

    private void SetNewTargetPosition()
    {
        if (looping)
        {
            if (index == path.Length - 1)
                index = -1;
        }
        if (index < path.Length - 1)
            index++;

        targetPosition = path.getPoint(index);
    }

    private void ChangeLane() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            path.ChangeLane();
            targetPosition = path.getPoint(index);
        }
            
    }
}
