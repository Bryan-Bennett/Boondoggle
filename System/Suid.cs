namespace System
{
    /// <summary>
    /// The Suid class (Suid stands for "Short Unique Identifier") converts a <see cref="Guid"/> into a 22-character unique identifier by
    /// converting the Guid into a base-64 string and removing the trailing '==' on the end of the resulting string.
    /// </summary>
    public struct Suid : IEquatable<Suid>, IComparable, IComparable<Suid>
    {
        private string _Value;

        /// <summary>
        /// The empty Suid.
        /// </summary>
        public static readonly Suid Empty = new Suid(Guid.Empty);

        private string Value
        {
            get
            {
                if (_Value == null)
                {
                    _Value = Suid.Empty.Value;
                }
                return _Value;
            }
            set => _Value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Suid"/> struct.
        /// </summary>
        /// <param name="guid">The unique identifier that generates the Suid.</param>
        public Suid(Guid guid)
        {
            _Value = Convert.ToBase64String(guid.ToByteArray()).Substring(0, 22);
        }

        /// <summary>
        /// Creates a new Suid object and returns it.
        /// </summary>
        /// <returns></returns>
        public static Suid NewSuid()
        {
            return new Suid(Guid.NewGuid());
        }

        /// <summary>
        /// Equals the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public bool Equals(Suid other)
        {
            if (Value == other.Value)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Suid item))
            {
                return false;
            }
            return Value.Equals(item.Value);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return Value;
        }

        /// <summary>
        /// Compares the object to this Suid object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            if (obj is Suid item)
            {
                return CompareTo(item);
            }
            return Value.CompareTo(obj);
        }

        /// <summary>
        /// Compares the Suid to this Suid object.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public int CompareTo(Suid other)
        {
            return Value.CompareTo(other.Value);
        }
    }
}
