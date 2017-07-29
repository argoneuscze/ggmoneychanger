using System;

namespace MemoryInjector
{
    public class MemoryHandler
    {
        private IntPtr _procPointer;

        public MemoryHandler()
        {
            _procPointer = IntPtr.Zero;
        }

        public void OpenProcess(string processName)
        {
            _procPointer = ApiConnector.GetProcessHandle(processName);
        }

        public void WriteInt32(IntPtr pointer, Int32 value)
        {
            if (_procPointer != IntPtr.Zero)
            {
                byte[] data = BitConverter.GetBytes(value);
                ApiConnector.WriteData(_procPointer, pointer, data);
            }
        }

        public void CloseProcess()
        {
            if (_procPointer != IntPtr.Zero)
            {
                ApiConnector.CloseProcess(_procPointer);
                _procPointer = IntPtr.Zero;
            }
        }
    }
}