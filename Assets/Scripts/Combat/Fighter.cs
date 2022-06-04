//Should be on the player
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Core;
namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float WeaponRange = 2f;
        [SerializeField] [Range(0f, 5f)] float timeBetweenAttacks; 
        
        
        Health target;    // Changed from transform to health to make sure our target has the health component
        int unarmedDamage = 50;
        float TimeSinceLastAttack = 0f;

        private void Start()
        {
        }
        private void Update()
        {

            TimeSinceLastAttack += Time.deltaTime;
            if (target == null) return;

            if (target.IsDead()) return;

            if (!GetIsInAttackRange())
                GetComponent<Mover>().moveToHitPoint(target.transform.position);       // Move towards the target..
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }

        bool GetIsInAttackRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < WeaponRange;
        }

        public void Attack(CombatTarget combat_target)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combat_target.GetComponent<Health>();
        }

        public void Cancel()
        {
            GetComponent<Animator>().ResetTrigger("attack");    
            GetComponent<Animator>().SetTrigger("stopAttack");
            target = null;
        }

        void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if(TimeSinceLastAttack > timeBetweenAttacks)
            {
                GetComponent<Animator>().ResetTrigger("stopAttack");
                GetComponent<Animator>().SetTrigger("attack");
                TimeSinceLastAttack = 0f;
            }
        }

        void Hit()
        {
            if (target == null) return;
            target.takeDamage(unarmedDamage);
        }

        public bool canAttack(CombatTarget combatTarget)
        {
            if (combatTarget == null) return false;
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }

    }

}
