using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    #region Dependencies
    private Collider spawnArea;
    [SerializeField] private GameObject[] fruitPrefabs;
    [SerializeField] private GameObject bombPrefab;
    [SerializeField][Range(0f,1f)] private float bombSpawningChance=0.05f; //5%
    [SerializeField] private float minSpawnDelay = 0.25f;
    [SerializeField] private float maxSpawnDelay = 1f;
    [SerializeField] private float minAngle = -15f;
    [SerializeField] private float maxAngle = 15f;
    [SerializeField] private float minForce = 18f;
    [SerializeField] private float maxForce = 22f;
    [SerializeField] private float maxLifetime = 5f;
    private SoundManager soundManager;

    #endregion

    #region Monobehaviour Methods
    private void Awake()
    {
        spawnArea = GetComponent<Collider>();
        soundManager = FindObjectOfType<SoundManager>();
    }
    private void OnEnable()
    {
        StartCoroutine(Spawn());
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
    #endregion

    #region Spawner Coroutines
    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(2f);
        while(enabled)
        {
            GameObject fruitPrefab = fruitPrefabs[Random.Range(0, fruitPrefabs.Length)];
            if (Random.value < bombSpawningChance)
            {
                fruitPrefab = bombPrefab;
            }
            Vector3 positioin = new Vector3();
            positioin.x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
            positioin.y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
            positioin.z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);
            Quaternion rotation = Quaternion.Euler(0f,0f,Random.Range(minAngle,maxAngle));
            GameObject fruit = Instantiate(fruitPrefab, positioin, rotation);
            float force = Random.Range(minForce,maxForce);
            fruit.GetComponent<Rigidbody>().AddForce(fruit.transform.up*force, ForceMode.Impulse);
            soundManager.PlaySFX("Popup");
            Destroy(fruit,maxLifetime);
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }
    #endregion
}
