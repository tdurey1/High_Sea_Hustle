public class GameInfo
{
    public static bool firstGame = true;

    public static bool doubleClickConfirm = true;

    // User related variables
    public static string username;

    public static int usernameLength = 20;

    public static string avatar;

    // N = Network, E = Easy AI Game, H = Hard AI Game, S = Story Mode
    public static char gameType;

    // E = Story Mode Easy Game, H = Story Mode Hard Game, T = Neither (did T since N might be confused for Network)
    public static char storyModeType;

    // 1 = player 1 (host of network game; human of quick game) 2 = player 2 (player who joins network game; ai of quick game)
    public static int selectPieceAtStart;

    public static float musicVolume = .5f;

    public static float soundFXVolume = .5f;

    public static int aiDelayBoardSpace = 0;

    public static int aiDelayPiece = 0;
}