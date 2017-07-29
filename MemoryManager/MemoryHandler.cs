using System;

namespace MemoryManager
{
    public class MemoryHandler
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

        public void CloseProcess()
        {
        }
    }
}