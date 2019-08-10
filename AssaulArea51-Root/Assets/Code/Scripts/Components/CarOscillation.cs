using UnityEngine;

public class CarOscillation : MonoBehaviour
{
    [Header("Tunning Params")]
    [SerializeField] private Transform _frontGoal;
    [SerializeField] private Transform _backGoal;
    [SerializeField] private Transform _leaveScreenGoal;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _ExitSpeed;

    [SerializeField] private Vector2 _minRandomValues;
    [SerializeField] private Vector2 _maxRandomValues;


    [Header("Sensing")]
    [SerializeField] private float _farAwayRadius;
    [SerializeField] private float _closeEnoughRadius;

    [Header("Debug")]
    [SerializeField] private bool _drawGizmos = false;

    private Transform _transform;
    private Transform _currentGoal;
    private float _currentSpeed;
    private bool _exitScreen = false;
    private bool _onStop = false;
    private float _startY;

    void Start()
    {
        _transform = this.transform;
        SwitchGoals();
        _startY = transform.position.y;
    }

    void Update()
    {
        if (_onStop)
            return;

        if (transform.position.y != _startY)
        {
            float newY = transform.position.y;
            newY = Mathf.MoveTowards(newY, _startY, 2.5f * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, newY, 0);
            return;
        }

        if (!_exitScreen)
            Move();
        else
            MoveOutScreen();

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Alpha0))
            HoldOnBackGoal();

        if (Input.GetKeyDown(KeyCode.Alpha1))
            LeaveScreen();

        if (Input.GetKeyDown(KeyCode.Alpha3))
            SwitchGoals();
#endif
    }

    public void Stop ()
    {
        _onStop = true;
    }

    public void Restart ()
    {
        _onStop = false;
    }

    public void LeaveScreen()
    {
        _currentGoal = _leaveScreenGoal;
        _exitScreen = true;
    }

    public void HoldOnBackGoal()
    {
        CancelInvoke("SwitchGoals");
        _currentGoal = _backGoal;
    }

    public void SwitchGoals()
    {
        if (_currentGoal == _frontGoal)
            _currentGoal = _backGoal;
        else
            _currentGoal = _frontGoal;

        float l_time = Random.Range(_maxRandomValues.x, _maxRandomValues.y) - Random.Range(_minRandomValues.x, _minRandomValues.y);
        Invoke("SwitchGoals", l_time);
    }

    private void Move()
    {
        float l_distanceToGoal = Vector3.Distance(_transform.position, _currentGoal.position);

        if (l_distanceToGoal < _closeEnoughRadius)
            return;
        else if (l_distanceToGoal >= _closeEnoughRadius && l_distanceToGoal <= _farAwayRadius)
        {
            Vector3 l_direction = (_currentGoal.position - _transform.position).normalized;
            l_direction.z = 0f;

            float l_brakeRatio = (l_distanceToGoal - _closeEnoughRadius)/(_farAwayRadius-_closeEnoughRadius);
            _currentSpeed = _maxSpeed * l_brakeRatio;
            _transform.position += l_direction * _currentSpeed * Time.deltaTime;

        }
        else
        {
            Vector3 l_direction = (_currentGoal.position - _transform.position).normalized;
            l_direction.z = 0f;
            _currentSpeed = _maxSpeed;
            _transform.position += l_direction * _currentSpeed * Time.deltaTime;
        }
    }

    private void MoveOutScreen()
    {
        Vector3 l_direction = (_currentGoal.position - _transform.position).normalized;
        l_direction.z = 0f;
        _currentSpeed = _ExitSpeed;
        _transform.position += l_direction * _currentSpeed * Time.deltaTime;
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        if(_drawGizmos)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_frontGoal.position, _closeEnoughRadius);
            Gizmos.DrawWireSphere(_backGoal.position, _closeEnoughRadius);
            Gizmos.DrawWireSphere(_leaveScreenGoal.position, _closeEnoughRadius);


            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(_frontGoal.position, _farAwayRadius);
            Gizmos.DrawWireSphere(_backGoal.position, _farAwayRadius);
            Gizmos.DrawWireSphere(_leaveScreenGoal.position, _farAwayRadius);


        }
    }
#endif
}
