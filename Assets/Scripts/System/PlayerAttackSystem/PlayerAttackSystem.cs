using Framework;
using UnityEngine;

namespace Elvenwood
{
    public interface IPlayerAttackSystem : ISystem
    {
        void Attack();
    }
    
    public class PlayerAttackSystem :AbstractSystem, IPlayerAttackSystem
    {
        protected override void OnInit()
        {
            
        }

        public void Attack()
        {
            Debug.Log("ATTACK!!!");
        }
    }
}