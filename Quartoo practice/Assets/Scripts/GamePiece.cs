using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePiece : MonoBehaviour
{
    private GameController gameController;

    public void SetGameControllerReference(GameController controller)
    {
        gameController = controller;
    }

    GameObject gamePiece;

    public int height = 0;
    public int emblem = 0;
    public int color = 0;
    public int type = 0;
    public string bitValue = "";

    private void OnMouseDown()
    {
        gameController.SetSelectedPiece(this);
    }
}
