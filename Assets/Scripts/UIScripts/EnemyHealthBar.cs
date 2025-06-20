using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [Header("Camera Reference")]
    [SerializeField] private Image fillHealthBarImage;

    [Header("Look at the Camera")]
    [SerializeField] private Transform HealthBarlookAtCamera;

    public void UpdateHealth(float current, float max)
    {
        float percent = Mathf.Clamp01(current / max);
        fillHealthBarImage.fillAmount = percent;
    }

    private void LateUpdate()
    {
        if (HealthBarlookAtCamera != null)
            transform.forward = HealthBarlookAtCamera.forward; // 
            
    }
}
