using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipaaKernelV3.Core
{
    public class FileSystemDriver
    {
        private CosmosVFS vfs;

        public void Initialize()
        {
            vfs = new CosmosVFS();
            VFSManager.RegisterVFS(vfs, false);
        }

        public CosmosVFS GetVFS()
        {
            return vfs;
        }
    }
}
