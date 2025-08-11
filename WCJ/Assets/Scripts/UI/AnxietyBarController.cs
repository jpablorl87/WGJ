using UnityEngine;
using UnityEngine.UI;

public class AnxietyBarController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerAnxiety playerAnxiety;
    [SerializeField] private Image anxietyFillImage;
    [SerializeField] private RectTransform anxietyIndicator;
    [SerializeField] private RectTransform barStartPoint;
    [SerializeField] private RectTransform barEndPoint;
    private float maxAnxiety = 100f;
    void Update()
    {
        if (playerAnxiety == null) return;
        float currentAnxiety = playerAnxiety.GetAnxiety();//Access to the parameter
        float fill = Mathf.Clamp01(currentAnxiety / maxAnxiety);
        //Update bar
        if (anxietyFillImage != null) anxietyFillImage.fillAmount = fill;
        //Moving the indicator
        if (anxietyIndicator != null && barStartPoint != null && barEndPoint != null)
        {
            anxietyIndicator.position = Vector3.Lerp(barStartPoint.position, barEndPoint.position, fill);
        }
    }
}
