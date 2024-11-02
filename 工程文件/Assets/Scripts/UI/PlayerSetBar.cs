using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSetBar : MonoBehaviour
{
    public Image healthImage;
    public Image healthDelayImage;
    public Image PowerImage;
    private void Update()
    {
        //前面都是针对healthImage，现在要让delayimage一起变，来制作一个渐变效果
        if(healthDelayImage.fillAmount>healthImage.fillAmount)
        {
            healthDelayImage.fillAmount-=Time.deltaTime;
        }
        else
        {
            healthDelayImage.fillAmount = healthImage.fillAmount;
        }
    }
    public void OnHealthChange(float persentage)
    {
        healthImage.fillAmount=persentage;

    }
}
