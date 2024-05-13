using System;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public event Action<Cube> CreateNewCubes;
    public event Action<Cube> NotCreateNewCubes;

    private Material _material;
    private int _decreaseNumber = 2;
    private int _separationChance=100;

    private void Awake()
    {
        _material = GetComponent<Renderer>().material;
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
            Debug.Log(_separationChance);
            CreateNewCubes?.Invoke(this);
        }
        else
        {
            Debug.Log(_separationChance);
            NotCreateNewCubes?.Invoke(this);
        }
    }
}
