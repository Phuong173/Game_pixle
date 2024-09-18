using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using GameTool;
using Pixel_Adventure_1.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinPopup : BaseUI
{
    [SerializeField] private RectTransform winImgRct;
    [SerializeField] private Button continueBtn;
    [SerializeField] private RectTransform continueBtnRct;
    [SerializeField] private Image bgImg;

    private void Awake()
    {
        continueBtn.onClick.AddListener(()=>
        {
            TransistionScene.I.EndScene(() => SceneManager.LoadScene(0));
            continueBtn.interactable = false;
            GameData.I.CurrentLevel++;
        });
    }

    private void OnEnable()
    {
        winImgRct.anchoredPosition = new Vector2(0, 2000);
        winImgRct.DOAnchorPosY(180,1f);
        continueBtnRct.anchoredPosition = new Vector2(0, -2000);
        continueBtnRct.DOAnchorPosY(-230,1f);
        Color color = Color.black;
        color.a = 0;
        bgImg.color = color;
        bgImg.DOFade(100f/255, 0.5f);
        continueBtn.interactable = true;
    }
}
