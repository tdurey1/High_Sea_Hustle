using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCore : MonoBehaviour
{
    private GameController gameController;
    private int row = 1;
    private int col = 0;
    // initialize GameBoard with empty slots
    public GamePiece[][] gameBoard = new GamePiece[4][] {
            new GamePiece[] {null, null, null, null },
            new GamePiece[] {null, null, null, null },
            new GamePiece[] {null, null, null, null },
            new GamePiece[] {null, null, null, null }
    };

    // usedPieces is empty until a game piece is set
    public List<GamePiece> usedPieces = new List<GamePiece>();

    // initialize availablePieces (it will include all game pieces until the first move)
    public List<GamePiece> availablePieces = new List<GamePiece> {
        new GamePiece( 0, 0, 0, 0, 0 ),
        new GamePiece( 0, 0, 0, 1, 1 ),
        new GamePiece( 0, 0, 1, 0, 2 ),
        new GamePiece( 0, 0, 1, 1, 3 ),
        new GamePiece( 0, 1, 0, 0, 4 ),
        new GamePiece( 0, 1, 0, 1, 5 ),
        new GamePiece( 0, 1, 1, 0, 6 ),
        new GamePiece( 0, 1, 1, 1, 7 ),

        // silver GamePieces                                         
        new GamePiece( 1, 0, 0, 0, 8 ),
        new GamePiece( 1, 0, 0, 1, 9 ),
        new GamePiece( 1, 0, 1, 0, 10 ),
        new GamePiece( 1, 0, 1, 1, 11 ),
        new GamePiece( 1, 1, 0, 0, 12 ),
        new GamePiece( 1, 1, 0, 1, 13 ),
        new GamePiece( 1, 1, 1, 0, 14 ),
        new GamePiece( 1, 1, 1, 1, 15 ),
    };

    public void SetGameControllerReference(GameController controller)
    {
        gameController = controller;
    }

    public bool SetPiece(GamePiece gamePiece, string position)
    {
        
        // updates variables row and col
        ConvertPosition(position);

        GamePiece convertedGamepiece = ConvertGamePiece(gamePiece);
        usedPieces.Add(convertedGamepiece);
        availablePieces.Remove(convertedGamepiece);
        gameBoard[row][col] = convertedGamepiece;
        Debug.Log(gameBoard[row][col].color);
        return (EndTurn() ? true : false);
    }

    // Check a substring of position, and set row/col to the correct values that correspond
    // with the gridspaces in unity
    private void ConvertPosition(string position)
    {
        try
        {
            string subStringPosition = position.Substring(11);

            switch (subStringPosition)
            {
                case "(1)":
                    row = 0;
                    col = 1;
                    break;
                case "(2)":
                    row = 0;
                    col = 2;
                    break;
                case "(3)":
                    row = 0;
                    col = 3;
                    break;
                case "(4)":
                    row = 1;
                    col = 0;
                    break;
                case "(5)":
                    row = 1;
                    col = 1;
                    break;
                case "(6)":
                    row = 1;
                    col = 2;
                    break;
                case "(7)":
                    row = 1;
                    col = 3;
                    break;
                case "(8)":
                    row = 2;
                    col = 0;
                    break;
                case "(9)":
                    row = 2;
                    col = 1;
                    break;
                case "(10)":
                    row = 2;
                    col = 2;
                    break;
                case "(11)":
                    row = 2;
                    col = 3;
                    break;
                case "(12)":
                    row = 3;
                    col = 0;
                    break;
                case "(13)":
                    row = 3;
                    col = 1;
                    break;
                case "(14)":
                    row = 3;
                    col = 2;
                    break;
                case "(15)":
                    row = 3;
                    col = 3;
                    break;
            }
        }
        catch
        {
            row = 0;
            col = 0;
        }
    }

    private GamePiece ConvertGamePiece(GamePiece gamePiece)
    {
        GamePiece convertedGamePiece = new GamePiece(
            gamePiece.color,
            gamePiece.height,
            gamePiece.type,
            gamePiece.emblem,
            gamePiece.id
        );

        return convertedGamePiece;

    }

    public GamePiece[][] GetGameBoard()
    {
        return gameBoard;
    }

    public void setGameBoard()
    {

    }

    private bool EndTurn()
    {
        //// checks the rows
        //for (int i = 0; i < gameBoard.Length; i++)
        //{
        //    GamePiece[] result = gameBoard[i];
        //    if (checkWinConditions(result[0], result[1], result[2], result[3]))
        //        return true;
        //}

        //// checks the cols
        //for (int i = 0; i < gameBoard.Length; i++)
        //{
        //    GamePiece[] result = new GamePiece[4];
        //    for (int j = 0; j < 4; j++)
        //        result[j] = gameBoard[j][i];
        //    if (checkWinConditions(result[0], result[1], result[2], result[3]))
        //        return true;
        //}

        // checks the main diagonal (left to right)
        if (checkWinConditions(gameBoard[0][0], gameBoard[1][1], gameBoard[2][2], gameBoard[3][3]))
            return true;

        //// checks the secondary diagonal (right to left)
        //if (checkWinConditions(gameBoard[0][3], gameBoard[1][2], gameBoard[2][1], gameBoard[3][0]))
        //    return true;

        // Checks for a tie
        if (usedPieces.Count >= 16)
            return true;

        return false;
    }

    // Checks all possible conditions of a winning move
    private bool checkWinConditions(GamePiece a, GamePiece b, GamePiece c, GamePiece d)
    {
        // checks if the other gameBoard of the game board are empty (no GamePieces on them)          
        if (a == null || b == null || c == null || d == null)
            return false;

        // checks if there are 4 GamePieces next to each other with similiar stats
        if (a.height == b.height && a.height == c.height && a.height == d.height)
            return true;

        else if (a.color == b.color && a.color == c.color && a.color == d.color)
            return true;

        else if (a.emblem == b.emblem && a.emblem == c.emblem && a.emblem == d.emblem)
            return true;

        else if (a.type == b.type && a.type == c.type && a.type == d.type)
            return true;

        // if there arent any conditions met, that means that there isn't a winner
        return false;
    }

    // Might not need this since gameController can just instantiate a new gameCore
    //public void ResetGameBoard()
    //{
    //    usedPieces.Clear();

    //}
}
