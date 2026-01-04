using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteHealthBar : MonoBehaviour
{
    public Health playerHealth;   
    public Image healthImage;        
    public Sprite fullHP;
    public Sprite seventyFiveHP;
    public Sprite fiftyHP;
    public Sprite twentyFiveHP;
    public Sprite zeroHP;


     void Update()
    {
        UpdateHealthSprite();
    }

    void UpdateHealthSprite()
    {
        // Have 5 sprite images and when it goes 25 percent lower the sprite changes accordingly
        float hpPercent = (float) playerHealth.GetCurrentHP() / (float) playerHealth.maxHP; // Calculating the percentage
        if (hpPercent >= 0.75f)
            healthImage.sprite = fullHP;
        else if (hpPercent >= 0.5f)
            healthImage.sprite = seventyFiveHP;
        else if (hpPercent >= 0.25f)
            healthImage.sprite = fiftyHP;
        else if (hpPercent > 0)
            healthImage.sprite = twentyFiveHP;
        else
            healthImage.sprite = zeroHP;
    }
}
