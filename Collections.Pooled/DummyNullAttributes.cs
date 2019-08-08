using System;

namespace Collections.Pooled
{
#if !NETCOREAPP3_0
    // As of this writing, these attributes related to nullable
    // reference types are not available outside of NETCOREAPP3_0
    // so these dummies prevent compiler errors.

    internal sealed class DoesNotReturnAttribute : Attribute
    {
    }

    internal sealed class MaybeNullAttribute : Attribute
    {
    }

    internal sealed class MaybeNullWhen : Attribute
    {
        public MaybeNullWhen(bool returnValue)
        {
            ReturnValue = returnValue;
        }

        public bool ReturnValue { get; private set; }
    }
#endif
}
