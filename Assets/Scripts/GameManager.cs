using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject target;
    public GameObject points;
    public int totalBall = 3;
    private int _currentBallIndex = 0;
    public int targetSpeed = 50 ;
    public static GameManager GameManagerInstance;
    public List<GameObject> pointList = new List<GameObject>();
    public List<GameObject> pointUpList = new List<GameObject>();
    public int pointsLeft;
    private bool _isCliked;
    public int levelindex = 1 ;

    public bool isGameActive = true;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public Button levelUpButton;
    private LineRenderer _lr;




    [Header("Ball positioning")] public float firstBallDownOffset;
    public float offsetBetweenSmallBalls;

    private void Awake()
    {
        levelUpButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        GameManagerInstance = this;
        float pos = firstBallDownOffset;
        for (int i = 0; i < totalBall; i++)
        {
            GameObject g = Instantiate(points, transform.position + new Vector3(0, -pos, 0), Quaternion.identity);
            pointList.Add(g);
            pointUpList.Add(g);
            pos += offsetBetweenSmallBalls;
        }

        target.transform.Rotate(0, 50 * Time.deltaTime, 0);
        pointsLeft = totalBall;
        _lr = GetComponentInChildren<LineRenderer>();




    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isGameActive)
        {
            target.transform.Rotate(0, 0, targetSpeed * Time.deltaTime);
        }

        if( isGameActive && pointsLeft == 0 && _isCliked)
        {
            levelUpButton.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
          
        }
    }

    public void OnPointerDown()
    {
        _isCliked = true;

        if (_currentBallIndex < totalBall)
        {

            Ball b = pointList[_currentBallIndex].GetComponent<Ball>();

            b.MoveBall();
            _currentBallIndex++;

            // Move the rest 
            for (int i = _currentBallIndex; i < totalBall; i++)
            {
                Ball otherBall = pointList[i].GetComponent<Ball>();

                StartCoroutine(otherBall.MoveUp(offsetBetweenSmallBalls));
            }
        }


    }

   


    public void GameOver()
    {
        isGameActive = false;
        Camera.main.backgroundColor = Color.red;
        Debug.Log("Game over");
        restartButton.gameObject.SetActive(true);

    }
    
       public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LevelUp()
    {

        SceneManager.LoadScene(levelindex);
        levelindex++;
        

    }

    
}
