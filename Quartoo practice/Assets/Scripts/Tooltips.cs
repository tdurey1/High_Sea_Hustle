using System.Collections.Generic;
using UnityEngine;

public class Tooltips : MonoBehaviour
{
    private List<int> usedTooltips = new List<int>();
    private int popupIndex = 0;
    private int tooltipIndex;
    public string[] tooltips =
    {
        /*0: Double click */"Click the piece once to select, and a second time to confirm you want to send it to your opponent. You can disable this to a single click in settings.",
        /*1: Help */"Forgot the rules or win conditions? Click the question mark above!",
        /*2: Staging*/"After you or your opponent has selected a piece, it is placed at the bottom of the board.",
        /*3: Any piece */"You can select any piece (gold or silver) to send to your opponent.",
        /*4: Songs */"Tired of this song? Go to settings and skip to the next or previous song."
    };

    public int getUsedTooltipLength()
    {
        return usedTooltips.Count;
    }

    public string ShowTooltip()
    {
        tooltipIndex = Random.Range(0, tooltips.Length);

        while (usedTooltips.Contains(tooltipIndex))
            tooltipIndex = Random.Range(0, tooltips.Length);

        usedTooltips.Add(tooltipIndex);

        return tooltips[tooltipIndex];
    }
}