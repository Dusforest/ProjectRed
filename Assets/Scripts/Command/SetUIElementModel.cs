using Framework;
using UnityEngine;

namespace Elvenwood
{
    public class SetUIElementModel : AbstractCommand
    {
        private GameObject mLoadPanel;
        private GameObject mBlackTransition;

        public SetUIElementModel(GameObject loadPanel, GameObject blackTransition)
        {
            mLoadPanel = loadPanel;
            mBlackTransition = blackTransition;
        }

        protected override void OnExecute()
        {
            var model = this.GetModel<IUIElementModel>();
            model.BlackTransition.Value = mLoadPanel;
            model.LoadPanel.Value = mBlackTransition;
        }
    }
}