using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody _rb;
    int _speed = 8;
    public bool isMoving = false;
    public bool isMovingUp = false;

    public LineRenderer lr;

    private bool _isTriggerable = false;
    private bool _once = false;

    private Vector3 _endpos;
    private Vector3 _startpos;
    private float _duration = 3f;
    private float _elapstTime;

    public Vector3 Endpos => _endpos;

    private void Awake() => lr = GetComponentInChildren<LineRenderer>();

    public void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _endpos = new Vector3(0, GameManager.GameManagerInstance.target.transform.position.y, 0);
        _startpos = new Vector3(0, transform.position.y, 0);


    }

    // Update is called once per frame
    private void Update()
    {
        if (isMoving)
        {
            transform.Translate(Vector3.up * _speed * Time.deltaTime);
           
        }

        if (_once)
        {
            DrawLine();
        }

        if (isMovingUp)
        {
            _elapstTime += Time.deltaTime;
            float perComp = _elapstTime / _duration;
            transform.position = Vector3.Lerp(_startpos, _endpos, perComp);

        }
    }

    public void MoveBall()
    {
        _isTriggerable = true;
        isMoving = true;
    }

    /// <summary>
    // Moves the ball up by the specified offset value
    // 0.5 saniye sonra alttaki topları belli bir offset değerinde üste getiriyorum.
    /// </summary>
    /// <param name="yoffset">Offset  parameter vertical axis</param>

    
    public  IEnumerator MoveUp(float yoffset)
    
    {
        yield return new WaitForSeconds(0.5f);
        transform.position += new Vector3(0, yoffset, 0);
    }
    
    public IEnumerable MoveUpBallSlowly()
    {
       
        yield return new WaitForSeconds(0.5f);
        transform.position += Vector3.Lerp(_startpos, _endpos, 0.5f);

    }


    /// <summary>
    /// drawing the lines between small balls and the target ball
    /// </summary>

    public void DrawLine()
    {
        lr.enabled = true;

        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, GameManager.GameManagerInstance.target.transform.position);
        isMoving = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        if(!_isTriggerable)
            return;
        if (other.gameObject.CompareTag("LineTriger") && !_once)
        {
            isMoving = false;
            _once = true;
          
            transform.parent = GameManager.GameManagerInstance.target.transform;
            
            if(GameManager.GameManagerInstance.isGameActive == true)
            {
                Numartor.PinCount++;
                GameManager.GameManagerInstance.pointsLeft --;
            }


        }
      
        if (other.gameObject.CompareTag("points"))
        {

            GameManager.GameManagerInstance.GameOver(); 
           
        }

    }

     


}
