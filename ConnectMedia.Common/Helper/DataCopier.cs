using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConnectMedia.Common
{
    public static class DataCopier
    {
        public static object Copy(object source, object destination)
        {
            if (source != null)
            {

                if (typeof(IEnumerable).IsAssignableFrom(source.GetType()) && !source.GetType().Equals(typeof(string)))
                {

                    if (source.GetType().GetGenericArguments().Count() == 0 && destination.GetType().GetGenericArguments().Count() == 0 && typeof(System.Collections.ArrayList) != destination.GetType())
                    {
                        IEnumerator enumerator = ((IEnumerable)source).GetEnumerator();
                        IEnumerator enumerator1 = ((IEnumerable)destination).GetEnumerator();
                        Type genericType = null;
                        while (enumerator1.MoveNext())
                        {
                            genericType = enumerator1.Current.GetType();
                            break;
                        }
                        int i = 0;
                        while (enumerator.MoveNext())
                        {
                            object des = Activator.CreateInstance(genericType);
                            object src = enumerator.Current;
                            des = Copy(src, des);
                            ((IList)destination)[i] = des;
                            i++;
                        }
                    }
                    else if ((source.GetType().GetGenericArguments().Count() == 1 && destination.GetType().GetGenericArguments().Count() == 1) || typeof(System.Collections.ArrayList) == destination.GetType())
                    {
                        Type genericType = typeof(System.Object);

                        if (destination.GetType().GetGenericArguments().Count() == 1)
                            genericType = destination.GetType().GetGenericArguments().First();
                        IEnumerator enumerator1 = ((IEnumerable)source).GetEnumerator();
                        while (enumerator1.MoveNext())
                        {
                            object des = Activator.CreateInstance(genericType);
                            object src = enumerator1.Current;
                            des = Copy(src, des);
                            destination.GetType().GetMethod("Add").Invoke(destination, new[] { des });
                        }

                    }


                }
                else if (source.GetType().IsInterface || source.GetType().IsClass && !source.GetType().Equals(typeof(string)))
                {
                    traverseObjectProperties(source, destination);
                }
                else
                {
                    if (destination.GetType() == source.GetType())
                    {
                        destination = source;
                    }
                    else if (destination.GetType() == typeof(System.Object))
                    {
                        destination = (object)source;
                    }
                    else
                    {
                        var converter = System.ComponentModel.TypeDescriptor.GetConverter(destination.GetType());
                        try
                        {
                            destination = converter.ConvertFrom(source.ToString());
                        }
                        catch
                        {

                        }
                    }
                }

            }

            return destination;

        }

        private static void traverseObjectProperties(object source, object destination)
        {
            PropertyInfo[] destinationPropertiesArray = destination.GetType().GetProperties();
            PropertyInfo[] sourcePropertiesArray = source.GetType().GetProperties();

            if (destinationPropertiesArray != null && destinationPropertiesArray.Length > 0 && sourcePropertiesArray != null && sourcePropertiesArray.Length > 0)
            {
                foreach (PropertyInfo sourceProperty in sourcePropertiesArray)
                {

                    //if (sourceProperty.GetCustomAttributes(false).Any(x => x is MongoDB.Bson.Serialization.Attributes.BsonIgnoreAttribute)) continue;
                    object sourceObj = sourceProperty.GetValue(source, null);
                    if (sourceObj != null && sourceObj.ToString() != "")
                    {
                        PropertyInfo destinationProperty = destinationPropertiesArray.Where(x => x.Name.Equals(sourceProperty.Name)).FirstOrDefault();

                        if (destinationProperty != null && destinationProperty.PropertyType.Name != "BsonDocument")
                        {
                            object destinationObj = destinationProperty.GetValue(destination, null);
                            if (setObject(destination, sourceProperty, sourceObj, destinationProperty, destinationObj))
                            {
                            }
                            else
                            {
                                setValue(destination, sourceProperty, sourceObj, destinationProperty);
                            }
                        }
                    }

                }
            }
        }

        private static bool setObject(object destination, PropertyInfo sourceProperty, object sourceObj, PropertyInfo destinationProperty, object destinationObj)
        {
            if (typeof(IEnumerable).IsAssignableFrom(sourceProperty.PropertyType) && !sourceProperty.PropertyType.Equals(typeof(string)) && typeof(IEnumerable).IsAssignableFrom(destinationProperty.PropertyType) && !destinationProperty.PropertyType.Equals(typeof(string)))
            {
                if (destinationObj == null)
                {
                    if (destinationProperty.PropertyType.GetConstructors().All(c => c.GetParameters().Length == 0))
                    {

                        var d = Activator.CreateInstance(destinationProperty.PropertyType);
                        destinationProperty.SetValue(destination, d, null);
                        destinationObj = destinationProperty.GetValue(destination, null);
                    }
                    else
                    {
                        int ct = 0;
                        IEnumerator enumerator = ((IEnumerable)sourceObj).GetEnumerator();
                        while (enumerator.MoveNext())
                            ct++;
                        if (destinationProperty.PropertyType.Name.Contains("[]"))
                        {
                            var array = Activator.CreateInstance(destinationProperty.PropertyType, ct);
                            destinationProperty.SetValue(destination, array, null);
                            destinationObj = destinationProperty.GetValue(destination, null);
                        }
                        else
                        {
                            var d = Activator.CreateInstance(destinationProperty.PropertyType);
                            destinationProperty.SetValue(destination, d, null);
                            destinationObj = destinationProperty.GetValue(destination, null);
                        }

                    }
                }
                Copy(sourceObj, destinationObj);
                return true;
            }
            else if (sourceProperty.PropertyType.IsInterface || sourceProperty.PropertyType.IsClass && !sourceProperty.PropertyType.Equals(typeof(string)))
            {
                if (destinationProperty.PropertyType.IsInterface || destinationProperty.PropertyType.IsClass && !destinationProperty.PropertyType.Equals(typeof(string)) && destinationProperty.PropertyType.Name.Equals(sourceProperty.PropertyType.Name))
                {
                    if (destinationObj == null)
                    {
                        if (destinationProperty.PropertyType.GetConstructors().All(c => c.GetParameters().Length == 0))
                        {

                            var d = Activator.CreateInstance(destinationProperty.PropertyType);
                            destinationProperty.SetValue(destination, d, null);
                            destinationObj = destinationProperty.GetValue(destination, null);
                        }
                    }
                    Copy(sourceObj, destinationObj);
                }
                return true;
            }
            else
            {
                return false;
            }

        }

        private static void setValue(object destination, PropertyInfo sourceProperty, object sourceObj, PropertyInfo destinationProperty)
        {
            if (destinationProperty.PropertyType.Name.Equals(sourceProperty.PropertyType.Name))
            {
                destinationProperty.SetValue(destination, sourceObj, null);
            }
            else
            {
                var converter = System.ComponentModel.TypeDescriptor.GetConverter(destinationProperty.PropertyType);
                try
                {
                    var result = converter.ConvertFrom(sourceObj.ToString());
                    destinationProperty.SetValue(destination, result, null);
                }
                catch
                {

                }

            }
        }

        public static object CopyFrom(this object destination, object source)
        {

            return Copy(source, destination);
        }

        public static DateTime WaveMilliSeconds(this DateTime value)
        {
            return value.AddMilliseconds(-(value.Millisecond));
        }

        public static List<PropertyCompareResult> Compare<T>(T oldObject, T newObject)
        {
            string[] listOfNotAddedColumn = new String[] { "IsCashReserveRequirement", "IsStatutoryReserveRequirement", "CreatedOn", "UpdatedOn", "UpdatedBy", "CreatedBy", "IsTenorDate", "IsGeneralHiba", "IsSpecialHiba", "IsPoolObjective", "IsPoolInvestment", "IsPoolRiskCharacteristics", "IsAllocateDepositToDifferentPool", "IsOther", "ApprovalId", "PoolDate" };
            PropertyInfo[] properties = typeof(T).GetProperties();
            List<PropertyCompareResult> result = new List<PropertyCompareResult>();          
            foreach (PropertyInfo pi in properties)
            {
                if (!listOfNotAddedColumn.Contains(pi.Name))
                {


                   


                    if (pi.CustomAttributes.Any(ca => ca.AttributeType == typeof(IgnorePropertyCompareAttribute)))
                    {
                        continue;
                    }
                    object oldValue = pi.GetValue(oldObject), newValue = pi.GetValue(newObject);
                    string columTypeName = string.Empty;
                    // We need to check whether the property is NULLABLE
                    if (pi.PropertyType.IsGenericType && pi.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        // If it is NULLABLE, then get the underlying type.eg if "Nullable<int>" then this will return just "int"
                        columTypeName = pi.PropertyType.GetGenericArguments()[0].Name;
                    }
                    else
                    {
                        columTypeName = pi.PropertyType.Name;
                    }
                    if(columTypeName=="DateTime")
                    {
                        oldValue = oldValue != null ? DateTime.Parse(oldValue.ToString()).ToShortDateString() : oldValue;
                        newValue = newValue != null ? DateTime.Parse(newValue.ToString()).ToShortDateString() : newValue;
                        if (!object.Equals(oldValue, newValue))
                        {
                            Type columnType = pi.PropertyType;


                            result.Add(new PropertyCompareResult(columTypeName, pi.Name, (oldValue), (newValue)));
                        }
                    }
                    else
                    {
                        if (!object.Equals(oldValue, newValue))
                        {
                            Type columnType = pi.PropertyType;


                            result.Add(new PropertyCompareResult(columTypeName, pi.Name, (oldValue), (newValue)));
                        }
                    }
                  
                }
            }

            return result;
        }

        public static void CopyPropertiesTo<T, TU>(this T source, TU dest)
        {

            var sourceProps = typeof(T).GetProperties().Where(x => x.CanRead).ToList();
            var destProps = typeof(TU).GetProperties()
                    .Where(x => x.CanWrite)
                    .ToList();

            foreach (var sourceProp in sourceProps)
            {
                if (destProps.Any(x => x.Name == sourceProp.Name))
                {
                    var p = destProps.First(x => x.Name == sourceProp.Name);
                    p.SetValue(dest, sourceProp.GetValue(source, null), null);
                }

            }

        }
    }
}


class IgnorePropertyCompareAttribute : Attribute { }
public class PropertyCompareResult
{
    public string ColumnName { get; private set; }
    public string Name { get; private set; }
    public object OldValue { get; private set; }
    public object NewValue { get; private set; }

    public PropertyCompareResult(string columnName,  string name, object oldValue, object newValue)
    {
        ColumnName = columnName;
        Name = name;
        OldValue = oldValue;
        NewValue = newValue;
    }
}