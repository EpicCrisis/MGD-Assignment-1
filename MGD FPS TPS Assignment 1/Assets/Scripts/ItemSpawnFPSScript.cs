using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnFPSScript : MonoBehaviour
{

	//public GameObject[] items;

	public float spawnTime = 4f;
	float tempSpawnTime = 0f;
	float spawnCounter = 0.0f;

	public float minTime = 2f;
	public float maxTime = 4f;

	int itemToRandomize = 0;

	public Vector3 positionA;
	public Vector3 positionB;

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

			itemToRandomize = Random.Range (0, 10);

			isRandomize = true;
		}

		if (isRandomize) {

			spawnCounter += Time.deltaTime;

			if (spawnCounter >= tempSpawnTime) {

				float x = Random.Range (positionA.x, positionB.x);
				float y = Random.Range (positionA.y, positionB.y);
				float z = Random.Range (positionA.z, positionB.z);

				if (itemToRandomize >= 0 && itemToRandomize <= 2) {

					CustomObjectPoolScript.Instance.Spawn ("HealthBox", new Vector3 (x, y, z), Quaternion.identity);
					//Instantiate (items [0], new Vector3 (x, y, z), Quaternion.identity);

				} else if (itemToRandomize >= 3 && itemToRandomize <= 9) {

					CustomObjectPoolScript.Instance.Spawn ("PistolAmmoBox", new Vector3 (x, y, z), Quaternion.identity);
					//Instantiate (items [1], new Vector3 (x, y, z), Quaternion.identity);

				}

				spawnCounter = 0f;

				isRandomize = false;

				tempSpawnTime = spawnTime;
			}
		}
	}
}
