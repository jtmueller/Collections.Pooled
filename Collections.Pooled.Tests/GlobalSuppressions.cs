
// This file is used by Code Analysis to maintain SuppressMessage 
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given 
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Assertions",
    "xUnit2017:Do not use Contains() to check if a value exists in a collection",
    Justification = "While testing collections, we want to exercise the collection's own Contains method.")]

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Assertions",
    "xUnit2013:Do not use equality check to check for collection size.",
    Justification = "While testing collections, we want to exercise the collection's own Count property.")]

[assembly: SuppressMessage("Usage", 
    "xUnit1026:Theory methods should use all of their parameters", 
    Justification = "Not always practical in this codebase.")]
