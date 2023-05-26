using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantShooterBullet : MonoBehaviour
{

    #region Fields

    #endregion

    #region Cached Components

    Rigidbody2D _rb;
    Transform _transform;

    #endregion

    #region MonoBehaviour Methods

    void Awake()
    {
        
        _rb = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();

    }

    private void Start()
    {

        

    }

    void Update()
    {

        if (_rb.velocity.y < -12)
            _rb.velocity = new Vector2(_rb.velocity.x, -12);
        
    }

    #endregion

    #region Public Methods

    public void Shoot(float angle, float bulletForce)
    {

        _rb.rotation = (angle);
        _rb.AddForce(transform.right * bulletForce, ForceMode2D.Impulse);

    }

    #endregion

}
