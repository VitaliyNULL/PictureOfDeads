using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IconIndexNum
{
    One,
    Two,
    Three,
    Four,
    Five,
    Six,
    Seven,
};
public class IconIndex : MonoBehaviour
{
    public IconIndexNum iconIndexNum;
    private GameManager gameManager;
    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void CheckItemInfo()
    {
        IconIndex iconIndex = GetComponent<IconIndex>();
        IconIndexNum currentIconIndexNum = iconIndex.iconIndexNum;
        gameManager.ShowCurrentItemInfo(currentIconIndexNum);
    }
}
