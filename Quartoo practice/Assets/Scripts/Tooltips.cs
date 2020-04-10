using UnityEngine;

public class Tooltips : MonoBehaviour
{
    public GameObject[] popups;
    private int popupIndex = 0;

    private string[] tooltips =
    {
        /*0: Double click */"Click the piece once to select, and a second time to confirm you want to send it to your opponent. You can disable this to a single click in settings.",
        /*1: Help */"Forgot the rules or win conditions? Click the question mark above!",
        /*2: Staging*/"After you or your opponent has selected a piece, it is placed at the bottom of the bored.",
        /*3: Any piece */"You can select any piece (gold or silver) to send to your opponent.",
        /*4: Songs */"Tired of this song? Go to settings and skip to the next or previous song."
    };

    public int GetPopupIndex()
    {
        return popupIndex;
    }

    public string ShowTooltip(int tooltipIndex)
    {
        return tooltips[tooltipIndex];
    }
}