using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonVisual : MonoBehaviour
{
    [SerializeField] Sprite _sprite0;
    [SerializeField] Sprite _sprite1;
    private int _indexSprite = 0;

    public void NextSprite()
    {
        if (_indexSprite == 0)
        {
            _indexSprite = 1;
            GetComponent<Image>().sprite = _sprite1;
        }
        else
        {
            _indexSprite = 0;
            GetComponent<Image>().sprite = _sprite0;
        }
    }
}
