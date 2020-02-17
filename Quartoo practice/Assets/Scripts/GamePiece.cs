using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePiece : MonoBehaviour
{
    private GameController gameController;
    public int color;
    public int height;
    public int type;
    public int emblem;
    public int id;

    // Constructor
    public GamePiece(int color, int height, int type, int emblem, int id)
    {
        this.color = color;
        this.height = height;
        this.type = type;
        this.emblem = emblem;
        this.id = id;
    }

    public void SetGameControllerReference(GameController controller)
    {
        gameController = controller;
    }

    private void OnMouseDown()
    {
        gameController.SetSelectedPiece(this);
    }

}
