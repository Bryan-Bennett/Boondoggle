using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.ComponentModel
{
    /// <summary>
    /// The ObservableObject class exposes convenient methods for subclasses to perform common data-binding operations.
    /// </summary>
    /// <example>
    /// The following example shows how to inherit from ObservableObject so that changes to property values will automatically 
    /// propagate <see cref="INotifyPropertyChanged.PropertyChanged"/> events for databinding operations.
    /// <code>
    /// public class Person : ObservableObject
    /// {
    ///     private string _Name;
    ///     
    ///     private int _Age;
    ///     
    ///     public string Name
    ///     {
    ///         get { return _Name; }
    ///         set
    ///         {
    ///             SetValue(ref _Name, value);
    ///         }
    ///     }
    ///     
    ///     public int Age
    ///     {
    ///         get { return _Age; }
    ///         set
    ///         {
    ///             SetValue(ref _Age, value);
    ///         }
    ///     }
    /// }
    /// </code>
    /// </example>
    /// <threadsafety static="true" instance="false"/>
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        /// <summary>
        /// Invoked when a property value has changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Invokes the <see cref="INotifyPropertyChanged.PropertyChanged"/> event with the specified property name.  If no
        /// name is provided, the calling member's name is used.
        /// </summary>
        /// <param name="propertyName">The name of the property that has changed.</param>
        /// <remarks>
        /// This method is overridable.
        /// </remarks>
        /// <example>
        /// The following properties uses the InvokePropertyChanged method in two different ways.  The FirstName property does not
        /// provides the property name parameter to the method thus resulting in a PropertyChanged event with the 'FirstName' string.
        /// The LastName property provides 'FooBar' as the property name parameter and thus results in a PropertyChanged event with the 'FooBar' string.
        /// 
        /// <code>
        /// private string _FirstName;
        /// 
        /// private string _LastName;
        /// 
        /// public string FirstName
        /// {
        ///     get { return _FirstName; }
        ///     set
        ///     {
        ///         _FirstName = value;
        ///         InvokePropertyChanged(); // causes a PropertyChanged event with the property name as "FirstName".
        ///     }
        /// }
        /// 
        /// public string LastName
        /// {
        ///     get { return _LastName; }
        ///     set
        ///     {
        ///         _LastName = value;
        ///         InvokePropertyChanged("FooBar"); // causes a PropertyChanged event with the property name as "FooBar".
        ///     }
        /// }
        /// </code>
        /// </example>
        protected virtual void InvokePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Sets the referenced field's value equal to the specified value if the values are different.  If they are different, the
        /// PropertyChanged event is invoked with the specified property name.  The <paramref name="propertyName"/> parameter uses the
        /// <see cref="CallerMemberNameAttribute"/> so if no property name is provided, the compiler will automatically use the caller
        /// member's name instead.  This method returns true if the referenced field's value changed.  False, otherwise.
        /// </summary>
        /// <typeparam name="TField">The type of the backing field.</typeparam>
        /// <param name="field">The referenced backing field.</param>
        /// <param name="value">The incoming new value for the referenced backing field.</param>
        /// <param name="propertyName">The name of the property that is changing.</param>
        /// <returns>True if the referenced field's value changed.  False, otherwise.</returns>
        /// <remarks>
        /// This method is overridable.
        /// </remarks>
        /// <example>
        /// 
        /// <code>
        /// private string _FirstName;
        /// 
        /// private string _LastName;
        /// 
        /// public string FirstName
        /// {
        ///     get { return _FirstName; }
        ///     set
        ///     {
        ///         if (SetValue(ref _FirstName, value))
        ///         {
        ///             Debug.WriteLine($"The {nameof(FirstName)} property value has changed.");
        ///         }
        ///     }
        /// }
        /// 
        /// public string LastName
        /// {
        ///     get { return _LastName; }
        ///     set
        ///     {
        ///         SetValue(ref _LastName, value, "FooBar");
        ///     }
        /// }
        /// </code>
        /// </example>
        protected virtual bool SetValue<TField>(ref TField field, TField value, [CallerMemberName] string propertyName = null)
        {
            if (!EqualityComparer<TField>.Default.Equals(field, value))
            {
                field = value;
                InvokePropertyChanged(propertyName);
                return true;
            }
            return false;
        }
    }
}