using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public static GameController Instance;

    [SerializeField]
    private GameObject[] GroundsPrefabs;
    [SerializeField]
    private GameObject[] GroundsOnStage;
    [SerializeField]
    public float Health;
    [SerializeField]
    private Image HealthBarRed;
    [SerializeField]
    private Image HealthBarGreen;
    [SerializeField]
    public float Score;
    
    public Text ScoreText;


    


    private float GroundSize;

    private GameObject Bird;

    private int NumberOfGrounds = 7;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
            Instance = this;
        Health = 100;

        
       
    }



    // Start is called before the first frame update
    void Start()
    {
        
        Bird = GameObject.Find("Bird");
        GroundsOnStage = new GameObject[NumberOfGrounds];
        

        for (int i = 0; i < NumberOfGrounds; i++)
        {
            int n = Random.Range(0, GroundsPrefabs.Length);
            GroundsOnStage[i] = Instantiate(GroundsPrefabs[n]);
        }

        GroundSize = GroundsOnStage[0].GetComponentInChildren<Transform>().Find("Road").localScale.z;

        float pos = Bird.transform.position.z + GroundSize /2 - 1.5f;
        foreach (var  ground in GroundsOnStage)
        {
            ground.transform.position = new Vector3(0, 0.2f, pos);
            pos += GroundSize;
        }

        Score = 0;

        MusicManager.instance.StopMenuMusic();
        



    }

    // Update is called once per frame
    void Update()
    {
        for(int i = GroundsOnStage.Length - 1; i >= 0; i--)
        {
            GameObject ground = GroundsOnStage[i];

            if(ground.transform.position.z + GroundSize /2 < Bird.transform.position.z - 6f)
            {
                float z = ground.transform.position.z;
                Destroy(ground);
                int n = Random.Range(0, GroundsPrefabs.Length);
                ground = Instantiate(GroundsPrefabs[n]);
                ground.transform.position = new Vector3(0, 0.2f, z + GroundSize * NumberOfGrounds);
                GroundsOnStage[i] = ground;
            }
        }

        HealthBarGreen.rectTransform.sizeDelta = new Vector2(HealthBarRed.rectTransform.sizeDelta.x * Health / 100 , HealthBarRed.rectTransform.sizeDelta.y);

        Score += 10 * Time.deltaTime;

        if(ScoreText != null)
        {
            ScoreText.text = "Score : " + Mathf.Round(Score).ToString();
        }

       


    }
    

    private void OnDestroy()
    {
        Instance = null;
       
    }

    
}
