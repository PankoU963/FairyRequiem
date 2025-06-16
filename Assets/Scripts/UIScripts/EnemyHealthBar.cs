using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [Header("Camera Reference")]
    public Image fillImage;

    [Header("Look at the Camera")]
    public Transform lookAtCamera;

    public void UpdateHealth(float current, float max)
    {
        float percent = Mathf.Clamp01(current / max);
        fillImage.fillAmount = percent;
    }

    private void LateUpdate()
    {
        if (lookAtCamera != null)
            transform.forward = lookAtCamera.forward; // Look at the camera
            
    }
}
