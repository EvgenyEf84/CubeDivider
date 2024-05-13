using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PrefabCreator : MonoBehaviour
{
    [SerializeField] private Cube _cube = new Cube();
    [SerializeField] private ParticleSystem _effect;
    [SerializeField] private float _explodeRadius;
    [SerializeField] private float _explodeForce;

    private void OnEnable()
    {
        _cube.CreateNewCubes += CreateCubes;
    }

    private void OnDisable()
    {
        _cube.CreateNewCubes -= CreateCubes;
    }

    private void CreateCubes(Cube cube)
    {
        int minObjectsQuantity = 2;
        int maxObjectsQuantity = 7;

        int newObjectsQuantity = Random.Range(minObjectsQuantity, maxObjectsQuantity);

        for (int i = 0; i <= newObjectsQuantity; i++)
        {
            Cube newCube = Instantiate(cube, transform.position, Quaternion.identity);
            newCube.Init();
            newCube.GetComponent<Rigidbody>().AddExplosionForce(_explodeForce, transform.position, _explodeRadius);
        }

        Instantiate(_effect, transform.position, transform.rotation);
    }
}
