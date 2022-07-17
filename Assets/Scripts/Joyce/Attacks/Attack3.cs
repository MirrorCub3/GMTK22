using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack3 : MonoBehaviour
{
    // two tracking dot with a slight fire delay
    [SerializeField] private float attackDelay = .5f; // time before the dot is launched
    public float attackDuration = 2f; // total length of the attack phase, overrides the base class
    public List<DotScript> dots;

    private float waitTime = 1f;
    private bool launched = false;
    private bool newStage = false;

    private Transform player;
    private BossScript boss;

    void Start()
    {
        StartCoroutine(StartAttack());
        StartCoroutine(CheckDots());
        launched = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        boss = FindObjectOfType<BossScript>().GetComponent<BossScript>();
        newStage = boss.stage2;
    }
    private void Update()
    {
        if(launched == false) //rotate towards player
        {
            transform.up = player.position - transform.position;
        }
        if (boss.dead || (boss.stage2 != newStage))
        {
            Destroy(this.gameObject);
        }
    }

    private IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(attackDelay);
        foreach (DotScript dot in dots)
        {
            if (dot != null)
            {
                dot.Launch();
                launched = true;
            }
        }
        transform.parent = null;
        yield return new WaitForSeconds(attackDuration);
        boss.EndAttack();

    }

    public IEnumerator CheckDots()
    {
        while (true)
        {
            print("running check");
            yield return new WaitForSeconds(waitTime);
            bool allNull = true;
            foreach (DotScript dot in dots)
            {
                if (dot)
                {
                    allNull = false;
                }
            }

            if (allNull)
            {
                boss.EndAttack();
                Destroy(this.gameObject);
            }
        }
    }
}
