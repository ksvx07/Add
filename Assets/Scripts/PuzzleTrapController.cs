using UnityEngine;

public class PuzzleTrapController : MonoBehaviour
{
    public bool trapped;
    public GameObject spikes;
    private float targetPosition = 4.0f;
    private Vector3 currentPosition;

    private float deploySpeed = 1f;
    private float retractSpeed = 0.01f;

    public bool deployed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentPosition = spikes.transform.localPosition;   
    }

    // Update is called once per frame
    void Update()
    {
        if (deployed)
        {
            currentPosition.y += deploySpeed;
            if (currentPosition.y > targetPosition)
            {
                deployed = false;
                currentPosition.y = targetPosition;
            }
        }
        else
        {
            currentPosition.y -= retractSpeed;
            if (currentPosition.y < 0)
            {
                currentPosition.y = 0;
            }
        }

        spikes.transform.localPosition = currentPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && trapped)
        {
            deployed = true;
            Debug.Log(trapped);
        }
    }
}
