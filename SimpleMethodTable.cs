using System.Runtime.InteropServices;

namespace StackClass
{
    [StructLayout(LayoutKind.Explicit)]
    internal struct SimpleMethodTable
    {
        // Low WORD is component size for array and string types (HasComponentSize() returns true).
        // Used for flags otherwise.
        [FieldOffset(0)] 
        public VmtFlags Flags;

        // Base size of instance of this class when allocated on the heap
        [FieldOffset(4)]
        public int BaseSizeSizeInBytes;
    }
}
