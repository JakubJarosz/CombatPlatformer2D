using UnityEngine;

public class Stagger : MonoBehaviour
{
    private EnemyAttack enemyAttack;

    public int staggerCount;
    private int originalStaggerCount;

    private void Awake() {
        enemyAttack = GetComponent<EnemyAttack>();
        originalStaggerCount = staggerCount;
    }

    public void DealStaggerDamage(int staggerDmg) {
        staggerCount -= staggerDmg;
        if (staggerCount <= 0) {
            enemyAttack.Stagger();
            staggerCount = originalStaggerCount;
        }
    }
    
}
