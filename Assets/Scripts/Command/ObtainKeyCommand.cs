using Framework;

namespace Elvenwood
{
    public class ObtainKeyCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.GetModel<IPlayerModel>().Data.keyFragmentCount.Value++;
        }
    }
}