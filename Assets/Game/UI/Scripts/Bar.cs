using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    public Image hpBar;
    public Character character;

    private void Start()
    {
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        float healthProgress = character.hitpoint /character.maxHitpoint;
        hpBar.fillAmount = healthProgress;
    }
    private void LateUpdate()
    {
        UpdateHealthBar();
    }

}
