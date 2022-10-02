using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    [SerializeField]
    Transform _positionRoot;
    [SerializeField]
    Transform _animationRoot;
    [SerializeField]
    AnimationCurve _heightCurve;
    [SerializeField]
    float _animationTime;
    [SerializeField]
    float _animationHeight;

    float _animTime = 0;
    Vector3 _lastposition;

    private void Start()
    {
        _lastposition = _positionRoot.position;
    }

    private void Update()
    {
        var currentPos = _positionRoot.position;
        if (_lastposition == currentPos) return;

        _lastposition = currentPos;
        _animTime += Time.deltaTime / _animationTime;
        _animTime %= 1;
        var h = _animationHeight * _heightCurve.Evaluate(_animTime);
        _animationRoot.localPosition = new Vector3(0, h, 0);
    }
}
