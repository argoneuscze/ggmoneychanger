using System;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace MemoryInjector
{
    public class ApiConnector
    {
        [Flags]
        private enum ProcessAccessFlags : uint
        {
            All = 0x001F0FFF,
            Terminate = 0x00000001,
            CreateThread = 0x00000002,
            VirtualMemoryOperation = 0x00000008,
            VirtualMemoryRead = 0x00000010,
            VirtualMemoryWrite = 0x00000020,
            DuplicateHandle = 0x00000040,
            CreateProcess = 0x000000080,
            SetQuota = 0x00000100,
            SetInformation = 0x00000200,
            QueryInformation = 0x00000400,
            QueryLimitedInformation = 0x00001000,
            Synchronize = 0x00100000
        }

        private static ProcessAccessFlags neededFlags = ProcessAccessFlags.VirtualMemoryOperation
                                                        | ProcessAccessFlags.VirtualMemoryRead
                                                        | ProcessAccessFlags.VirtualMemoryWrite;

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr OpenProcess(ProcessAccessFlags processAccess, bool bInheritHandle, int processId);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] byte[] lpBuffer,
            int dwSize,
            out IntPtr lpNumberOfBytesRead);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer,
            uint nSize, out UIntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool CloseHandle(IntPtr hObject);

        public static Tuple<IntPtr, IntPtr> GetProcessHandle(string name)
        {
            Process[] proc = Process.GetProcessesByName(name);
            try
            {
                int procId = proc[0].Id;
                IntPtr baseAddr = proc[0].MainModule.BaseAddress;
                return Tuple.Create(OpenProcess(neededFlags, false, procId), baseAddr);
            }
            catch (IndexOutOfRangeException e)
            {
                throw new IndexOutOfRangeException("Process not found.", e);
            }
        }

        public static void CloseProcess(IntPtr handle)
        {
            CloseHandle(handle);
        }

        public static void WriteData(IntPtr handle, IntPtr pointer, byte[] data)
        {
            UIntPtr bytesWritten;
            WriteProcessMemory(handle, pointer, data, (uint) data.Length, out bytesWritten);
        }

        public static IntPtr ResolvePointer(IntPtr handle, IntPtr pointer)
        {
            byte[] readPointer = new byte[4];
            IntPtr bytesRead;
            ReadProcessMemory(handle, pointer, readPointer, 4, out bytesRead);
            IntPtr newPointer = new IntPtr(BitConverter.ToInt32(readPointer, 0));
            return newPointer;
        }
    }
}