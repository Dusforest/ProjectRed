using Framework;

namespace Elvenwood
{
    public class KeyIsObtainQuery : AbstractQuery<bool>
    {
        protected override bool OnDo()
        {
            return this.GetModel<IPlayerModel>().Data.keyFragmentCount.Value >= 1;
        }
    }
}