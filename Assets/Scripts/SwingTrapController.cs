using UnityEngine;

public class SwingTrapController : MonoBehaviour
{
    public Vector3 rotation;
    public float angleOffset;
    public float swingRange = 45.0f;
    public float swingSpeed = 1.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rotation.x = Mathf.Sin((Time.time + angleOffset) * swingSpeed) * swingRange;
        transform.rotation = Quaternion.Euler(rotation);
    }
}
