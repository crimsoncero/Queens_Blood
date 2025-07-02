using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    [Header("Settings")] 
    [SerializeField] private bool _isMine;
    [SerializeField] private CardData _card;
    
    
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
    [FormerlySerializedAs("_gridImagesRow1")] [SerializeField] private List<Image> _gridImagesRow0;
    [FormerlySerializedAs("_gridImagesRow2")] [SerializeField] private List<Image> _gridImagesRow1;
    [FormerlySerializedAs("_gridImagesRow3")] [SerializeField] private List<Image> _gridImagesRow2;
    [FormerlySerializedAs("_gridImagesRow4")] [SerializeField] private List<Image> _gridImagesRow3;
    [FormerlySerializedAs("_gridImagesRow5")] [SerializeField] private List<Image> _gridImagesRow4;
    
    
    [Header("Other")]
    [SerializeField] private Image _character;
    [SerializeField] private TMP_Text _power;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _description;


    public void SetCardData(CardData cardData)
    {
        _card = cardData;
        _name.text = cardData.Name;
        _power.text = cardData.Power.ToString();
        SetTemplate();
        SetBackground();
        SetCost();
        SetGrid();
        
    }
    private void SetTemplate()
    {
        switch (_card.Rarity)
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
    private void SetCost()
    {
        switch (_card.Cost)
        {
            case CardCostEnum.One:
                _pawnOne.gameObject.SetActive(true);
                _pawnTwo.gameObject.SetActive(false);
                _pawnThree.gameObject.SetActive(false);
                _replace.gameObject.SetActive(false);
                break;
            case CardCostEnum.Two:
                _pawnOne.gameObject.SetActive(true);
                _pawnTwo.gameObject.SetActive(true);
                _pawnThree.gameObject.SetActive(false);
                _replace.gameObject.SetActive(false);
                break;
            case CardCostEnum.Three:
                _pawnOne.gameObject.SetActive(true);
                _pawnTwo.gameObject.SetActive(true);
                _pawnThree.gameObject.SetActive(true);
                _replace.gameObject.SetActive(false);
                break;
            case CardCostEnum.Replace:
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

    private void SetGrid()
    {
        for (var i = 0; i < CardGrid.HEIGHT; i++)
        {
            for (var j = 0; j < CardGrid.WIDTH; j++)
            {
                var o = _isMine ? CardGrid.Orientation.Normal : CardGrid.Orientation.Rotated;
                SetGridCellSprite(_card.Grid[i,j,o], i, j);
                
            }
        }
    }

    private void SetGridCellSprite(TileEffectEnum tileEffect, int i, int j)
    {
        var row = i switch
        {
            0 => _gridImagesRow0,
            1 => _gridImagesRow1,
            2 => _gridImagesRow2,
            3 => _gridImagesRow3,
            4 => _gridImagesRow4,
            _ => _gridImagesRow0
        };
        
        var image = row[j];
        var col = image.color;
        col.a = 1;
        image.color = col;
        
        switch (tileEffect)
        {
            case TileEffectEnum.None:
                col.a = 0;
                image.color = col;
                break;
            case TileEffectEnum.Pawn:
                image.sprite = _gridPawn;
                break;
            case TileEffectEnum.Effect:
                image.sprite = _gridEffect;
                break;
            case TileEffectEnum.PawnAndEffect:
                image.sprite = _gridPawnEffect;
                break;
            case TileEffectEnum.Center:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(tileEffect), tileEffect, null);
        }
    }
    
    private void OnValidate()
    {
        if(_card != null)
            SetCardData(_card);
    }
}
