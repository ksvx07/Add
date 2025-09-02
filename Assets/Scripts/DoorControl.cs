using Unity.VisualScripting;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
    private bool isDoorMoving;
    private float openSpeed = 2f;
    public GameObject door;
    public ParticleSystem doorParticleSystem;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDoorMoving) return;
        if(door.transform.localPosition.y > -10)
        {
            door.transform.Translate(Vector3.down * openSpeed * Time.deltaTime);
        }
        else
        {
            doorParticleSystem.Stop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && !isDoorMoving)
        {
            isDoorMoving = true;
            doorParticleSystem.Play();
        }
    }
}
