using UnityEngine;

public class TheCubeController : MonoBehaviour
{
    public MeshRenderer Renderer;
    private Vector3 startPosition;
    private Material material;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
        material = Renderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(25.0f * Time.deltaTime, 50.0f * Time.deltaTime, 0.0f);
        transform.position = startPosition + Vector3.up * Mathf.Sin(Time.time * 2.0f) * .5f;
        material.color = new Color((Mathf.Sin(Time.time * 1.0f) + 1.0f), (Mathf.Cos(Time.time * 1.0f) + 1.0f), 1);
    }
}
