using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class EventController : MonoBehaviour {

    [SerializeField]
    private GameObject[] eventPrefabs = null, rareEventPrefabs = null;
    private GameObject currentEvent;

    [SerializeField]
    private int chanceOfRareEvent = 1000;

    [SerializeField]
    private float minInterval = 60, maxInterval = 90;

    private int lastEventIndex = -1;

    private void Start() {
        List<GameObject> temp = eventPrefabs.ToList();
        temp.RemoveAll(e => e.name == "Fog Event");//temporary because fog event doesn't work.
        eventPrefabs = temp.ToArray();

        StartCoroutine(StartEvent());
    }

    private IEnumerator StartEvent() {
        while (true) {
            yield return new WaitForSeconds(Random.Range(minInterval, maxInterval));
            if (!currentEvent)
                if (rareEventPrefabs.Length > 0 && Random.Range(0, chanceOfRareEvent) == 0)
                    currentEvent = Instantiate(rareEventPrefabs[Random.Range(0, rareEventPrefabs.Length)]);
                else {
                    lastEventIndex = Extensions.RandomIntExcluding(0, eventPrefabs.Length, lastEventIndex);
                    currentEvent = Instantiate(eventPrefabs[lastEventIndex]);
                }
        }
    }

}
