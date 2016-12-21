using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace UltraClassConvertingTool
{
    /// <summary>
    /// Class for UltraClassConvertingTool extension utility.
    /// </summary>
    public static class ConverterExtension
    {
        #region Static      
            
        #region Methods

        /// <summary>
        /// This method tries to convert any object to generic type T.
        /// Note that method return new object of T.
        /// Method try to copy properties of object into properties of T.
        /// </summary>
        /// <typeparam name="T">Generic typed parameter.</typeparam>
        /// <param name="obj">Object to convert.</param>
        /// <returns>Returns new object of T.</returns>
        public static T To<T>(this Object obj)
        {
            T instance = (T)Activator.CreateInstance(typeof(T));
            Type t = typeof(T);
            PropertyInfo[] propertiesInfo = t.GetRuntimeProperties().ToArray();
            foreach (PropertyInfo propertyInfo in propertiesInfo)
            {
                string propName = propertyInfo.Name;
                Object value = obj._GetPropValue(propName);
                instance._SetPropValue(propName, value);
            }
            return instance;
        }

        /// <summary>
        /// Overload of "To" method.
        /// This method tries to convert any object to generic type T.
        /// Note that method return new object of T.
        /// Method try to copy properties of object into properties of T.
        /// If stictFlag is Stict, then if some property not copied cause
        /// an exception.
        /// </summary>
        /// <typeparam name="T">Generic typed parameter.</typeparam>
        /// <param name="obj">Object to convert.</param>
        /// <param name="stictFlag">Stict flag.</param>
        /// <returns>Returns new object of T.</returns>
        public static T To<T>(this Object obj, StictFlags stictFlag)
        {
            if (stictFlag == StictFlags.Forced)
            {
                return obj.To<T>();
            }
            else // (stictFlag == StictFlags.Stictly)
            {
                T instance = (T)Activator.CreateInstance(typeof(T));
                Type t = typeof(T);
                PropertyInfo[] propertiesInfo = t.GetRuntimeProperties().ToArray();
                foreach (PropertyInfo propertyInfo in propertiesInfo)
                {
                    string propName = propertyInfo.Name;
                    Object value = obj._GetPropValue(propName);
                    bool success = instance._SetPropValue(propName, value);
                    if (success == false)
                    {
                        PropertyNotSettedException ex =
                            new PropertyNotSettedException(propName);
                        throw ex;
                    }
                }
                return instance;
            }            
        }
        #endregion Methods

        #region Utilities

        /// <summary>
        /// Method purposed to get property value by property name via 
        /// reflection.
        /// </summary>
        /// <param name="obj">Object from where we get property.</param>
        /// <param name="name">Property name.</param>
        /// <returns>Propery value if exists and has value, else returns null.
        /// </returns>
        private static Object _GetPropValue(this Object obj, String name)
        {
            foreach (String part in name.Split('.'))
            {
                if (obj == null) return null;
                Type type = obj.GetType();
                PropertyInfo info = type.GetRuntimeProperty(part);
                if (info == null) return null;
                obj = info.GetValue(obj, null);
            }
            return obj;
        }

        /// <summary>
        /// Method purposed to set value by property name via reflection.
        /// </summary>
        /// <param name="obj">Object where we want to set property.</param>
        /// <param name="name">Property name.</param>
        /// <param name="value">Propery value.</param>
        /// <returns>Returns true if value was setted, else returns false.</returns>
        private static bool _SetPropValue(this Object obj, String name, Object value)
        {
            bool fl = false;    
            foreach (String part in name.Split('.'))
            {
                if (obj == null) return false;
                Type type = obj.GetType();
                PropertyInfo info = type.GetRuntimeProperty(part);
                if (info == null) return false;
                if (info.PropertyType == value.GetType())
                {
                    info.SetValue(obj, value);
                    fl = true;                 
                }                
            }
            return fl;            
        }

        #endregion Utilities

        #endregion Static
    }

    /// <summary>
    /// Flags that setting up Converter Methods.
    /// </summary>
    public enum StictFlags
    {
        /// <summary>
        /// Setting this flag causes throwing en exeption if any propety value
        /// wasn't set.
        /// </summary>
        Stictly = 0,
        /// <summary>
        /// Setting of this flag causes ignoring of usetted values.
        /// </summary>
        Forced = 1
    }

    /// <summary>
    /// Derive all custom exceptions of ConverterExtension  class from this 
    /// class.
    /// </summary>
    public class ConverterExtensionException : Exception
    {
        /// <summary>
        /// Some inherited from "Exception" consructors.
        /// </summary>
        public ConverterExtensionException():base()
        {
        }

        /// <summary>
        ///  Some inherited from "Exception" consructors.
        /// </summary>
        /// <param name="message">Error message.</param>
        public ConverterExtensionException(string message)
        : base(message)
        {
        }

        /// <summary>
        /// Some inherited from "Exception" consructors.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <param name="inner">Inner exception.</param>
        public ConverterExtensionException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }

    /// <summary>
    /// Property not setted exception.  
    /// </summary>
    public class PropertyNotSettedException : ConverterExtensionException
    {
        #region Constants

        /// <summary>
        /// Standart error message of exception.
        /// </summary>
        private const string ERROR_MSG = "Propery value wasn't setted.";

        #endregion Constants

        #region Constructors

        /// <summary>
        /// Empty constructor. Standart exception message will be thrown.
        /// </summary>
        public PropertyNotSettedException():base(ERROR_MSG)
        {
        }

        /// <summary>
        /// Parametrised consructor. Exception message with property name
        /// will be thrown.
        /// </summary>
        /// <param name="propertyName"></param>
        public PropertyNotSettedException(string propertyName)
            :base(ERROR_MSG+" Property name:" + propertyName)
        {
        }

        #endregion Constructors
    }
}
