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

    public void Start()
    {
        StartCoroutine(SetParameters());
    }

    public IEnumerator SetParameters()
    {
        if (BossManager.instance.boss == null)
        {
            yield return null;
        }

        bossText.text = BossManager.instance.bossName;
        healthbar.maxValue = BossManager.instance.boss.attributes.maxHealth;
    }

    public void Update()
    {
        switch (true)
        {
            case bool x when GameManager.instance.player != null:
                healthbar.value = BossManager.instance.boss.components.curHealth;
                break;

        }
    }
}