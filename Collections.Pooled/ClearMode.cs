using System.Runtime.CompilerServices;

namespace Collections.Pooled
{
    /// <summary>
    /// This enum allows control over how data is treated when internal
    /// arrays are returned to the ArrayPool. Be careful to understand 
    /// what each option does before using anything other than the default
    /// of Auto.
    /// </summary>
    public enum ClearMode
    {
        /// <summary>
        /// <para><see cref="Auto"/> has different behavior depending on the host project's target framework.</para>
        /// <para>.NET Standard 2.1, .NET Core 3: Reference types and value types that contain reference types are cleared
        /// when the internal arrays are returned to the pool. Value types that do not contain reference
        /// types are not cleared when returned to the pool.</para>
        /// <para>.NET Standard 2.0, .NET Framework: All user types are cleared before returning to the pool, in case they
        /// contain reference types.
        /// For .NET Standard, Auto and Always have the same behavior.</para>
        /// </summary>
        Auto = 0,
        /// <summary>
        /// <para>The <see cref="Always"/> option has the effect of always clearing user types before returning to the pool.
        /// This is the default behavior on .NET Standard 2.0 and .NET Framework.</para><para>You might want to turn this on in a .NET Core project
        /// if you were concerned about sensitive data stored in value types leaking to other pars of your application.</para> 
        /// </summary>
        Always = 1,
        /// <summary>
        /// <para><see cref="Never"/> will cause pooled collections to never clear user types before returning them to the pool.</para>
        /// <para>You might want to use this setting in a .NET Core 3 or .NET Standard 2.1 project when you know that a particular collection stores
        /// only value types and you want the performance benefit of not taking time to reset array items to their default value.</para>
        /// <para>Be careful with this setting: if used for a collection that contains reference types, or value types that contain
        /// reference types, this setting could cause memory issues by making the garbage collector unable to clean up instances
        /// that are still being referenced by arrays sitting in the ArrayPool.</para>
        /// </summary>
        Never = 2
    }

    internal static class ClearModeUtil
    {
        /// <summary>
        /// Implements the logic described for each case of the <see cref="ClearMode"/> enum.
        /// </summary>
        internal static bool ShouldClear<T>(ClearMode mode)
        {
#if NETSTANDARD2_1 || NETCOREAPP
            return mode == ClearMode.Always
                || (mode == ClearMode.Auto && RuntimeHelpers.IsReferenceOrContainsReferences<T>());
#else
            return mode != ClearMode.Never;
#endif
        }
    }
}
