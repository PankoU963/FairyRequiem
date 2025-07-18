using UnityEngine;

public class ParallaxBackGround : MonoBehaviour
{
    public Transform cam;
    public float parallaxMultiplier = 0.5f;
    public float viewZone = 10f; // Distancia de activación de cambio de tile
    public float backgroundLength; // Ancho de cada tile (fondo)

    private Transform[] layers;
    private int leftIndex;
    private int rightIndex;

    private float lastCamX;

    void Start()
    {
        if (cam == null)
            cam = Camera.main.transform;

        // Inicializa con los hijos del objeto (tiles)
        layers = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
            layers[i] = transform.GetChild(i);

        leftIndex = 0;
        rightIndex = layers.Length - 1;

        lastCamX = cam.position.x;
    }

    void Update()
    {
        float deltaX = cam.position.x - lastCamX;
        transform.position += Vector3.right * (deltaX * parallaxMultiplier);
        lastCamX = cam.position.x;

        if (cam.position.x < (layers[leftIndex].position.x + viewZone))
            ScrollLeft();

        if (cam.position.x > (layers[rightIndex].position.x - viewZone))
            ScrollRight();
    }

    void ScrollLeft()
    {
        int lastRight = rightIndex;
        layers[rightIndex].position = new Vector3(layers[leftIndex].position.x - backgroundLength,
                                                  layers[leftIndex].position.y,
                                                  layers[leftIndex].position.z);

        leftIndex = rightIndex;
        rightIndex = (rightIndex - 1 + layers.Length) % layers.Length;
    }

    void ScrollRight()
    {
        int lastLeft = leftIndex;
        layers[leftIndex].position = new Vector3(layers[rightIndex].position.x + backgroundLength,
                                                 layers[rightIndex].position.y,
                                                 layers[rightIndex].position.z);

        rightIndex = leftIndex;
        leftIndex = (leftIndex + 1) % layers.Length;
    }
}
