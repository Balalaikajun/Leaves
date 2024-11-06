using StackExchange.Redis;
using System.Text.Json;

namespace Acces.Services
{
    [Obsolete("Класс недоработан, воздержитесь от его использование.")]
    internal class RedisService<T>
    {
        //Интеграция кеширования отложена на потом


        //Формат ключа Redis: "[_keyPrefix][Идентификатор]".
        private string _keyPrefix; //Для избежания повторения ключей. Название хранимых данных вполне подходит
        private IDatabase _database;
        public RedisService(string keyPrefix, IDatabase connection)
        {
            _database = connection;
            _database = connection;
        }

        public void Set(string id, T value, TimeSpan? liveTime = null)
        {
            _database.StringSet(
                _keyPrefix + id,
                JsonSerializer.Serialize<T>(value), liveTime);
        }

        public T Get(string id)
        {
            return JsonSerializer.Deserialize<T>(
                _database.StringGet(_keyPrefix + id));
        }

        public void Delete(string id)
        {
            _database.KeyDelete(nameof(T) + id);
        }


    }
}
