using Framework;
using UnityEngine;

namespace Elvenwood
{
    public interface IUIElementModel: IModel
    {
        BindableProperty<GameObject> BlackTransition { get; set; }
        BindableProperty<GameObject> LoadPanel { get; set; }
    }
    public class UIElementModel : AbstractModel, IUIElementModel
    {
        private IUIElementModel mIuiElementModelImplementation;

        protected override void OnInit()
        {
            
        }

        public BindableProperty<GameObject> BlackTransition { get; set; } = new BindableProperty<GameObject>(null);
        public BindableProperty<GameObject> LoadPanel { get; set; } = new BindableProperty<GameObject>(null);

    }
}