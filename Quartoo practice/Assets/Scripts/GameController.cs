using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Button[] buttonList;
    public List<GamePiece> gamePieces;
    public List<GamePiece> usedPieces;
    public List<GamePiece> availablePieces;
    public GamePiece selectedPiece;
    public Button recentMove;
    private string playerSide;
    private int moveCount;

    void SetGameControllerReferenceOnButtons()
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponent<GameBoard>().SetGameControllerReference(this);
            availablePieces[i].GetComponent<GamePiece>().SetGameControllerReference(this);
        }
    }

    void SetGameControllerReferenceOnUsedPiece()
    {
        usedPieces[usedPieces.Count - 1].GetComponent<GamePiece>().SetGameControllerReference(this);
    }

    void Awake()
    {
        SetGameControllerReferenceOnButtons();
        playerSide = "X";
        moveCount = 0;
    }

    public void SetRecentMove(Button button)
    {
        recentMove = button;
    }

    public Button GetRecentMove()
    {
        return recentMove;
    }

    public GamePiece GetSelectedPiece()
    {
        return selectedPiece;
    }

    public void SetSelectedPiece(GamePiece gamePiece)
    {
        selectedPiece = gamePiece;
    }

    public void RemoveFromAvailablePieces(GamePiece gamePiece)
    {
        availablePieces.Remove(gamePiece);
    }

    public void AddToUsedPieces(GamePiece gamePiece)
    {
        usedPieces.Add(gamePiece);
        SetGameControllerReferenceOnUsedPiece();
    }

    public void EndTurn()
    {
        moveCount++;

        // Checks the rows
        if (CheckWinCondition(buttonList[0], buttonList[1], buttonList[2], buttonList[3]))
            GameOver();
        else if (CheckWinCondition(buttonList[4], buttonList[5], buttonList[6], buttonList[7]))
            GameOver();
        else if (CheckWinCondition(buttonList[8], buttonList[9], buttonList[10], buttonList[11]))
            GameOver();
        else if (CheckWinCondition(buttonList[12], buttonList[13], buttonList[14], buttonList[15]))
            GameOver();


        // Checks the cols
        if (CheckWinCondition(buttonList[0], buttonList[4], buttonList[8], buttonList[12]))
            GameOver();
        else if (CheckWinCondition(buttonList[1], buttonList[5], buttonList[9], buttonList[13]))
            GameOver();
        else if (CheckWinCondition(buttonList[2], buttonList[6], buttonList[10], buttonList[14]))
            GameOver();
        else if (CheckWinCondition(buttonList[3], buttonList[7], buttonList[11], buttonList[15]))
            GameOver();

        // Checks the main diagonal (left to right)
        if (CheckWinCondition(buttonList[0], buttonList[5], buttonList[10], buttonList[15]))
            GameOver();

        // Checks the secondary diagonal (right to left)
        if (CheckWinCondition(buttonList[3], buttonList[6], buttonList[9], buttonList[12]))
            GameOver();
        // Checks for a tie
        if (moveCount >= 16)
            GameOver();

        if (moveCount == 5)
            RestartGame();

        changeSides();
    }

    bool CheckWinCondition(Button btn1, Button btn2, Button btn3, Button btn4)
    {
        GamePiece gamePiece1 = btn1.GetComponentInChildren<GamePiece>();
        GamePiece gamePiece2 = btn2.GetComponentInChildren<GamePiece>();
        GamePiece gamePiece3 = btn3.GetComponentInChildren<GamePiece>();
        GamePiece gamePiece4 = btn4.GetComponentInChildren<GamePiece>();

        // Height will only be either 0 or 1 if a gamePiece was set there; if it is 2 then piece was set by user         
        if (gamePiece1.height == 2 || gamePiece2.height == 2 || gamePiece3.height == 2 || gamePiece4.height == 2)
            return false;

        // checks if there are 4 GamePieces next to each other with similiar stats
        if (gamePiece1.height == gamePiece2.height && gamePiece1.height == gamePiece3.height && gamePiece1.height == gamePiece4.height)
            return true;

        else if (gamePiece1.color == gamePiece2.color && gamePiece1.color == gamePiece3.color && gamePiece1.color == gamePiece4.color)
            return true;

        else if (gamePiece1.emblem == gamePiece2.emblem && gamePiece1.emblem == gamePiece3.emblem && gamePiece1.emblem == gamePiece4.emblem)
            return true;

        else if (gamePiece1.type == gamePiece2.type && gamePiece1.type == gamePiece3.type && gamePiece1.type == gamePiece4.type)
            return true;

        // if there arent any conditions met, that means that there isn't a winner
        return false;
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
        usedPieces.Clear();
        availablePieces = gamePieces;

        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInChildren<GamePiece>().height = 2;
            buttonList[i].GetComponentInChildren<GamePiece>().emblem = 2;
            buttonList[i].GetComponentInChildren<GamePiece>().color = 2;
            buttonList[i].GetComponentInChildren<GamePiece>().type = 2;
        }
        SetBoardInteractable(true);
    }

    public void SetBoardInteractable(bool toggle)
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<Button>().interactable = toggle;
        }
    }
}
