using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//https://www.youtube.com/watch?v=a1RFxtuTVsk
public class TutorialManager : MonoBehaviour
{
    public GameObject[] popups;
    private int popupIndex = 0;

    //these are temporary:
    private string[] captions =
    {
        /*0 -*/ "Hello! I'm Peter Parrot and I'll be showing you how to play Quarto. Click the arrow on the bottom right to continue.",
        /*1 -*/ "The objective of this game is to create a line of four pieces which share at least one common characteristic.",
        /*2 -*/ "These characteristics are color, height, shape, and stamp. The line can be vertical, horizontal, or diagonal.",
        /*3 HS*/ "The game begins with the first player selecting a piece for their opponent to place. Click the hilighted piece and then the " +
                "check to confirm that's the piece you'd like to send.",
        /*4 CP*/ "Great! Now, each turn will have two parts: placing a piece and selecting your opponents piece. I will start my turn by first placing the piece you gave me.",
        /*5 CS*/ "Now that I've placed the piece you gave to me, I will give you a piece to place.",
        /*6 HP*/ "Here you go! Now, place the piece on the highlighted board space.",
        /*7 -*/ "Good Job! Normally you would now select a piece to give to me as you did before, but instead let's jump ahead a few turns.",
        /*8 -*/ "If you look at the board now, there is a potential win for a piece that is either tall or triangular.",
        /*9 HS*/ "It's your turn to select a piece for me, but you want to make sure not to give me a piece that will cause me to win. Click the highlighted piece.",
        /*10 CP*/ "I've blocked the winning move, but in doing so I have made a new one for a piece that has a stamp. Now I will select a piece for you to place.",
        /*11 CS HP*/ "You have just been given a winning piece! Place it in the hilighted board space to win by matching four pieces with a stamp.",
        /*12*/ "Congratulations, you've just completed your first game of Quarto! If you ever forget the rules while playing, just click on the question mark! Goodbye!"
    };

    public int GetPopupIndex()
    {
        return popupIndex;
    }

    public string ShowNextStep()
    {
        popupIndex++;

        //
        return getCurrentCaption();
        //

        //for (int i = 0; i < popups.Length; i++)
        //{
        //    if (i == popupIndex)
        //    {
        //        popups[i].SetActive(true);
        //    }
        //    else
        //    {
        //        popups[i].SetActive(false);
        //    }
        //}
    }

    public string getCurrentCaption()
    {
        return captions[popupIndex];
    }

    void Start()
    {
        //Show the initial step
    }


    //Hello, I'm Peter Parrot and I'll be showing you how to play.
    //The objective of this game is to create a line of four pieces which share at least one common characteristic.
    //These characteristics are color, height, shape, and stamp. The line can be vertical, horizontal, or diagonal.
    //***user input begins***
    //The game begins with the first player sending a move to their opponent. Click the hilighted piece and then the check to
    //   confirm that's the piece you'd like to send.
    //Your opponent will place the piece on the board, then send you a piece to place. Turns continue in this way for the rest of the game.
    //Let's jump ahead a few turns.
    //If you look at the board now, there is a place for a potential win if a piece is _____ or _____. Make sure you don't give your
    //   opponent a piece with these characteristics!
    //Your opponent has given you a winning piece! Place it in the hilighted board space to win by ______ (characteristic).
    //You've just completed your first game of Quarto! 
}
