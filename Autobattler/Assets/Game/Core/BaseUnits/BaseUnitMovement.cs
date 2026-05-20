using System;
using Cysharp.Threading.Tasks;
using Game.Data.UnitConfigs;
using Game.PatternCombat.Grid;
using Grid;
using UnityEngine;

namespace Game.Core.BaseUnits
{
    public class BaseUnitMovement : MonoBehaviour
    {
        [SerializeField] protected Rigidbody2D rb2D;
        [SerializeField] protected SpriteRenderer spriteRenderer;
        [SerializeField] protected Animator animator;
        [SerializeField] protected float stopDistance = 0.1f;
        
        protected bool IsMoving;
        protected Vector2 CurrentTarget;

        protected UnitConfig UnitConfig;

        protected virtual void FixedUpdate()
        {
            if (!IsMoving || CurrentTarget == Vector2.zero)
                return;

            Debug.Log($"rb pos = {rb2D.position} and current target = {CurrentTarget} \n Vector2.Distance({Vector2.Distance(rb2D.position, CurrentTarget)})");
            
            if (Vector2.Distance(rb2D.position, CurrentTarget) <= stopDistance)
            {
                Stop();
                return;
            }
            
            Vector2 direction = (CurrentTarget - rb2D.position).normalized;
            
            SetSpriteSide(direction);
            
            rb2D.linearVelocity = direction * UnitConfig.Movement.speed;
        }

        public virtual async UniTask MoveAsync(GridData targetGridData, UnitConfig unitConfig)
        {
            CurrentTarget = targetGridData.worldPosition;
            UnitConfig = unitConfig;
            
            animator.SetBool(UnitConfig.Animation.walk, true);
            
            IsMoving = true;
            
            await UniTask.WaitUntil(() => !IsMoving);
        }

        protected void SetSpriteSide(Vector2 currentDirection)
        {
            spriteRenderer.flipX = currentDirection.x > 0;
        }
        
        protected void Stop()
        {
            IsMoving = false;
            rb2D.linearVelocity = Vector2.zero;
            CurrentTarget = Vector2.zero;

            animator.SetBool(UnitConfig.Animation.walk, false);  
        }
    }
}