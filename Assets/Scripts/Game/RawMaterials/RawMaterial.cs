using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;
using UnityEngine.UI;

public abstract class RawMaterial : NetworkBehaviour, IPoolable<RawMaterial>, IDamageable
{
    [SerializeField] private List<GameObject> resources = new List<GameObject>();
    private ObjectPool<RawMaterial> _rawMaterialPool;
    
    public Slider healthBarSlider;
    
    [SerializeField] private int _health;
    public int Health { get; set; }
    
    private Selectables _selectables;
    
    private void Awake()
    {
        if (healthBarSlider == null) healthBarSlider = GetComponentInChildren<Slider>();
        _selectables = GetComponent<Selectables>();
        healthBarSlider.gameObject.SetActive(false);
        Health = _health;
        SetHealthBar(Health);
    }
    
    public void Initialize(ObjectPool<RawMaterial> objPool)
    {
        _rawMaterialPool = objPool;
    }

    public void ReturnToPool()
    {
        _rawMaterialPool.Release(this);
        TopPanelController.Instance.SetStock(_selectables.woodCount, _selectables.stoneCount, _selectables.foodCount, _selectables.herbCount);
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
        healthBarSlider.value = Health;
        if (Health <= 0)
        {
            ReturnToPool();
        }
    }

    private void SetHealthBar(float health)
    {
        healthBarSlider.maxValue = health;
        healthBarSlider.value = health;
    }
}
