using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CamParallaxManager : MonoBehaviour
{

    #region Fields

    [SerializeField] float _length;
    [SerializeField] Vector2 _startPosition;

    [SerializeField] Transform _cam;

    [SerializeField] float _parallaxEffect;
    [SerializeField] bool _isTilemap;

    #endregion

    #region MonoBehaviour Methods

    private void Start()
    {

        //_startPosition = transform.position;
        //_length = _isTilemap ? GetComponent<TilemapRenderer>().bounds.size.x : GetComponent<SpriteRenderer>().bounds.size.x;
        _cam = Camera.main.transform;

    }

    private void Update()
    {

        float distanceX = _cam.transform.position.x * _parallaxEffect;
        float distanceY = _cam.transform.position.y * _parallaxEffect;

        transform.position = new Vector3(_startPosition.x + distanceX, _startPosition.y + distanceY, transform.position.z);

    }

    #endregion

}
