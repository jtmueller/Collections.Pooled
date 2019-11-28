using System;
using System.Collections;

namespace Collections.Pooled.Tests.PooledList
{
    public abstract class List_NonGeneric_Tests<T> : IList_NonGeneric_Tests
    {
        public override bool SupportsJson => true;
        public override Type CollectionType => typeof(PooledList<T>);

        protected override IList NonGenericIListFactory()
        {
            var list = new PooledList<T>();
            RegisterForDispose(list);
            return list;
        }

        protected override bool Enumerator_Current_UndefinedOperation_Throws => true;
        protected override bool IList_CurrentAfterAdd_Throws => false;
        protected override bool NullAllowed => default(T) == null;
        protected override Type ICollection_NonGeneric_CopyTo_NonZeroLowerBound_ThrowType
            => typeof(ArgumentOutOfRangeException);
        protected override Type ICollection_NonGeneric_CopyTo_IndexLargerThanArrayCount_ThrowType
            => typeof(ArgumentException);
        protected override Type ICollection_NonGeneric_CopyTo_ArrayOfEnumType_ThrowType
            => typeof(ArgumentException);
    }

    public class List_NonGeneric_Tests_Int : List_NonGeneric_Tests<int>
    {
        protected override object CreateT(int seed)
        {
            Random rand = new Random(seed);
            return rand.Next();
        }
    }

    public class List_NonGeneric_Tests_String : List_NonGeneric_Tests<string>
    {
        protected override object CreateT(int seed)
        {
            int stringLength = seed % 10 + 5;
            Random rand = new Random(seed);
            byte[] bytes = new byte[stringLength];
            rand.NextBytes(bytes);
            return Convert.ToBase64String(bytes);
        }
    }
}
