namespace StackClass
{
    internal static class TypeUtils
    {
        public unsafe static int HeapSize(in object referenceType) 
        {
            ArgumentNullException.ThrowIfNull(referenceType);

            var type = referenceType.GetType();
            SimpleMethodTable* methodTable = (SimpleMethodTable*)type.TypeHandle.Value;

            if (type.IsArray)
            {
                Array arr = referenceType as Array ?? throw new InvalidCastException("Array type cannot be null");
                return methodTable->BaseSizeSizeInBytes + arr.Length * methodTable->Flags.ComponentSize;
            }

            if (referenceType is string stringValue)
            {
                return methodTable->BaseSizeSizeInBytes + stringValue.Length * methodTable->Flags.ComponentSize;
            }

            return methodTable->BaseSizeSizeInBytes;
        }
    }
}
