using Framework;

namespace Elvenwood
{
    public class ObtainMagicArrowCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.GetModel<IPlayerModel>().Data.hasMagicArrow.Value = true;
        }
    }
}