using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayZombieSpawnScript : MonoBehaviour
{
	public float spawnTime = 10f;
	float tempSpawnTime = 0f;
	float spawnCounter = 0.0f;

	public float minTime = 2f;
	public float maxTime = 4f;

	public float spawnRate = 0.5f;

	bool isRandomize = false;

	void Start ()
	{
		tempSpawnTime = spawnTime;
	}

	void Update ()
	{
		CheckSpawn ();
	}

	void CheckSpawn ()
	{
		if (!isRandomize) {

			tempSpawnTime += Random.Range (Mathf.Abs (minTime), Mathf.Abs (maxTime));

			isRandomize = true;
		}

		if (isRandomize) {

			spawnCounter += Time.deltaTime;

			if (spawnCounter >= tempSpawnTime) {

				//CustomObjectPoolScript.Instance.Spawn ("Zombie", new Vector3 (transform.position.x + Random.Range (-2f, 2f), transform.position.y, transform.position.z + Random.Range (-2f, 2f)), transform.rotation);

				CustomObjectPoolScript.Instance.Spawn ("RayZombie", new Vector3 (transform.position.x + Random.Range (-2f, 2f), transform.position.y, transform.position.z + Random.Range (-2f, 2f)), transform.rotation);

				spawnCounter = 0f;

				isRandomize = false;

				tempSpawnTime = spawnTime;

				IncreaseSpawn ();
			}
		}
	}

	void IncreaseSpawn ()
	{
		if (spawnTime >= 2) {
			spawnTime -= spawnRate;
		} else {
			return;
		}
	}
}
