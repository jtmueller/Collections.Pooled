using System;

namespace Collections.Pooled
{
#if !NETCOREAPP3_0
    // As of this writing, these attributes related to nullable
    // reference types are not available outside of NETCOREAPP3_0
    // so these dummies prevent compiler errors.

    internal class DoesNotReturnAttribute : Attribute
    {
    }

    internal class MaybeNullAttribute : Attribute
    {
    }
    
    internal class MaybeNullWhen : Attribute
    {
        public MaybeNullWhen(bool _) {}
    }
#endif
}
