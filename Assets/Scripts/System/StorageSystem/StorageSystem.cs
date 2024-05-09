using System.Collections.Generic;
using Framework;

namespace Elvenwood
{
    public interface IStorageSystem : ISystem
    {
        void Save();
        
        void Load();
    }
    public class StorageSystem : AbstractSystem, IStorageSystem
    {
        private IStorage mStorageUtility;
        private List<IIsJsonModel> mJsonModels;
        protected override void OnInit()
        {
            mStorageUtility = this.GetUtility<IStorage>();
            mJsonModels = new List<IIsJsonModel>();
            GetJsonModels();
        }

        public void Save()
        {
            foreach (var variable in mJsonModels)
            {
                variable.Save();
            }
        }
        
        public void Load()
        {
            foreach (var variable in mJsonModels)
            {
                variable.Load();
            }
        }

        void GetJsonModels()
        {
            var tempList = Elvenwood.Instance.GetModels<IModel>();
            foreach (var variable in tempList)
            {
                if (variable is IIsJsonModel)
                {
                    mJsonModels.Add(variable as IIsJsonModel);
                }
            }
        }
    }
}