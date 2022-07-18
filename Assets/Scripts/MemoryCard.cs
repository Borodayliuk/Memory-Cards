using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MemoryCard : MonoBehaviour
{
    [SerializeField] private GameObject _cardBack;
    public SceneController controller;
    private int _id;
    public int id
    {
        get { return _id; }
    }
    public void SetCard(int id, Sprite image)
    {
        _id = id;
        GetComponent<SpriteRenderer>().sprite = image;
    }
    public void OnMouseDown()
    {
        if (!controller.IsPaused())
        {
            if (_cardBack.activeSelf && controller._secondRevealed == null)
            {
                _cardBack.SetActive(false);
                controller.CardRevealed(this);
            }
        }
    }
    public void Unreveal()
    {
        if (!controller.IsPaused())
        {
            if (!_cardBack.activeSelf)
            {
                _cardBack.SetActive(true);
            }
        }
    }
    public void DestroyCard()
    {
        StartCoroutine(Destroy());
    }
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(0);
        transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBounce).OnComplete(() => Destroy(gameObject));

    }
}
