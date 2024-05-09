using Framework;

namespace Elvenwood
{
    public class CurrentKeyCount : AbstractQuery<int>
    {
        protected override int OnDo()
        {
            return this.GetModel<IPlayerModel>().Data.keyFragmentCount.Value;
        }
    }
}