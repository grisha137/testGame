using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    public void SetHealth(float current, float max)
    {
        if (slider == null)
        {
            return;
        }

        slider.maxValue = max;
        slider.value = current;
    }
}
