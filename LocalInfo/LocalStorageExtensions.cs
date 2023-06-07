using Blazored.LocalStorage;

namespace WereWolfMud.LocalInfo
{
    public static class LocalStorageExtensions
    {
        public static async Task SaveLocalInfo(this ILocalStorageService storageService, LocalStorageInfo localStorage)
        {
            await storageService.SetItemAsync<LocalStorageInfo>(nameof(LocalStorageInfo), localStorage);
        }

        public static async Task<LocalStorageInfo> GetLocalInfo(this ILocalStorageService storageService)
        {
            var localInfo = await storageService.GetItemAsync<LocalStorageInfo>(nameof(LocalStorageInfo));

            return localInfo;
        }
    }
}
