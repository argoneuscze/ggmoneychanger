using System;

namespace MemoryInjector
{
    public class MemoryHandler
    {
        private IntPtr _procHandle;
        private IntPtr _procBaseAddress;

        public MemoryHandler()
        {
            _procHandle = IntPtr.Zero;
        }

        public void OpenProcess(string processName)
        {
            var result = ApiConnector.GetProcessHandle(processName);
            _procHandle = result.Item1;
            _procBaseAddress = result.Item2;
        }

        public void WriteInt32Ptr(IntPtr pointer, Int32 value)
        {
            if (_procHandle != IntPtr.Zero)
            {
                byte[] data = BitConverter.GetBytes(value);
                IntPtr valPointer = IntPtr.Add(_procBaseAddress, pointer.ToInt32());
                ApiConnector.WriteData(_procHandle, valPointer, data);
            }
        }

        public void CloseProcess()
        {
            if (_procHandle != IntPtr.Zero)
            {
                ApiConnector.CloseProcess(_procHandle);
                _procHandle = IntPtr.Zero;
            }
        }
    }
}