using UnityEngine;

public class Spawner : MonoBehaviour
{
    public MapGenerator mapGenerator;

    public Player localPlayer;

    [SerializeField]
    private GameObject[] enemyArray;
    private GameObject[] itemsArray;
    private GameObject[] AltarArray;

    [SerializeField]
    private int enemyCount;
    int itemCount;
    int AltarCount;
    [SerializeField]
    private int maxEnemyCount = 5;
    [SerializeField]
    int maxItemCount = 5;
    int terrainSize;

    float nextSpawnTime;
    float spawnInterval = 5f;

    bool shouldSpawnItems;

    Transform spawnedGameobjects;
    private void Awake()
    { 
        itemCount = 0;
        enemyCount = 0;
        enemyArray = Resources.LoadAll<GameObject>("EnemyPrefabs");
        itemsArray = Resources.LoadAll<GameObject>("ItemPrefabs");
        AltarArray = Resources.LoadAll<GameObject>("AltarPrefabs");
        GameManager.Instance.OnLocalPlayerJoined += HandleOnLocalPlayerJoined;
        mapGenerator = GameObject.Find("Map Generator").GetComponent<MapGenerator>();
        terrainSize = mapGenerator.terrainData.terrianSize;
        spawnedGameobjects = new GameObject("SpawnedGameobjects").transform;
    }

    private void Start()
    {
        spawnItems();
    }
    private void Update()
    {
        spawnEnemy();
        //print("enemys:" + (Physics.OverlapSphere(localPlayer.transform.position, 20f, 1 << 9)).Length);
    }
    public void Respawn(GameObject go, float inSeconds)
    {
        go.SetActive(false);
        GameManager.Instance.Timer.Add(() =>{
            go.SetActive(true);
        }, inSeconds);
        
    }

    public void spawnEnemy()
    {
        if(shouldSpawnEnemy())
        {
            if (Time.time < nextSpawnTime)
            {
                return;
            }
            nextSpawnTime = Time.time + spawnInterval;
            GameManager.Instance.Timer.Add(() =>
            {
                int spawnPosX = System.Convert.ToInt32(localPlayer.transform.position.x + Random.Range(0, 5));
                int spawnPosZ = System.Convert.ToInt32(localPlayer.transform.position.z + Random.Range(0, 5));
                int spawnPosY = System.Convert.ToInt32(mapGenerator.getHeight(mapGenerator.mapData.heightMap,spawnPosX, spawnPosZ));
                if (spawnPosY > 0 && spawnPosY < 5)
                {
                   Instantiate(enemyArray[Random.Range(0, enemyArray.Length)],
                        new Vector3(spawnPosX, spawnPosY, spawnPosZ), Quaternion.identity).transform.parent = spawnedGameobjects;
                    enemyCount++;
                }
            }, 0f);           
            
            
        }
    }

    public void spawnItems()
    {
        
        while(itemCount<5)
        {
            int spawnPosX = System.Convert.ToInt32(Random.Range((terrainSize - 1) / -2f, (terrainSize - 1) / 2f));
            int spawnPosZ = System.Convert.ToInt32(Random.Range((terrainSize - 1) / 2f, (terrainSize - 1) / -2f));
            int spawnPosY = System.Convert.ToInt32(mapGenerator.getHeight(mapGenerator.mapData.heightMap, spawnPosX, spawnPosZ));
            if(spawnPosY<0.1 || spawnPosY > 5)
            {
                continue;
            }
            Instantiate(itemsArray[Random.Range(0, itemsArray.Length - 1)],
                new Vector3(spawnPosX, spawnPosY+1, spawnPosZ), Quaternion.identity).transform.parent = spawnedGameobjects;
            itemCount++;
        }
    }
    public void spawnAltar()
    {

        while (AltarCount < 1)
        {
            int spawnPosX = System.Convert.ToInt32(Random.Range((terrainSize - 1) / -2f, (terrainSize - 1) / 2f));
            int spawnPosZ = System.Convert.ToInt32(Random.Range((terrainSize - 1) / 2f, (terrainSize - 1) / -2f));
            int spawnPosY = System.Convert.ToInt32(mapGenerator.getHeight(mapGenerator.mapData.heightMap, spawnPosX, spawnPosZ));
            if (spawnPosY < 0.1 || spawnPosY > 5)
            {
                continue;
            }
            Instantiate(itemsArray[Random.Range(0, AltarArray.Length - 1)],
                new Vector3(spawnPosX, spawnPosY + 1, spawnPosZ), Quaternion.identity).transform.parent = spawnedGameobjects;
            itemCount++;
        }
    }
    bool shouldSpawnEnemy()
    {
        if (enemyCount < maxEnemyCount) 
        {
            if (Physics.OverlapSphere(localPlayer.transform.position, 5f, 1 << 9).Length < 3)
            {
                return true;
            }
            return false;
        }
        return false;
    }

    void HandleOnLocalPlayerJoined(Player player)
    {
        localPlayer = player;
    }
    
    public void InitNewGame()
    {
        Destroy(spawnedGameobjects.gameObject);
        enemyCount = 0;
        itemCount = 0;
        AltarCount = 0;
        print("Destroy all");
        print("DrawNewMap");
    }
}
