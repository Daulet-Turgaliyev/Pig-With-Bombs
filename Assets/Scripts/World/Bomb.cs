using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] 
    private Animator animator;

    [SerializeField]
    private int timer;
    
    [SerializeField]
    private int damage;
    
    [SerializeField]
    private int radius;
    
    private int bombBoomId => Animator.StringToHash("bombBoom");

    private void Start()
    {
        StartCoroutine(Waiting());
    }

    private IEnumerator Waiting()
    {
        yield return new WaitForSeconds(timer);
        DestroyBomb();
    }
    
    private void DestroyBomb()
    {
        animator.Play(bombBoomId);
        Explosion2D();
        Destroy(gameObject, .30f);
    }

    private void Explosion2D()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach (var hit in colliders)
        {
            if (hit.gameObject.TryGetComponent(out HeartBody body) == false) continue;
            float dist = Vector2.Distance(gameObject.transform.position, hit.gameObject.transform.position);
            int dmg = Mathf.RoundToInt(damage / dist);
            body.Damage(dmg);
        }
    }
}
