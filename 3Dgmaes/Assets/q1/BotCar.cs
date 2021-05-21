using UnityEngine;

public class BotCar : MonoBehaviour
{
    public PathMaker path;
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

    float timer;
    float delay= 5f;
    void Update()
    {
        float distance = Vector3.Distance(targetPosition, transform.position);

        if (distance < 0.2f)
            SetNewTargetPosition();
        SetLookRotation();
        GoToTarget();

        
        if ((timer +=Time.deltaTime) > delay )
        {
            ChangeLane();
            timer = 0;
            delay = Random.Range(3f, 6f);
        }
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


    private void ChangeLane()
    {
        path.ChangeLane();
        targetPosition = path.getPoint(index);
    }
}
