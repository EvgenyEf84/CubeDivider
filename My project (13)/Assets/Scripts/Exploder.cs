using System.Collections.Generic;
using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Exploder : MonoBehaviour
{
    [SerializeField] private float _explodeRadius;
    [SerializeField] private float _explodeForce;
    [SerializeField] private ParticleSystem _effect;
    [SerializeField] private Cube _cube;

    private void OnEnable()
    {
        _cube.NotCreateNewCubes += Explode;
    }

    private void OnDisable()
    {
        _cube.NotCreateNewCubes -= Explode;
    }

    private void Explode(Cube cube)
    {
        foreach (Rigidbody explorableObject in GetExplorableObjects())
        {
            explorableObject.AddExplosionForce(_explodeForce, transform.position, _explodeRadius);
        }

        Instantiate(_effect, transform.position, transform.rotation);
    }

    private List<Rigidbody> GetExplorableObjects()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explodeRadius);

        List<Rigidbody> cubes = new();

        foreach (Collider hit in hits)
        {
            if (hit.attachedRigidbody != null)
                cubes.Add(hit.attachedRigidbody);
        }

        return cubes;
    }
}
