using System.Collections;
using UnityEngine;

public class BotManager : MonoBehaviour
{
    public int id;
    public bool isRegistered;
    Transform player;
    float moveSpeed = 10;
    public static Vector3 seenPosition;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player").transform;
        Register();
    }

    public void Register() {
        if (isRegistered)
        {
            EventManager.playerSeenHandler += OnPlayerSeen;
            gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
            EventManager.instance.registeredObjects.Add(this);
            Debug.Log("registered  bot: " + id);
        }
        else
            gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
    }

    private void OnPlayerSeen(int id)
    {
        if (this.id != id) return;

        Debug.Log(gameObject.name +" : Player seen, I need help");
        foreach (var item in EventManager.instance.registeredObjects)
        {
            if (item==this) continue;
            item.Notify();
        }
    }

    private void Notify()
    {
        Debug.Log("hold on, I am coming" + id);
        StartCoroutine(GoToTarget());
    }

    public IEnumerator GoToTarget() {
       
        while (Vector3.Distance(seenPosition, transform.position) > 0.5f)
        {
            Vector3 dir = seenPosition - transform.position;
            dir.Normalize();
            transform.position += dir * moveSpeed*Time.deltaTime;
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals("player")) {
            seenPosition = other.transform.position;
            EventManager.instance.OnPLayerSeenHandler(id);
        } 
    }
}
