using Framework;
using UnityEngine;

namespace Elvenwood
{
    public abstract class AbstractController :  MonoBehaviour, IController
    {
        public IArchitecture GetArchitecture()
        {
            return Elvenwood.Instance;
        }
    }
}