using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PrefabCreator : MonoBehaviour
{
    [SerializeField] private Cube _cube = new Cube();
    [SerializeField] private float _explodeForce;

    private void OnEnable()
    {
        _cube.CreatingNewCubes += CreateCubes;
    }

    private void OnDisable()
    {
        _cube.CreatingNewCubes -= CreateCubes;
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

            foreach (Rigidbody explorableObject in GetExplorableObjects())
            {
                explorableObject.AddExplosionForce(_explodeForce, transform.position, transform.localScale.x);
            }
        }
    }

    private List<Rigidbody> GetExplorableObjects()
    {
        Collider[] hits = Physics.OverlapBox(transform.position, transform.localScale);

        List<Rigidbody> cubes = new();

        foreach (Collider hit in hits)
        {
            if (hit.attachedRigidbody != null)
                cubes.Add(hit.attachedRigidbody);
        }

        return cubes;
    }
}
