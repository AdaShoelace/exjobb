using System.Collections;
using System.Collections.Generic;
using Unidux;
using UniRx;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

namespace Pierre.Unidux
{
    public class Unidux : SingletonMonoBehaviour<Unidux>, IStoreAccessor
    {
        private static string logFile = "Assets/Resources/Logs/" + System.DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + ".txt";
        private Store<SceneState> _store;

        public IStoreObject StoreObject
        {
            get { return Store; }
        }

        public static SceneState State
        {
            get { return Store.State; }
        }

        public static Subject<SceneState> Subject
        {
            get { return Store.Subject; }
        }

        private static SceneState InitialState
        {
            get
            {
                return new SceneState();
            }
        }

        public static Store<SceneState> Store
        {
            get
            {
                Instance._store = Instance._store ?? new Store<SceneState>(InitialState, new Reducers.Reducer());
                return Instance._store;
            }
        }

        public static object Dispatch<TAction>(TAction action)
        {
            return Store.Dispatch(action);
        }

        void Update()
        {
            Store.Update();
        }

        void Start()
        {
            Store.ApplyMiddlewares(
                Logger
            );
        }

        public static System.Func<System.Func<object, object>, System.Func<object, object>> Logger(IStoreObject store)
        {
            return (System.Func<object, object> next) => (object action) =>
            {
                var sb = new System.Text.StringBuilder();
                sb.AppendLine("Previous: " + JsonConvert.SerializeObject((SceneState)store.ObjectState));
                sb.AppendLine("Action: " + JsonConvert.SerializeObject(((Action)action).ActionType.ToString()));
                var result = next(action);
                sb.AppendLine("Next state: " + JsonConvert.SerializeObject((SceneState)store.ObjectState));
                Debug.Log(sb.ToString());
                LogToFile(logFile, sb.ToString());
                return result;
            };
        }
        private static void LogToFile(string path, string ToLog)
        {
            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }
            StreamWriter sw = File.AppendText(path);
            sw.WriteLine(ToLog);
            sw.Close();
        }
    }
}