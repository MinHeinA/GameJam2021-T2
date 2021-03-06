using UnityEngine;
using UnityEngine.UI;

public class HealthbarBehaviour : MonoBehaviour
{
    public Slider slider;
    public Color low;
    public Color high;
    public Vector3 offset;
    public bool forPlayer = false;

    public void SetHealth(int health, int maxHealth)
    {
        slider.gameObject.SetActive(health != 0);
        slider.value = health * 1.0f;
        slider.maxValue = maxHealth * 1.0f;

        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, high, slider.normalizedValue);
    }

    // Update is called once per frame
    void Update()
    {
        if (!forPlayer)
        {
            slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
        }
        
    }
}
