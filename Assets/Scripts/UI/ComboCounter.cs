using UnityEngine;
using UnityEngine.UI;

public class ComboCounter : MonoBehaviour
{
    [SerializeField] private Text comboText;

    public void SetCombo(int combo)
    {
        if (comboText == null)
        {
            return;
        }

        comboText.text = combo > 1 ? $"Combo x{combo}" : string.Empty;
    }
}
