using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [Header("Health Bar Settings")]
    [SerializeField] private Image fillHealthBarImage;
    [SerializeField] private Transform cameraTransform;

    public void UpdateHealth(float current, float max)
    {
        float percent = Mathf.Clamp01(current / max);
        fillHealthBarImage.fillAmount = percent;
    }

    private void LateUpdate()
    {
        if (cameraTransform != null)
            transform.forward = cameraTransform.forward; // 
            
    }
}
