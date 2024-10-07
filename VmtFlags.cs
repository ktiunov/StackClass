using System.Runtime.InteropServices;

namespace StackClass
{
    [StructLayout(LayoutKind.Explicit)]
    internal struct VmtFlags
    {
        [FieldOffset(0)] 
        internal ushort ComponentSize;

        [FieldOffset(2)] 
        internal ushort Flags;
    }
}
