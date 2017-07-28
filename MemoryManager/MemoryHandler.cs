using System;
using System.Collections.Generic;
using System.Text;

namespace MemoryManager
{
    class MemoryHandler
    {
        private ApiConnector connector;

        public MemoryHandler()
        {
            this.connector = new ApiConnector();
        }

        public void OpenProcess(string processName)
        {
        }

        public void WriteToProcess(IntPtr pointer, byte[] data)
        {
        }
    }
}