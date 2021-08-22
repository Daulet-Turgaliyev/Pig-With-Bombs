using System;
using UnityEngine;

public class HeartBody : MonoBehaviour
{
    [SerializeField] 
    private GameObject bodyObject;

    [SerializeField]
    private float healthPoint;
    
    public Action<float> OnStart = delegate {  };
    public Action<float> OnDamaged = delegate {  };

    [SerializeField]
    private bool isPlayer;
    
    private void Start()
    {
        OnStart(healthPoint);
    }

    public void Damage(float dmg)
    {
        healthPoint -= dmg;

        if (healthPoint < 0)
        {
            if (isPlayer)
                UIManager.Instance.losePanel.SetActive(true);
            else
                Destroy(bodyObject);
            return;
        }

        OnDamaged?.Invoke(healthPoint);
    }

    private void OnDisable()
    {
        OnStart = null;
        OnDamaged = null;
    }
}
