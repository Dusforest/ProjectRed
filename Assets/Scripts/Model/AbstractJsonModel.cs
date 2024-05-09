using Framework;
using UnityEngine;

namespace Elvenwood
{
    public interface IIsJsonModel
    {
        void Save();
        void Load();
    }
    public interface IJsonModel<TSerializeData> : IModel, IIsJsonModel
    {
        TSerializeData Data { get; set; }
    }
    /// <summary>
    /// 适用于需要实现Json存储的Model
    /// </summary>
    /// <typeparam name="TSerializeData">为存储数据的数据类型，需要单独实现</typeparam>
    public abstract class AbstractJsonModel<TSerializeData> : AbstractModel, IJsonModel<TSerializeData>
    {
        public abstract TSerializeData Data { get; set; }

        public void Save()
        {
            SaveByJson();
        }

        public void Load()
        {
            LoadFromJson();
        }

        void SaveByJson()
        {
            ;
            this.GetUtility<IStorage>().SaveByJson(typeof(TSerializeData).ToString(), Data);
        }

        void LoadFromJson()
        {
            Debug.Log(typeof(TSerializeData).ToString());
            var saveData =  this.GetUtility<IStorage>().LoadFromJson<TSerializeData>(typeof(TSerializeData).ToString());

            LoadData(saveData);
        }
        
        protected abstract void LoadData(TSerializeData saveData);
    }
}