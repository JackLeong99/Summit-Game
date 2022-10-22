using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossHealthBar : MonoBehaviour
{
    [Header("Reference")]
    public Slider healthbar;
    public TextMeshProUGUI bossText;

    public void Awake()
    {
        gameObject.SetActive(false);
    }

    public IEnumerator SetParameters()
    {
        if (BossManager.instance.boss == null)
        {
            yield return null;
        }

        bossText.text = BossManager.instance.boss.attributes.bossName;
        healthbar.maxValue = BossManager.instance.boss.components.apMaxHealth;
    }

    public IEnumerator ClearParameters()
    {
        if (BossManager.instance.boss != null)
        {
            yield return null;
        }

        bossText.text = "";
        healthbar.maxValue = 0;
        healthbar.value = 0;
    }

    public void Update()
    {
        switch (true)
        {
            case bool x when BossManager.instance.boss != null:
                healthbar.value = BossManager.instance.boss.components.curHealth;
                break;
        }
    }
}