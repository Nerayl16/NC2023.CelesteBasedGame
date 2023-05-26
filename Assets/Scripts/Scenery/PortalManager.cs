using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{

    #region Fields

    [SerializeField] Transform _secondPortal;
    [SerializeField] Vector3 _teleportDistance;
    [SerializeField] int _direction;

    #endregion

    #region Cached Components

    Transform _transform;

    #endregion

    #region MonoBehaviour Methods

    private void Awake()
    {

        _transform = GetComponent<Transform>();

    }

    #endregion

    #region Collider Methods

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.gameObject.CompareTag("Player"))
            collision.transform.position = _secondPortal.position + _teleportDistance;

    }

    #endregion

}
