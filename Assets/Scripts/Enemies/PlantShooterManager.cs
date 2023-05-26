using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantShooterManager : MonoBehaviour
{

    #region Fields

    [SerializeField] GameObject _plantBulletPrefab;
    [SerializeField] Transform _bulletSpawnerPosition;
    [SerializeField] float _angle;
    [SerializeField] float _bulletForce;
    [Space]
    [SerializeField] float _timeToShoot;
    float _currentTimeToShoot;

    #endregion

    #region Cached Components

    Transform _transform;

    #endregion

    #region MonoBehaviour Methods

    private void Awake()
    {
     
        _transform = GetComponent<Transform>();
        
    }

    private void Start()
    {

        _transform.localRotation = Quaternion.Euler(0, 0, -_angle);

    }

    private void Update()
    {

        _currentTimeToShoot += Time.deltaTime;
        if (_currentTimeToShoot >= _timeToShoot)
            Shoot();

    }

    #endregion

    #region Private Methods

    void Shoot()
    {

        GameObject plantBullet = Instantiate(_plantBulletPrefab, _bulletSpawnerPosition.position, _bulletSpawnerPosition.rotation);
        plantBullet.GetComponent<PlantShooterBullet>().Shoot(_angle, _bulletForce);


        _currentTimeToShoot = 0;

    }

    #endregion


}
