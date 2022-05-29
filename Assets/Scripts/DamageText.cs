using TMPro;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(TextMeshPro))]
public class DamageText : MonoBehaviour
{
    [SerializeField] TextMeshPro text;

    Transform cameraTransform;
    DamageTextPool pool;

    void Awake ()
    {
        cameraTransform = Camera.main.transform;
    }

    void Update ()
    {
        FaceCamera();
    }

    public void SetText (string value)
    {
        text.text = value;
    }

    public void SetColor (Color color)
    {
        text.color = color;
    }

    public void SetPosition (Vector3 position)
    {
        transform.position = position;
    }

    public void SetSize (float size)
    {
        text.fontSize = size;
    }

    public void SetPool (DamageTextPool pool)
    {
        this.pool = pool;
    }

    public void Play ()
    {
        FaceCamera();

        Vector3 startPos = transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f);
        Vector3 targetPos = startPos + new Vector3(Random.Range(-2f, 2f), Random.Range(2f, 4f), 0f);

        transform.position = startPos;
        transform.localScale = new Vector3(2, 2, 2);

        const float duration = 2f;
        Sequence sequence = DOTween.Sequence(this);

        sequence.Insert(0f, transform.DOScale(Vector3.one, duration).SetEase(Ease.OutQuad));
        sequence.Insert(0f, transform.DOMove(targetPos, duration).SetEase(Ease.OutQuad));
        sequence.Insert(duration * 0.5f, text.DOFade(0f, duration * 0.5f));

        sequence.OnComplete(() =>
        {
            pool.Return(this);
        });
    }

    private void FaceCamera ()
    {
        transform.forward = transform.position - cameraTransform.position;
    }
}
