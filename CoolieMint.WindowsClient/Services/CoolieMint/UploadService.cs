using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolieMint.WindowsClient.Services
{
    public class UploadService : IUploadService
    {
        public async Task Upload(byte[] content)
        {
            await Task.CompletedTask;
        }
    }
}
