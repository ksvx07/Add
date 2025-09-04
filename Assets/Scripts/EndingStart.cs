using UnityEngine;

public class EndingStart : MonoBehaviour
{
    private void Start()
    {
        TeleportToEnding();
    }

    private void TeleportToEnding()
    {
        GameObject.Find("Player").transform.position = GameObject.Find("Player").transform.position + transform.position - GameObject.Find("EndingPoint").transform.position;
        //GameObject.Find("Player").transform.position = GameObject.Find("Player").transform.position + transform.position - new Vector3(0f, 3.8f, -42.84f) + new Vector3(0f, 0f, -48.32f);
        //GameObject.Find("Player").transform.position += transform.position - new Vector3(0f, 3.8f, -42.84f);

    }
}

