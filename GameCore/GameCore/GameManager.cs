namespace GameCore
{
    class GameManager
    {
        static void Main(string[] args)
        {
            GameBoard gameBoard = new GameBoard();
            bool exitGame = false;
            gameBoard.drawBoard();
            gameBoard.piecesRemaining();

            while (exitGame == false)
            {
                if (!gameBoard.placeGamePiece())
                    continue;

                if (gameBoard.isGameOver())
                {
                    if (gameBoard.startNewGame())
                    {
                        gameBoard = new GameBoard();
                        gameBoard.drawBoard();
                        gameBoard.piecesRemaining();
                    }
                    else
                        exitGame = true;
                }
                else
                {
                    gameBoard.drawBoard();
                    gameBoard.piecesRemaining();
                }
            }
        }
    }
}
