using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    GameBoard gameBoard = new GameBoard();
    List<GamePiece> availablePieces = new List<GamePiece>();
    Player player1 = new Player();
    Player player2 = new Player();

    bool exitGame = false;

    void setAvailablePieces ()
    {
        availablePieces = gameBoard.getAvailablePieces();
    }

    void runGame()
    {
        while (exitGame == false)
        {
            if (!gameBoard.placeGamePiece())
                continue;

            if (gameBoard.isGameOver())
            {
                if (gameBoard.startNewGame())
                {
                    gameBoard = new GameBoard();
                    setAvailablePieces();
                }
                else
                    exitGame = true;
            }
            else
                setAvailablePieces();
        }
    }
}
