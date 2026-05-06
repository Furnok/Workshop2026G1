using DG.Tweening;
using UnityEngine;

public class ObjectPull : MonoBehaviour
{
    public float distance = 5f;
    public float duration = 2f;

    private Vector3 startPosition;
    private Vector3 openPosition;

    private bool isOpen = false;
    private bool isTransit = false;

    void Start()
    {
        startPosition = transform.position;
        openPosition = startPosition + transform.forward * distance;
    }

    public void MaybeOpen()
    {
        if (!isTransit)
        {
            if (isOpen) Close();
            else Open();
        }
    }

    private void Open()
    {
        isTransit = true;

        transform.DOMove(openPosition, duration)
        .SetEase(Ease.InOutSine)
        .OnComplete(() =>
        {
            isOpen = true;
            isTransit = false;
        });
    }

    private void Close()
    {
        isTransit = true;

        transform.DOMove(startPosition, duration)
        .SetEase(Ease.InOutSine)
        .OnComplete(() => 
        {
            isOpen = false;
            isTransit = false;
        });
    }
}
