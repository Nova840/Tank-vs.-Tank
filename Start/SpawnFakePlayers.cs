using UnityEngine;
using System.Collections;
using System.Linq;

[RequireComponent(typeof(StartPlayersCamera))]
public class SpawnFakePlayers : MonoBehaviour {

    [SerializeField]
    private GameObject fakePlayerPrefab;

    [SerializeField]
    private Transform spawnpoint1 = null, spawnpoint2 = null, spawnpoint3 = null, spawnpoint4 = null;
    private Transform[] spawnpoints = new Transform[4];
    private GameObject[] fakePlayers = new GameObject[4];

    private StartPlayersCamera startPlayersCamera;

    private void Awake() {
        startPlayersCamera = GetComponent<StartPlayersCamera>();

        spawnpoints[0] = spawnpoint1;
        spawnpoints[1] = spawnpoint2;
        spawnpoints[2] = spawnpoint3;
        spawnpoints[3] = spawnpoint4;
    }

    private void Start() {
        for (int i = 0; i < Players.playerControlSchemes.Length; i++)//includes null values
            Spawn(Players.playerControlSchemes[i], i);
    }

    private void Update() {
        if (StageSelectController.stageSelect)
            return;

        AddPlayerIfCanSpawn(Controls.keyboard);
        AddPlayerIfCanSpawn(Controls.joystick1);
        AddPlayerIfCanSpawn(Controls.joystick2);
        AddPlayerIfCanSpawn(Controls.joystick3);
        AddPlayerIfCanSpawn(Controls.joystick4);

        DespawnIfInput(Controls.keyboard);
        DespawnIfInput(Controls.joystick1);
        DespawnIfInput(Controls.joystick2);
        DespawnIfInput(Controls.joystick3);
        DespawnIfInput(Controls.joystick4);
    }

    private void AddPlayerIfCanSpawn(ControlScheme controls) {
        if (CanSpawn(controls))
            AddPlayer(controls);
    }

    private void DespawnIfInput(ControlScheme controls) {
        if (Input.GetButtonDown(controls.Respawn))
            Despawn(controls);
    }

    private bool CanSpawn(ControlScheme controls) {
        return Input.GetButtonDown(controls.Shoot) && !Players.playerControlSchemes.Contains(controls) && fakePlayers.Any(p => !p);
    }

    private void AddPlayer(ControlScheme controls) {
        for (int i = 0; i < Players.playerControlSchemes.Length; i++) {
            if (!Players.playerControlSchemes[i]) {
                Spawn(controls, i);
                return;
            }
        }
    }

    private void Spawn(ControlScheme controls, int index) {
        if (!controls)
            return;
        Players.playerControlSchemes[index] = controls;
        fakePlayers[index] = (GameObject)Instantiate(fakePlayerPrefab, spawnpoints[index].position, spawnpoints[index].rotation);
        startPlayersCamera.targetIndex = LastPlayerIndex() + 1;
    }

    private void Despawn(ControlScheme controls) {
        for (int i = 0; i < Players.playerControlSchemes.Length; i++) {
            if (Players.playerControlSchemes[i] == controls) {
                Players.playerControlSchemes[i] = null;
                Destroy(fakePlayers[i]);
                startPlayersCamera.targetIndex = LastPlayerIndex() + 1;
                return;
            }
        }
    }

    private int LastPlayerIndex() {
        for (int i = Players.playerControlSchemes.Length - 1; i >= 0; i--) {
            if (Players.playerControlSchemes[i] != null)
                return i;
        }
        return -1;
    }

    public void DespawnAllPlayers() {
        foreach (GameObject g in fakePlayers)
            Destroy(g);
        startPlayersCamera.targetIndex = 0;
    }

}
