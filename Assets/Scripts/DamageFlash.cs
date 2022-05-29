using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DamageFlash : MonoBehaviour
{
    [SerializeField] Renderer targetRenderer;
    [SerializeField][ColorUsage(false, true)] Color color;
    [SerializeField] float duration = 0.5f;

    Tweener tween;

    void Awake ()
    {
        targetRenderer.material = new Material(targetRenderer.sharedMaterial);
        targetRenderer.material.EnableKeyword("_EMISSION");
    }

    public void Flash ()
    {
        if (tween == null)
        {
            tween = DOVirtual.Color(color, default, duration, (color) =>
            {
                targetRenderer.sharedMaterial.SetColor("_EmissionColor", color);
            }).SetRecyclable(true).SetAutoKill(false).SetEase(Ease.OutQuad);
        }
        else
        {
            tween.Restart();
        }
    }
}
