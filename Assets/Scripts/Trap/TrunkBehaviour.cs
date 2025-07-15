using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class TrunkBehaviour : MonoBehaviour
{
    private Vector3 velocidadRotacion = new Vector3(0f, -150f, 0f);

    public float speed = 0.04f;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - speed);
        transform.Rotate(velocidadRotacion * Time.deltaTime);
        Destroy(gameObject, 5);
    }
}
