using Framework;

namespace Elvenwood
{
    public class HealingPlayerCommand : AbstractCommand
    {
        private readonly int mHealing;

        public HealingPlayerCommand(int damage)
        {
            mHealing = damage;
        }

        protected override void OnExecute()
        {
            this.GetModel<IPlayerModel>().Data.curHp.Value += mHealing;
        }
    }
}