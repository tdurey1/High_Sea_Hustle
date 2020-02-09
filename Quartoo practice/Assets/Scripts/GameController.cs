using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text[] buttonList;
    private string playerSide;
    private int moveCount;

    void SetGameControllerReferenceOnButtons()
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<GameBoard>().SetGameControllerReference(this);
        }
    }

    void Awake()
    {
        SetGameControllerReferenceOnButtons();
        playerSide = "X";
        moveCount = 0;
    }

    public string GetPlayerSide()
    {
        return  playerSide;
    }

    public void EndTurn()
    {
        moveCount++;

        if (buttonList[0].text == playerSide && buttonList[1].text == playerSide && buttonList[2].text == playerSide)
        {
            GameOver();
        }

        if (moveCount >= 16)
        {
            GameOver();
        }

        changeSides();
    }

    void GameOver()
    {
        SetBoardInteractable(false);
    }

    void changeSides()
    {
        playerSide = (playerSide == "X") ? "O" : "X";
    }


    public void RestartGame()
    {
        playerSide = "X";
        moveCount = 0;

        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].text = "";
        }
        SetBoardInteractable(false);
    }

    public void SetBoardInteractable (bool toggle)
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<Button>().interactable = toggle;
        }
    }
}
