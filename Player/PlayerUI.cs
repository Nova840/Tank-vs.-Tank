using UnityEngine;
using System.Collections;

public class PlayerUI : MonoBehaviour {

    [SerializeField]
    private GameObject playerScreenPrefab = null, deadCanvasPrefab = null;

    [SerializeField]
    private VersusLives versusLives = null;//for countdown, can be null

    private GameObject[] playerScreens = new GameObject[4];
    public GameObject[] PlayerScreens { get { return playerScreens; } }
    private GameObject[] playerRespawnCanvases = new GameObject[4];

    private void Start() {//Awake happens too early to call PlayerNumberAtIndex()

        foreach (GameObject player in Scripts.Players.players) {
            player.GetComponent<Health>().playerUI = this;
            player.GetComponent<PlayerSpawn>().playerUI = this;
        }

        int numberOfPlayers = Players.NumberOfPlayers;

        if (numberOfPlayers == 1) {

            playerScreens[0] = CreateScreen(Players.PlayerNumberAtIndex(0));

        } else if (numberOfPlayers == 2) {

            playerScreens[0] = CreateScreen(Players.PlayerNumberAtIndex(0));
            playerScreens[1] = CreateScreen(Players.PlayerNumberAtIndex(1));

            playerScreens[0].GetComponent<RectTransform>().anchorMax = new Vector2(.5f, 1);

            playerScreens[1].GetComponent<RectTransform>().anchorMin = new Vector2(.5f, 0);

        } else if (numberOfPlayers == 3) {

            playerScreens[0] = CreateScreen(Players.PlayerNumberAtIndex(0));
            playerScreens[1] = CreateScreen(Players.PlayerNumberAtIndex(1));
            playerScreens[2] = CreateScreen(Players.PlayerNumberAtIndex(2));

            RectTransform p0RT = playerScreens[0].GetComponent<RectTransform>();
            p0RT.anchorMin = new Vector2(0, .5f);
            p0RT.anchorMax = new Vector2(.5f, 1);

            playerScreens[1].GetComponent<RectTransform>().anchorMin = new Vector2(.5f, .5f);

            playerScreens[2].GetComponent<RectTransform>().anchorMax = new Vector2(.5f, 1);

            transform.Find("P4 Fill Image").gameObject.SetActive(true);//for the space by having no P4

        } else if (numberOfPlayers == 4) {

            playerScreens[0] = CreateScreen(Players.PlayerNumberAtIndex(0));
            playerScreens[1] = CreateScreen(Players.PlayerNumberAtIndex(1));
            playerScreens[2] = CreateScreen(Players.PlayerNumberAtIndex(2));
            playerScreens[3] = CreateScreen(Players.PlayerNumberAtIndex(3));

            RectTransform p0RT = playerScreens[0].GetComponent<RectTransform>();
            p0RT.anchorMin = new Vector2(0, .5f);
            p0RT.anchorMax = new Vector2(.5f, 1);

            playerScreens[1].GetComponent<RectTransform>().anchorMin = new Vector2(.5f, .5f);

            playerScreens[2].GetComponent<RectTransform>().anchorMax = new Vector2(.5f, 1);

            RectTransform p3RT = playerScreens[3].GetComponent<RectTransform>();
            p3RT.anchorMin = new Vector2(.5f, 0);
            p3RT.anchorMax = new Vector2(1, .5f);

        }
    }

    private GameObject CreateScreen(int playerNumber) {
        GameObject screen = Instantiate(playerScreenPrefab);
        screen.transform.SetParent(transform, false);
        screen.name = "Player " + playerNumber + " Screen";
        screen.GetComponentInChildren<SetTextToHealth>().playerNumber = playerNumber;
        screen.GetComponentInChildren<SetSliderToHealth>().playerNumber = playerNumber;
        screen.GetComponentInChildren<BulletDisplay>().playerNumber = playerNumber;
        screen.GetComponentInChildren<AmmoDisplayController>().playerNumber = playerNumber;

        screen.transform.Find("Health Display").localScale = ScreenScale();
        screen.transform.Find("Ammo Display").localScale = ScreenScale();

        return screen;
    }

    private static Vector3 ScreenScale() {
        if (Players.NumberOfPlayers > 2)
            return new Vector3(.75f, .75f, 1);
        else if (Players.NumberOfPlayers == 2)
            return new Vector3(.85f, .85f, 1);
        else
            return Vector3.one;
    }

    public void DisplayRespawn(int playerIndex) {
        if (Scripts.Reference.GetComponent<LoseOnPlayersDead>())//happens in co-op
            return;

        GameObject canvas = Instantiate(deadCanvasPrefab);
        canvas.name = "Dead Canvas [" + playerIndex + "]";

        RectTransform area = canvas.transform.Find("Area").GetComponent<RectTransform>();

        if (Scripts.Reference.GetComponent<PlayersCanRespawn>()) {
            area.Find("Respawn Text").gameObject.SetActive(true);
            if (Players.GetControlSchemeForPlayer(Players.PlayerNumberAtIndex(playerIndex)) == Controls.keyboard)
                area.Find("Key Image").gameObject.SetActive(true);
            else
                area.Find("Controller Image").gameObject.SetActive(true);
        } else if (versusLives && versusLives.Lives[playerIndex] > 1) {//lives have not gone down yet, could not find a better way
            GameObject g = area.Find("Dead Countdown Text").gameObject;
            g.GetComponent<CountdownInText>().countdownTime = versusLives.RespawnTime;
            g.SetActive(true);
        } else {
            area.Find("Dead Text").gameObject.SetActive(true);
        }

        RectTransform screenRT = playerScreens[playerIndex].GetComponent<RectTransform>();
        area.anchorMin = screenRT.anchorMin;
        area.anchorMax = screenRT.anchorMax;
        playerRespawnCanvases[playerIndex] = canvas;
    }

    public void RemoveRespawn(int playerIndex) {
        Destroy(playerRespawnCanvases[playerIndex]);
    }

}
