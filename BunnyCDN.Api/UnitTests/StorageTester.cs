using System;
using System.Threading.Tasks;

namespace BunnyCDN.Api.UnitTests
{
    public class StorageTester : StorageInterface
    {
        public string Zone { get { return _zone; } }

        public byte[] responseBytes;
        public StorageEntry[] responseStorageEntry;
        public bool responseBool;
        private string _zone;

        public StorageTester(string zoneName)
        {
            _zone = zoneName;
        }

        public async Task<byte[]> GetFile(string path)
        {
            return await Task.Run(() => { return responseBytes; });
        }

        public async Task<StorageEntry[]> GetFolder(string path)
        {
            return await Task.Run(() => { return responseStorageEntry; });
        }

        public async Task<bool> Put(byte[] fileContent, string path)
        {
            return await Task.Run(() => { return responseBool; });
        }

        public async Task<bool> Delete(string path)
        {
            return await Task.Run(() => { return responseBool; });
        }
    }
}