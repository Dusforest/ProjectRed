using Framework;

namespace Elvenwood
{
    public class HurtPlayerCommand : AbstractCommand
    {
        private readonly int mDamage;

        public HurtPlayerCommand(int damage)
        {
            mDamage = damage;
        }

        protected override void OnExecute()
        {
            this.GetModel<IPlayerModel>().Data.curHp.Value -= mDamage;
        }
    }
}