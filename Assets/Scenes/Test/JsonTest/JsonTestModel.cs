using System.Collections.Generic;
using System.Linq;
using Framework;

namespace Elvenwood.Test
{
    public interface IJsonTestModel : IJsonModel<JsonTestModelData>
    {


    }
    
    [System.Serializable]
    public class JsonTestModelData
    {
        public JsonTestModelData(int testHp, float testSpeed, string testName, List<int> testList,
            SerializableDictionary<int, string> testDic)
        {
            this.testHp = new BindableProperty<int>(testHp);
            this.testSpeed = new BindableProperty<float>(testSpeed);
            this.testName = new BindableProperty<string>(testName);
            this.testList = new BindableProperty<List<int>>(testList);
            this.testDic = new BindableProperty<SerializableDictionary<int, string>>(testDic);
        }
        
        public BindableProperty<int> testHp;
        public BindableProperty<float> testSpeed;
        public BindableProperty<string> testName;
        public BindableProperty<List<int>> testList;
        public BindableProperty<SerializableDictionary<int, string>> testDic;
    }


    public class JsonTestModel : AbstractJsonModel<JsonTestModelData>, IJsonTestModel
    {
        protected override void OnInit()
        {
            DataFileName = "TestData.sav";
            Data = new JsonTestModelData(0, 0, null, new List<int>(), new SerializableDictionary<int, string>());
        }

        public override JsonTestModelData Data { get; set; }
        
        protected virtual string DataFileName { get; set; }

        protected override void LoadData(JsonTestModelData saveData)
        {
            Data.testHp = saveData.testHp;
            Data.testSpeed = saveData.testSpeed;
            Data.testName = saveData.testName;
            Data.testList = saveData.testList;
            Data.testDic = saveData.testDic;
        }
    }
}