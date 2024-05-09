using Framework;

namespace Elvenwood
{
    public class PlayerCurrentHpQuery : AbstractQuery<int>
    {
        protected override int OnDo()
        {
            return this.GetModel<IPlayerModel>().Data.curHp.Value;
        }
    }
}