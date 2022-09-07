using DG.Tweening;
using UnityEngine;

public class DamageTextPool : Pool<DamageText>
{
    public void Awake()
    {
        BossManager.instance.damageTextPool = this;
    }

    protected override void OnInstantiate (DamageText instance)
    {
        base.OnInstantiate(instance);

        instance.SetPool(this);
    }

    protected override void OnReturn (DamageText instance)
    {
        base.OnReturn(instance);

        DOTween.Kill(instance);
    }

    public DamageText Spawn (Vector3 position, string text, Color color, float size)
    {
        DamageText instance = Get();

        instance.SetPosition(position);
        instance.SetText(text);
        instance.SetColor(color);
        instance.SetSize(size);

        instance.Play();

        return instance;
    }
}
