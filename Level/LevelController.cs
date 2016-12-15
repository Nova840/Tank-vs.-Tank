using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(LoseOnPlayersDead))]
public class LevelController : MonoBehaviour {

    [System.Serializable]
    private class Wave {
        [SerializeField]
        private GameObject[] enemies;

        [SerializeField]
        private MoveLevel[] waveObstacles = new MoveLevel[0];

        [SerializeField]
        private GameObject eventContainer;

        [SerializeField]
        private float eventDelay = 0, endShakeDuration = 0, endShakeMagnitude = .2f;

        public GameObject[] Enemies { get { return enemies; } }
        public MoveLevel[] WaveObstacles { get { return waveObstacles; } }
        public GameObject EventContainer { get { return eventContainer; } }
        public float EventDelay { get { return eventDelay; } }
        public float EndShakeDuration { get { return endShakeDuration; } }
        public float EndShakeMagnitude { get { return endShakeMagnitude; } }
    }

    [SerializeField]
    private GameObject alertPrefab;
    [SerializeField]
    private string winMessage = "LEVEL COMPLETE";
    [SerializeField]
    private Text winText;
    [SerializeField]
    private float winDuration = 3;

    [SerializeField]
    private Wave[] waves;

    private int currentWaveIndex = -1;

    [SerializeField]
    private Transform enemySpawnPointContainer;

    private List<Transform> enemySpawnPoints = new List<Transform>();

    private bool winCoroutineStarted = false;

    private void Awake() {
        foreach (Transform t in enemySpawnPointContainer.GetComponentsInChildren<Transform>())
            if (t != enemySpawnPointContainer)
                enemySpawnPoints.Add(t);
        NextWave(false);
    }

    private void Update() {
        if (!winCoroutineStarted && Scripts.Players.enemiesDead() && Scripts.Players.NumberOfSpawners() == 0)
            NextWave(true);
    }

    private void NextWave(bool movePrevious) {
        if (IsLastWave()) {
            WinGame();
            return;
        }

        if (movePrevious)
            MoveLevels(waves[currentWaveIndex].WaveObstacles);

        foreach (GameObject camera in Scripts.Players.playerCameras)
            camera.GetComponent<CameraShake>().Shake(waves[currentWaveIndex].EndShakeDuration, waves[currentWaveIndex].EndShakeMagnitude);

        currentWaveIndex++;
        Wave currentWave = waves[currentWaveIndex];

        Instantiate(alertPrefab).GetComponentInChildren<Text>().text = IsLastWave() ? "Final Wave" : "Wave " + (currentWaveIndex + 1);

        if (currentWave.EventContainer)
            StartCoroutine(CreateEvent(currentWave.EventContainer, currentWave.EventDelay));

        MoveLevels(currentWave.WaveObstacles);

        List<Transform> spawnpoints = new List<Transform>(enemySpawnPoints);
        foreach (GameObject enemy in currentWave.Enemies) {
            int spawnpointIndex = Random.Range(0, spawnpoints.Count);
            Transform spawnpoint = spawnpoints[spawnpointIndex];
            Instantiate(enemy, spawnpoint.position, spawnpoint.rotation);
            spawnpoints.RemoveAt(spawnpointIndex);
        }

    }

    public static void MoveLevels(MoveLevel[] levels) {
        foreach (MoveLevel ml in levels)
            ml.Move();
    }

    private bool IsLastWave() {
        return currentWaveIndex == waves.Length - 1;
    }

    private static IEnumerator CreateEvent(GameObject e, float time) {
        yield return new WaitForSeconds(time);
        Instantiate(e);
    }

    private void WinGame() {
        winText.gameObject.SetActive(true);
        winText.text = winMessage;
        GetComponent<LoseOnPlayersDead>().enabled = false;
        StartCoroutine(ReturnToStart(winDuration));
    }

    private IEnumerator ReturnToStart(float seconds) {
        winCoroutineStarted = true;
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene("Start");
    }

}
