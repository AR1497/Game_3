using System.IO;
using UnityEngine;

namespace Hmmmm
{
    public sealed class SaveDataRepository
    {
        private readonly IData<SavedData> _data;

        private const string _folderName = "dataSave";
        private const string _fileName = "data.bat";
        private readonly string _path;

        public SaveDataRepository()
        {
            if (Application.platform == RuntimePlatform.WebGLPlayer)
            {
                //_data = new PlayerPrefsData();
            }
            else
            {
                // _data = new JsonData<SavedData>();
                _data = new SerializableXMLData<SavedData>();
            }
            _path = Path.Combine(Application.dataPath, _folderName);
        }

        public void Save(SavedData saveData)
        {
            if (!Directory.Exists(Path.Combine(_path)))
            {
                Directory.CreateDirectory(_path);
            }

            _data.Save(saveData, Path.Combine(_path, _fileName));
        }

        public SavedData Load()
        {
            var file = Path.Combine(_path, _fileName);

            if (!File.Exists(file)) new SavedData();

            var newPlayer = _data.Load(file);

            return newPlayer;
        }
    }
}