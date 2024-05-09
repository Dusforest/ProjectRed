using Framework;

namespace Elvenwood.Test
{
    public class TestSaveCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            var model = this.GetModel<IPlayerModel>();
            
            model.Save();
        }
    }
}