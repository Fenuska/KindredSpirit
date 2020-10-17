using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace KindredSpiritCore
{
    public static class Kindred
    {
        public static List<PropertyDifferences> Differences { get; set; } = new List<PropertyDifferences>();

        /// <summary>
        /// Compares the properties of two objects of the same type and returns if all properties are equal.
        /// </summary>
        /// <param name="itemA">The first object to compare.</param>
        /// <param name="objectB">The second object to compre.</param>
        /// <returns><c>true</c> if all property values are equal, otherwise <c>false</c>.</returns>
        public static bool AreItemsEqual(object itemA, object itemB)
        {
            var result = true;

            if (itemA != null && itemB != null)
            {
                Type objectType;

                objectType = itemA.GetType();
                if (IsPrimitive(objectType))
                    result &= AreValuesEqual(itemA, itemB);
                else if (IsIEnumerableType(objectType))
                    result &= CompareIEnumerableType(itemA, itemB);
                else
                    foreach (PropertyInfo propertyInfo in objectType.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(p => p.CanRead))
                    {
                        var valueA = propertyInfo.GetValue(itemA, null);
                        var valueB = propertyInfo.GetValue(itemB, null);

                        if (IsPrimitive(propertyInfo.PropertyType))
                            result &= AreValuesEqual(valueA, valueB);
                        else if (IsIEnumerableType(propertyInfo))
                        {
                            result &= CompareIEnumerableType(valueA, valueB);
                        }
                        else if (IsClass(propertyInfo))
                        {
                            result &= CompareClass(itemA, itemB, result, propertyInfo);
                        }
                        else
                            result &= false;
                    }
            }
            else
                result &= Equals(itemA, itemB);

            return result;
        }

        private static bool IsClass(PropertyInfo propertyInfo) => propertyInfo.PropertyType.IsClass;

        private static bool CompareClass(object objectA, object objectB, bool result, PropertyInfo propertyInfo)
        {
            if (!AreItemsEqual(propertyInfo.GetValue(objectA, null), propertyInfo.GetValue(objectB, null)))
            {
                result = false;
            }

            return result;
        }

        private static bool CompareIEnumerableType(object valueA, object valueB)
        {
            bool result = true;
            if ((valueA == null && valueB != null) || (valueA != null && valueB == null))
            {
                result = false;
            }
            else if (valueA != null && valueB != null)
            {
                var collectionItems1 = ((IEnumerable)valueA).Cast<object>();
                var collectionItems2 = ((IEnumerable)valueB).Cast<object>();
                var amountOfItems = collectionItems1.Count();
                if (amountOfItems != collectionItems2.Count())
                    result = false;
                else
                    for (int i = 0; i < amountOfItems; i++)
                    {
                        var collectionItem1 = collectionItems1.ElementAt(i);
                        var collectionItem2 = collectionItems2.ElementAt(i);
                        var collectionItemType = collectionItem1.GetType();

                        if (IsPrimitive(collectionItemType))
                        {
                            if (!AreValuesEqual(collectionItem1, collectionItem2))
                                result = false;
                        }
                        else if (!AreItemsEqual(collectionItem1, collectionItem2))
                            result = false;
                    }
            }

            return result;
        }

        private static bool IsIEnumerableType(PropertyInfo propertyInfo)
        {
            return IsIEnumerableType(propertyInfo.PropertyType);
        }

        private static bool IsIEnumerableType(Type type)
        {
            return typeof(IEnumerable).IsAssignableFrom(type);
        }

        /// <summary>
        /// Determines whether value instances of the specified type can be directly compared.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        /// 	<c>true</c> if this value instances of the specified type can be directly compared; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsPrimitive(Type type)
        {
            return typeof(IComparable).IsAssignableFrom(type) || type.IsPrimitive || type.IsValueType;
        }

        /// <summary>
        /// Compares two values and returns if they are the same.
        /// </summary>
        /// <param name="valueA">The first value to compare.</param>
        /// <param name="valueB">The second value to compare.</param>
        /// <returns><c>true</c> if both values match, otherwise <c>false</c>.</returns>
        private static bool AreValuesEqual(object valueA, object valueB)
        {
            var result = true;

            if (valueA == null && valueB != null || valueA != null && valueB == null)
                result = false;
            else if (valueA is IComparable selfValueComparer && selfValueComparer.CompareTo(valueB) != 0)
                result = false;
            else if (!Equals(valueA, valueB))
                result = false;

            return result;
        }
    }
}
