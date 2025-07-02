using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    [Header("Settings")] 
    [SerializeField] private CardRarityEnum _rarity;
    [SerializeField] private CardPawnEnum _pawns;
    [SerializeField] private bool _isMine;

    [SerializeField] private CardScriptable _card;
    
    
    [Header("Background")]
    [SerializeField] private Image _blueBackground;
    [SerializeField] private Image _redBackground;

    [Header("Template")]
    [SerializeField] private Image _standardTemplate;
    [SerializeField] private Image _legendaryTemplate;

    [Header("Pawns")]
    [SerializeField] private Image _replace;
    [SerializeField] private Image _pawnOne;
    [SerializeField] private Image _pawnTwo;
    [SerializeField] private Image _pawnThree;
    
    [Header("Grid")] 
    [SerializeField] private Sprite _gridPawn;
    [SerializeField] private Sprite _gridEffect;
    [SerializeField] private Sprite _gridPawnEffect;
    [SerializeField] private List<Image> _gridImagesRow1;
    [SerializeField] private List<Image> _gridImagesRow2;
    [SerializeField] private List<Image> _gridImagesRow3;
    [SerializeField] private List<Image> _gridImagesRow4;
    [SerializeField] private List<Image> _gridImagesRow5;
    
    
    [Header("Other")]
    [SerializeField] private Image _character;
    [SerializeField] private TMP_Text _power;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _description;

    
    
    private void SetTemplate()
    {
        switch (_rarity)
        {
            case CardRarityEnum.Standard:
                _standardTemplate.gameObject.SetActive(true);
                _legendaryTemplate.gameObject.SetActive(false);
                break;
            case CardRarityEnum.Legendary:
                _legendaryTemplate.gameObject.SetActive(true);
                _standardTemplate.gameObject.SetActive(false);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    private void SetPawns()
    {
        switch (_pawns)
        {
            case CardPawnEnum.One:
                _pawnOne.gameObject.SetActive(true);
                _pawnTwo.gameObject.SetActive(false);
                _pawnThree.gameObject.SetActive(false);
                _replace.gameObject.SetActive(false);
                break;
            case CardPawnEnum.Two:
                _pawnOne.gameObject.SetActive(true);
                _pawnTwo.gameObject.SetActive(true);
                _pawnThree.gameObject.SetActive(false);
                _replace.gameObject.SetActive(false);
                break;
            case CardPawnEnum.Three:
                _pawnOne.gameObject.SetActive(true);
                _pawnTwo.gameObject.SetActive(true);
                _pawnThree.gameObject.SetActive(true);
                _replace.gameObject.SetActive(false);
                break;
            case CardPawnEnum.Replace:
                _pawnOne.gameObject.SetActive(false);
                _pawnTwo.gameObject.SetActive(false);
                _pawnThree.gameObject.SetActive(false);
                _replace.gameObject.SetActive(true);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void SetBackground()
    {
        var scale = _character.rectTransform.localScale;
        scale.x = _isMine ? 1f : -1f;
        _character.rectTransform.localScale = scale;

        _blueBackground.gameObject.SetActive(_isMine);
        _redBackground.gameObject.SetActive(!_isMine);
    }

    private void OnValidate()
    {
        SetTemplate();
        SetPawns();
        SetBackground();
    }
}
