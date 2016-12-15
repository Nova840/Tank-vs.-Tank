using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class VersusLevel : MonoBehaviour {

    [System.Serializable]
    private class LevelObstacles {//as a class so it will appear in the editor
        [SerializeField]
        private MoveLevel[] obstacles = new MoveLevel[0];
        public MoveLevel[] Obstacles { get { return obstacles; } }
    }

    [SerializeField]
    private LevelObstacles[] levelObstacles;

    [SerializeField]
    private float shakeDuration = 0, shakeMagnitude = .2f;

    private int currentIndex = -1;

    [SerializeField]
    private float minInterval = 45, maxInterval = 75;

    private void Start() {
        if (levelObstacles.Length > 0)
            StartCoroutine(Switch());
    }

    private IEnumerator Switch() {
        while (true) {
            yield return new WaitForSeconds(Random.Range(minInterval, maxInterval));
            Move();
            int oldIndex = currentIndex;
            currentIndex = Extensions.RandomIntExcluding(-1, levelObstacles.Length, oldIndex);
            Move();
            foreach (GameObject camera in Scripts.Players.playerCameras)
                camera.GetComponent<CameraShake>().Shake(shakeDuration, shakeMagnitude);
        }
    }

    private void Move() {
        if (currentIndex >= 0)
            LevelController.MoveLevels(levelObstacles[currentIndex].Obstacles);
    }

}
