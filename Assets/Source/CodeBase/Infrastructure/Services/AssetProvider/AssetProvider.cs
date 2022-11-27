using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace SimpleRPG.Services.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        private readonly Dictionary<string, AsyncOperationHandle> completedCashe = new Dictionary<string, AsyncOperationHandle>();
        private readonly Dictionary<string, List<AsyncOperationHandle>> handles = new Dictionary<string, List<AsyncOperationHandle>>();

        public void Initialize()
            => Addressables.InitializeAsync();

        public async Task<T> Load<T>(AssetReference reference) where T : class
        {
            if (completedCashe.TryGetValue(reference.AssetGUID, out var completedHandles))
                return completedHandles.Result as T;

            var handle = Addressables.LoadAssetAsync<T>(reference);
            return await RunWithCacheOnComplete(reference.AssetGUID, handle);
        }

        public async Task<T> Load<T>(string addres) where T : class
        {
            if (completedCashe.TryGetValue(addres, out var completedHandles))
                return completedHandles.Result as T;

            var handle = Addressables.LoadAssetAsync<T>(addres);
            return await RunWithCacheOnComplete<T>(addres, handle);
        }

        public void Cleanup()
        {
            foreach (List<AsyncOperationHandle> resourceHandles in handles.Values)
                foreach (AsyncOperationHandle handle in resourceHandles)
                    Addressables.Release(handle);

            completedCashe.Clear();
            handles.Clear();
        }

        private async Task<T> RunWithCacheOnComplete<T>(string name, AsyncOperationHandle<T> handle) where T : class
        {
            handle.Completed += (h) =>
            {
                completedCashe[name] = h;
            };
            AddHandle(name, handle);
            return await handle.Task;
        }

        private void AddHandle<T>(string name, AsyncOperationHandle<T> handle) where T : class
        {
            if (!handles.TryGetValue(name, out var resourceHandles))
            {
                resourceHandles = new List<AsyncOperationHandle>();
                handles[name] = resourceHandles;
            }
            resourceHandles.Add(handle);
        }
    }
}
