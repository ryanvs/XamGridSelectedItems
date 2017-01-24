using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace XamGridSelectedItems
{
    /// <summary>
    /// This is the abstract base class for any object that provides property change notifications.
    /// </summary>
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        #region Constructor

        protected ObservableObject()
        {
        }

        #endregion // Constructor

        #region RaisePropertyChanged

        /// <summary>
        /// Raises this object's PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The property that has a new value.</param>
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.VerifyPropertyName(propertyName);

            PropertyChangedEventHandler handler = null;
            lock (this) { handler = this.PropertyChanged; }
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        /// <summary>
        /// Raises this object's PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The property that has a new value.</param>
        protected void RaisePropertyChanged<T>([CallerMemberName] string propertyName = null, PropertyChangedExEventArgs<T> args = null)
        {
            this.VerifyPropertyName(propertyName);

            PropertyChangedEventHandler handler = null;
            lock (this) { handler = this.PropertyChanged; }
            if (handler != null)
            {
                var e = (args != null) ? args : new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        #endregion // RaisePropertyChanged

        #region SetField

        protected virtual bool SetField<T>(ref T field, T value, bool doNotCheckEquality = false, [CallerMemberName] string propertyName = null)
        {
            if (!doNotCheckEquality)
            {
                if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            }
            var args = new PropertyChangedExEventArgs<T>(propertyName, field, value);
            field = value;
            RaisePropertyChanged(propertyName, args);
            return true;
        }

        #endregion

        #region Debugging Aides

        /// <summary>
        /// Warns the developer if this object does not have
        /// a public property with the specified name. This 
        /// method does not exist in a Release build.
        /// </summary>
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public void VerifyPropertyName(string propertyName)
        {
            // If you raise PropertyChanged and do not specify a property name,
            // all properties on the object are considered to be changed by the binding system.
            if (String.IsNullOrEmpty(propertyName))
                return;

            // Verify that the property name matches a real,  
            // public, instance property on this object.
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                string msg = "Invalid property name: " + propertyName;

                if (this.ThrowOnInvalidPropertyName)
                    throw new ArgumentException(msg);
                else
                    Debug.Fail(msg);
            }
        }

        /// <summary>
        /// Returns whether an exception is thrown, or if a Debug.Fail() is used
        /// when an invalid property name is passed to the VerifyPropertyName method.
        /// The default value is false, but subclasses used by unit tests might 
        /// override this property's getter to return true.
        /// </summary>
        protected virtual bool ThrowOnInvalidPropertyName { get; private set; }

        #endregion // Debugging Aides

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Raised when a property on this object has a new value.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion // INotifyPropertyChanged Members

        #region Designer Mode
        public static bool IsInDesignMode
        {
            get { return System.ComponentModel.DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()); }
        }
        #endregion
    }

    public class PropertyChangedExEventArgs<T> : PropertyChangedEventArgs
    {
        public virtual T OldValue { get; private set; }
        public virtual T NewValue { get; private set; }

        public PropertyChangedExEventArgs(string propertyName, T oldValue, T newValue)
            : base(propertyName)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}
