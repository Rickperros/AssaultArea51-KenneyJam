using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    public float _backgroundSpeedBack = 3f;

    public List<Transform> _backgroundTilesBack;

    private Vector3 _tilesOffsetBack;
    private float _displacementBack = 0f;
    private int _headIndexBack = 0;

    void Start()
    {
        _tilesOffsetBack = _backgroundTilesBack[0].position - _backgroundTilesBack[1].position;
    }

    void Update()
    {
        _displacementBack += _backgroundSpeedBack * Time.deltaTime;

        if(Mathf.Abs(_displacementBack - _tilesOffsetBack.x) <= 0.1f)
        {
            _backgroundTilesBack[_headIndexBack].position = _backgroundTilesBack[GetPreviousIndex(_headIndexBack, _backgroundTilesBack.Count)].position - _tilesOffsetBack;
            _headIndexBack = (_headIndexBack + 1) % _backgroundTilesBack.Count;
            _displacementBack = 0f;
        }

        foreach (Transform l_tile in _backgroundTilesBack)
            l_tile.position += Vector3.right * _backgroundSpeedBack * Time.deltaTime;
    }

    int GetPreviousIndex(int currentIndex, int module)
    {
        if(currentIndex == 0)
            return module-1;
        return (_headIndexBack - 1) % _backgroundTilesBack.Count;
    }
}
