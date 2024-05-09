using Framework;

namespace Elvenwood
{
    public class MaxHpUpCommand: AbstractCommand
    {
        protected override void OnExecute()
        {
            this.GetModel<IPlayerModel>().Data.maxHp.Value++;
            this.GetModel<IPlayerModel>().Data.curHp.Value++;
        }
    }
}