using UnityEngine;
using UnityEngine.UI;
public class EnemyHealthDisplay : MonoBehaviour
{
    [SerializeField]
    private Slider healthSlider;


    public void updateSlider(float health)
    {
                healthSlider.value = health;
    }

    public void setMaxFill(float maxHealth)
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
    }
}
