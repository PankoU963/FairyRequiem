using UnityEngine;

public class RootMovement : MonoBehaviour
{
    float speed;
    float rotSpeed;
    float lifeTime;
    float timer = 0f;
    bool goingDown = false;
    Vector3 originalPosition;

    public void Init(float riseSpeed, float rotationSpeed, float duration)
    {
        speed = riseSpeed;
        rotSpeed = rotationSpeed;
        lifeTime = duration;
        timer = 0f;
        goingDown = false;
        originalPosition = transform.position;
    }

    void Update()
    {
        if (!goingDown)
        {
            // Subir
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
        else
        {
            // Bajar
            transform.position -= Vector3.up * speed * Time.deltaTime;
        }

        // Rotar siempre
        transform.Rotate(Vector3.up * rotSpeed * Time.deltaTime, Space.Self);

        timer += Time.deltaTime;

        if (!goingDown && timer >= lifeTime)
        {
            goingDown = true;
            timer = 0f; // reiniciar para contar bajada
        }
        else if (goingDown && timer >= lifeTime)
        {
            gameObject.SetActive(false); // devolver al pool
        }
    }
}