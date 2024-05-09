using Framework;

namespace Elvenwood
{
    public class GameStartCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            var model = this.GetModel<IUIElementModel>();
            UIFuncKit.Instance.LoadSceneAsync("GameSceneTest",model.LoadPanel.Value,model.BlackTransition.Value, 2f);
        }
    }
}