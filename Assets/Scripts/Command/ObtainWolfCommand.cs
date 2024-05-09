using Framework;

namespace Elvenwood
{
    public class ObtainWolfCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.GetModel<IPlayerModel>().Data.hasWolfMode.Value = true;
        }
    }
}