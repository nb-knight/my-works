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
        //ǰ�涼�����healthImage������Ҫ��delayimageһ��䣬������һ������Ч��
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
