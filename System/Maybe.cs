namespace System
{
    /// <summary>
    /// The Maybe{TRef} struct permits 'pseudo' nullable reference type behavior and is more descriptive of the intention of an API.
    /// For example, if a method has a Maybe{string} parameter in its signature, that tells callers that the string parameter CAN be null.
    /// If a method returns a Maybe{string}, however, that tells callers that the result of the method MAY return a null string.
    /// </summary>
    /// <typeparam name="TRef">The type of the reference.</typeparam>
    /// <examples>
    /// The following example shows how to use the Maybe{TRef} struct in a method's signature as well as its return type.  It also
    /// demonstrates the convenience of its implicit conversion to its type parameter.
    /// <code>
    /// public static class Program
    /// {
    ///     public static void Main(string[] args)
    ///     {
    ///         var obj1 = "FooBar";
    ///         string obj2 = null;
    ///         Maybe{string} maybe1 = GetStringFromObject(obj1); //obj1 is implicitly converted into a Maybe{string} object when passing it into the method.
    ///         Maybe{string} maybe2 = GetStringFromObject(obj2); //obj2 is implicitly converted into a Maybe{string} object when passing it into the method.
    ///         if (maybe1.Hasvalue)
    ///         {
    ///             Console.WriteLine($"Value1: {maybe1.Value});
    ///         }
    ///         if (!maybe2.HasValue)
    ///         {
    ///             Console.WriteLine($"Value2: NULL");
    ///         }
    ///         Console.ReadKey(true);
    ///     }
    ///     
    ///     public static Maybe{string} GetStringFromObject(Maybe{object} obj)
    ///     {
    ///         if (obj.HasValue)
    ///         {
    ///             return obj.Value.ToString();
    ///         }
    ///         else
    ///         {
    ///             return null; //automatically converts a null string into a Maybe{string} object with an underlying null string value.
    ///         }
    ///     }
    /// }
    /// </code>
    /// Output:
    ///     Value1: FooBar
    ///     Value2: NULL
    /// </examples>
    public struct Maybe<TRef> where TRef : class
    {
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public TRef Value { get; }

        /// <summary>
        /// Gets a value indicating whether this instance has value.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has value; otherwise, <c>false</c>.
        /// </value>
        public bool HasValue { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Maybe{TRef}"/> struct.
        /// </summary>
        /// <param name="value">The value.</param>
        public Maybe(TRef value)
        {
            Value = value;
            HasValue = (Value != null);
        }

        /// <summary>
        /// Performs an implicit conversion from the generic type to <see cref="Maybe{TRef}"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator Maybe<TRef>(TRef value)
        {
            return new Maybe<TRef>(value);
        }
    }
}
