using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Framework
{
    #region Architecture
    public interface IArchitecture
    {
        void RegisterModel<TModel>(TModel model) where TModel : IModel;
        TModel GetModel<TModel>() where TModel : class, IModel;
        void RegisterUtility<TUtility>(TUtility utility) where TUtility : IUtility;
        TUtility GetUtility<TUtility>() where TUtility : class, IUtility;
        void RegisterSystem<TSystem>(TSystem system) where TSystem : ISystem;
        TSystem GetSystem<TSystem>() where TSystem : class, ISystem;

        void SendCommand<TCommand>() where TCommand : ICommand, new();
        void SendCommand<TCommand>(TCommand command) where TCommand : ICommand;

        TResult SendQuery<TResult>(IQuery<TResult> query);

        void SendEvent<TEvent>() where TEvent : new();

        void SendEvent<TEvent>(TEvent e);

        IUnRegister RegisterEvent<TEvent>(Action<TEvent> onEvent);

        void UnRegisterEvent<TEvent>(Action<TEvent> onEvent);
    }
    
    
    /// <summary>
    /// 使用时记得严格按照分层原则使用注册与获取方法。继承者作为游戏的架构本体，需要在一开始进行初始化。//TODO 当前的处理方式为：架构本体被永不销毁的GameManager调用并在其Awake事件函数中调用，除GameManager外其余所有继承自MonoBehaviour的脚本都不可调用Awake，而是调用Start。若同一场景出现多个Start函数顺序出现冲突则采用Init手动管理，或设置Start函数执行顺序
    /// </summary>
    /// <typeparam name="T">这里的T只用于指代架构的泛型，所以其中具体方法自身涉及到的泛型用TT来指代,以便区分</typeparam>
    public abstract class Architecture<T> : IArchitecture where T : Architecture<T>, new()
    {
        private static T mInstance;
        
        private List<IModel> mModels = new List<IModel>();
        private List<ISystem> mSystems = new List<ISystem>();

        public static T Instance
        {
            get
            {
                MakeSureArchitecture();
                return mInstance;
            }
        }

        private static void MakeSureArchitecture()
        {
            if (mInstance == null)
            {
                mInstance = new T();
                mInstance.Init();
                    
                foreach (var model in mInstance.mModels)
                {
                    model.Init();
                }
                    
                foreach (var system in mInstance.mSystems)
                {
                    system.Init();
                }
            }
        }
        
        protected abstract void Init();
        
        private IocContainer mContainer = new IocContainer();
        
        public void RegisterModel<TModel>(TModel instance) where TModel : IModel
        {
            mContainer.Register(instance);
            instance.SetArchitecture(mInstance);
            mModels.Add(instance);
        }

        public TModel GetModel<TModel>() where TModel : class, IModel
        {
            return mContainer.Get<TModel>();
        }
        
        public List<IModel> GetModels<TModel>() where TModel : class, IModel
        {
            return mModels;
        }

        public void RegisterUtility<TUtility>(TUtility instance) where TUtility : IUtility
        {
            mContainer.Register(instance);
        }

        public TUtility GetUtility<TUtility>() where TUtility : class, IUtility
        {
            return mContainer.Get<TUtility>();
        }

        public void RegisterSystem<TSystem>(TSystem instance) where TSystem : ISystem
        {
            mContainer.Register(instance);
            instance.SetArchitecture(mInstance);
            mSystems.Add(instance);
        }

        public TSystem GetSystem<TSystem>() where TSystem : class, ISystem
        {
            return mContainer.Get<TSystem>();
        }
        
        public List<ISystem> GetSystems<TSystem>() where TSystem : class, ISystem
        {
            return mSystems;
        }

        public void SendCommand<TCommand>() where TCommand : ICommand, new()
        {
            var command = new TCommand();
            command.SetArchitecture(mInstance);
            command.Execute();
            command.SetArchitecture(null);
        }

        public void SendCommand<TCommand>(TCommand command) where TCommand : ICommand
        {
            command.SetArchitecture(mInstance);
            command.Execute();
            command.SetArchitecture(null);
        }

        public TResult SendQuery<TResult>(IQuery<TResult> query)
        {
            query.SetArchitecture(this);
            return query.Do();
        }


        private ITypeEventSystem mTypeEventSystem = new TypeEventSystem();
        
        public void SendEvent<TEvent>() where TEvent : new()
        {
            mTypeEventSystem.Send<TEvent>();
        }

        public void SendEvent<TEvent>(TEvent e)
        {
            mTypeEventSystem.Send(e);
        }

        public IUnRegister RegisterEvent<TEvent>(Action<TEvent> onEvent)
        {
            return mTypeEventSystem.Register(onEvent);
        }

        public void UnRegisterEvent<TEvent>(Action<TEvent> onEvent)
        {
            mTypeEventSystem.UnRegister(onEvent);
        }
    }
    #endregion

    #region Controller
    public interface IController : ICanSendCommand, ICanGetSystem, ICanGetModel, ICanRegisterEvent, ICanSendQuery
    {

    }
    #endregion

    #region System
    public interface ISystem : ICanSetArchitecture, ICanGetUtility, ICanGetModel, ICanSendEvent, ICanRegisterEvent, ICanGetSystem
    {
        void Init();
    }
    public abstract class AbstractSystem : ISystem
    {
        private IArchitecture mArchitecture;
        
        void ISystem.Init()
        {
            OnInit();
        }

        protected abstract void OnInit();
        
        void ICanSetArchitecture.SetArchitecture(IArchitecture architecture)
        {
            mArchitecture = architecture;
        }

        IArchitecture ICanGetArchitecture.GetArchitecture()
        {
            return mArchitecture;
        }
    }
    #endregion

    #region Model
    public interface IModel : ICanSetArchitecture, ICanGetUtility, ICanSendEvent
    {
        void Init();
    }
    
    public abstract class AbstractModel: IModel
    {
        private IArchitecture mArchitecture;
        
        void IModel.Init()
        {
            OnInit();
        }

        protected abstract void OnInit();
        
        void ICanSetArchitecture.SetArchitecture(IArchitecture architecture)
        {
            mArchitecture = architecture;
        }

        IArchitecture ICanGetArchitecture.GetArchitecture()
        {
            return mArchitecture;
        }
    }
    
    #endregion

    #region Utility
    public interface IUtility 
    {
        
    }
    #endregion

    #region Command
    public interface ICommand : ICanSetArchitecture, ICanGetUtility, ICanGetModel, ICanGetSystem, ICanSendCommand, ICanSendEvent, ICanSendQuery
    {
        void Execute();
    }

    public abstract class AbstractCommand : ICommand
    {
        private IArchitecture mArchitecture;

        void ICanSetArchitecture.SetArchitecture(IArchitecture architecture)
        {
            mArchitecture = architecture;
        }
        
        IArchitecture ICanGetArchitecture.GetArchitecture()
        {
            return mArchitecture; 
        }

        void ICommand.Execute()
        {
            OnExecute();
        }

        protected abstract void OnExecute();

    }
    #endregion
    
    #region Query
    public interface IQuery<TResult> : ICanSetArchitecture, ICanGetModel, ICanGetSystem, ICanSendQuery
    {
        TResult Do();
    }

    public abstract class AbstractQuery<T> : IQuery<T>
    {
        public T Do()
        {
            return OnDo();
        }

        protected abstract T OnDo();

        private IArchitecture mArchitecture;
            
        public IArchitecture GetArchitecture()
        {
            return mArchitecture;
        }

        public void SetArchitecture(IArchitecture architecture)
        {
            mArchitecture = architecture;
        }
    }
    #endregion

    #region Rule
    public interface ICanGetArchitecture
    {
        IArchitecture GetArchitecture();
    }
    
    public interface ICanSetArchitecture
    {
        void SetArchitecture(IArchitecture architecture);
    }
    
    public interface ICanGetModel : ICanGetArchitecture
    {
          
    }

    public static class CanGetModelExtension
    {
        public static T GetModel<T>(this ICanGetModel self) where T : class, IModel
        {
            return self.GetArchitecture().GetModel<T>();
        }
    }
    
    public interface ICanGetSystem : ICanGetArchitecture
    {
        
    }
    
    public static class CanGetSystemExtension
    {
        public static T GetSystem<T>(this ICanGetSystem self) where T : class, ISystem
        {
            return self.GetArchitecture().GetSystem<T>();
        }
    }
    
    public interface ICanGetUtility : ICanGetArchitecture
    {
        
    }

    public static class CanGetUtilityExtension
    {
        public static T GetUtility<T>(this ICanGetUtility self) where T : class, IUtility
        {
            return self.GetArchitecture().GetUtility<T>();
        }
    }
    
    public interface ICanRegisterEvent : ICanGetArchitecture
    {
        
    }

    public static class CanRegisterEventExtension
    {
        public static IUnRegister RegisterEvent<T>(this ICanRegisterEvent self, Action<T> onEvent)
        {
            return self.GetArchitecture().RegisterEvent(onEvent);
        }
        
        public static void UnRegisterEvent<T>(this ICanRegisterEvent self, Action<T> onEvent)
        {
            self.GetArchitecture().UnRegisterEvent(onEvent);
        }
    }
    
    public interface ICanSendCommand : ICanGetArchitecture
    {
        
    }

    public static class CanSendCommandExtension
    {
        public static void SendCommand<T>(this ICanSendCommand self) where T : ICommand, new()
        {
            self.GetArchitecture().SendCommand<T>();
        }
        
        public static void SendCommand<T>(this ICanSendCommand self, T command) where T : ICommand
        {
            self.GetArchitecture().SendCommand(command);
        }
    }
    
    public interface ICanSendEvent : ICanGetArchitecture
    {
        
    }

    public static class CanSendEventExtention
    {
        public static void SendEvent<T>(this ICanSendEvent self) where T : new()
        {
            self.GetArchitecture().SendEvent<T>();
        }
        
        public static void SendEvent<T>(this ICanSendEvent self, T e)
        {
            self.GetArchitecture().SendEvent(e);
        }
    }
    
    public interface ICanSendQuery : ICanGetArchitecture
    {
        
    }

    public static class CanSendQueryExtension
    {
        public static TResult SendQuery<TResult>(this ICanSendQuery self, IQuery<TResult> query)
        {
            return self.GetArchitecture().SendQuery(query);
        }
    }
    #endregion

    #region TypeEventSystem
    public interface ITypeEventSystem
    {
        void Send<T>() where T : new();
        void Send<T>(T e);
        IUnRegister Register<T>(Action<T> onEvent);
        void UnRegister<T>(Action<T> onEvent);
    }

    public interface IUnRegister
    {
        void UnRegister();
    }

    public struct TypeEventSystemUnRegister<T> : IUnRegister
    {
        public ITypeEventSystem typeEventSystem;
        public Action<T> onEvent;
        public void UnRegister()
        {
            typeEventSystem.UnRegister(onEvent);

            typeEventSystem = null;

            onEvent = null;
        }
    }
    
    public class UnRegisterOnDestroyTrigger : MonoBehaviour
    {
        private HashSet<IUnRegister> mUnRegisters = new HashSet<IUnRegister>();

        public void AddUnRegister(IUnRegister unRegister)
        {
            mUnRegisters.Add(unRegister);
        }

        private void OnDestroy()
        {
            foreach (var unRegister in mUnRegisters)
            {
                unRegister.UnRegister();
            }
            
            mUnRegisters.Clear();
        }
    }
    
    public static class UnRegisterExtension
    {
        public static void UnRegisterWhenGameObjectDestroyed(this IUnRegister unRegister, GameObject gameObject)
        {
            var trigger = gameObject.GetComponent<UnRegisterOnDestroyTrigger>();

            if (!trigger)
            {
                trigger = gameObject.AddComponent<UnRegisterOnDestroyTrigger>();
            }
            trigger.AddUnRegister(unRegister);
        }
    }
    
    public class TypeEventSystem : ITypeEventSystem
    {
        public interface IRegistrations
        {
            
        }

        public class Registrations<T> : IRegistrations
        {
            public Action<T> onEvent = _ => { };
        }

        private Dictionary<Type, IRegistrations> mEventRegistration = new Dictionary<Type, IRegistrations>();

        public void Send<T>() where T : new()
        {
            var e = new T();
            Send(e);
        }

        public void Send<T>(T e)
        {
            var type = typeof(T);

            if (mEventRegistration.TryGetValue(type, out var registrations))
            {
                (registrations as Registrations<T>).onEvent(e);
            }
        }

        public IUnRegister Register<T>(Action<T> onEvent)
        {
            var type = typeof(T);

            if (mEventRegistration.TryGetValue(type, out var registrations))
            {
                
            }
            else
            {
                registrations = new Registrations<T>();
                mEventRegistration.Add(type, registrations);
            }

            (registrations as Registrations<T>).onEvent += onEvent;

            return new TypeEventSystemUnRegister<T>()
            {
                onEvent = onEvent,
                typeEventSystem = this
            };
        }

        public void UnRegister<T>(Action<T> onEvent)
        {
            var type = typeof(T);
            IRegistrations registrations;

            if (mEventRegistration.TryGetValue(type, out registrations))
            {
                (registrations as Registrations<T>).onEvent -= onEvent;
            }
        }
    }

    #endregion

    #region IOCContainer
    public class IocContainer
    {
        private Dictionary<Type, object> mInstances = new Dictionary<Type, object>();

        public void Register<T>(T instance)
        {
            var key = typeof(T);

            if (mInstances.ContainsKey(key))
            {
                mInstances[key] = instance;
            }
            else
            {
                mInstances.Add(key, instance);
            }
        }

        public T Get<T>() where T : class
        {
            var key = typeof(T);

            if (mInstances.TryGetValue(key, out var reInstance))
            {
                return reInstance as T;
            }

            return null;
        }
        
    }
    #endregion

    #region BindableProperty
    [Serializable]
    public class BindableProperty<T> 
    {
        public BindableProperty(T defaultValue = default)
        {
            value = defaultValue;
        }
        [SerializeField] private T value;

        public T Value
        {
            get
            {
                return value;
            }
            set
            {
                if(value == null && this.value == null) return;
                if(value != null && value.Equals(this.value)) return;
                
                this.value = value;
                mOnValueChanged?.Invoke(value);
            }
        }

        private Action<T> mOnValueChanged = _ => { };

        public IUnRegister Register(Action<T> onValueChanged)
        {
            mOnValueChanged += onValueChanged;
            return new BindablePropertyUnRegister<T>()
            {
                BindableProperty = this,
                OnValueChanged = onValueChanged
            };
        }

        public IUnRegister RegisterWithValue(Action<T> onValueChanged)
        {
            onValueChanged(value);
            return Register(onValueChanged);
        }

        public static implicit operator T(BindableProperty<T> property)
        {
            return property.Value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
        
        public void UnRegister(Action<T> onValueChanged)
        {
            mOnValueChanged -= onValueChanged;
        }
        
    }
    

    public class BindablePropertyUnRegister<T> : IUnRegister 
    {
        public BindableProperty<T> BindableProperty { get; set; }
        
        public Action<T> OnValueChanged { get; set; }
        
        public void UnRegister()
        {
            BindableProperty.UnRegister(OnValueChanged);

            BindableProperty = null;
            OnValueChanged = null;
        }
    }
    #endregion
}