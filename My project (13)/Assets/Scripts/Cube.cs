using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private int _separationChance = 100;

    public event Action<Cube> CreatingNewCubes;
    public event Action<Cube> NotCreatingNewCubes;

    private Material _material;
    private Rigidbody _rigidbody;
    private int _decreaseNumber = 2;

    private void Awake()
    {
        _material = GetComponent<Renderer>().material;
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnMouseUpAsButton()
    {
        TryDivide();
        Destroy(gameObject);
    }

    public void Init()
    {
        ReduceSeparateChance();
        SetScale();
        SetColor();
    }

    public void ReduceSeparateChance()
    {
        int reduceNumber = 2;

        _separationChance /= reduceNumber;
    }

    private void SetScale()
    {
        transform.localScale /= _decreaseNumber;
    }

    private void SetColor()
    {
        _material.color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
    }

    public void TryDivide()
    {
        int _minSeparationChance = 0;
        int _maxSeparationChance = 101;

        float probability = UnityEngine.Random.Range(_minSeparationChance, _maxSeparationChance);
   
        if (_separationChance >= probability)
        {
            CreatingNewCubes?.Invoke(this);
        }
    }
}
