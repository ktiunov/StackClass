using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace StackClass;

internal class Program
{
    private const int BufferSizeInChars = 16;
    private const int FirstFieldOffsetInChars = 4;

    static void Main(string[] args)
    {
        Test(out TestClassContainer heap, out TestClassContainer stack);

        //Try to call method  by reference
        heap.Reference.Test(); // Hello from Heap!
        //stack.Reference.Test(); // AccessViolationException here
    }

    unsafe static void Test(out TestClassContainer heapTest, out TestClassContainer stackTest)
    {
        var objectOnHeap = new DerivedClass();

        Span<char> buffer = stackalloc char[BufferSizeInChars]; // Allocate char buffer on stack
        CopyObjectToStack(objectOnHeap, ref buffer);

        // changed private fields values
        buffer[FirstFieldOffsetInChars] = 'S';
        buffer[FirstFieldOffsetInChars + 1] = 't';
        buffer[FirstFieldOffsetInChars + 2] = 'a';
        buffer[FirstFieldOffsetInChars + 3] = 'c';
        buffer[FirstFieldOffsetInChars + 4] = 'k';

#pragma warning disable CS8500 // It's the aim of this sample
        BaseClass objectOnStack = Unsafe.AsRef<BaseClass>((BaseClass*)&buffer); // Cast buffer pointer to BaseClass object
#pragma warning restore CS8500 // This takes the address of, gets the size of, or declares a pointer to a managed type

        objectOnHeap.Test(); // Child class. Hello from Heap!
        objectOnStack.Test(); // Child class. Hello from Stack // This shows that method call via VMT which placed on stack

        heapTest = new(objectOnHeap); // Save the reference to heap
        stackTest = new(objectOnStack); // Save the reference to stack
    }

    private static unsafe Span<char> CopyObjectToStack(DerivedClass objectOnHeap, ref Span<char> buffer)
    {
        GCHandle gcHandle = GCHandle.Alloc(objectOnHeap, GCHandleType.WeakTrackResurrection); // Allocate the weak reference to objectOnHeap
        IntPtr thePointer = Marshal.ReadIntPtr(GCHandle.ToIntPtr(gcHandle)); // Read the pointer to objectOnHeap from the weak reference
        Span<char> spanUnmanagedBuffer = new(thePointer.ToPointer(), BufferSizeInChars); // Cast the pointer to Span<char>
        gcHandle.Free();

        spanUnmanagedBuffer.CopyTo(buffer); // Copy objectOnHeap object to stack
        return buffer;
    }
}


