using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DamageFlash : MonoBehaviour
{
    [SerializeField] Renderer renderer;
    [SerializeField][ColorUsage(false, true)] Color color;
    [SerializeField] float duration = 0.5f;

    Tweener tween;

    void Awake ()
    {
        renderer.material = new Material(renderer.sharedMaterial);
        renderer.material.EnableKeyword("_EMISSION");
    }

    public void Flash ()
    {
        if (tween == null)
        {
            tween = DOVirtual.Color(color, default, duration, (color) =>
            {
                renderer.sharedMaterial.SetColor("_EmissionColor", color);
            }).SetRecyclable(true).SetAutoKill(false).SetEase(Ease.OutQuad);
        }
        else
        {
            tween.Restart();
        }
    }
}
