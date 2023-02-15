using CodeHollow.FeedReader.Feeds.Itunes;
using CodeHollow.FeedReader;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Net;

namespace Pitchcast.Scrapper
{
    public static class Extensions
    {
        public static IEnumerable<IEnumerable<T>> Page<T>(this IEnumerable<T> source, int pageSize)
        {
            Contract.Requires(source != null);
            Contract.Requires(pageSize > 0);
            Contract.Ensures(Contract.Result<IEnumerable<IEnumerable<T>>>() != null);

            using IEnumerator<T> enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var currentPage = new List<T>(pageSize)
            {
                enumerator.Current
            };

                while (currentPage.Count < pageSize && enumerator.MoveNext())
                {
                    currentPage.Add(enumerator.Current);
                }
                yield return new ReadOnlyCollection<T>(currentPage);
            }
        }


        public static Episode GetItunesItemWithTitle(this FeedItem item)
        {
            return new Episode(item.SpecificItem.Element);
        }

        //
        // Summary:
        //     Decodes a html encoded string
        //
        // Parameters:
        //   text:
        //     html text
        //
        // Returns:
        //     decoded html
        public static string HtmlDecode(this string text)
        {
            return WebUtility.HtmlDecode(text);
        }

        //
        // Summary:
        //     Determines whether this string and another string object have the same value.
        //
        // Parameters:
        //   text:
        //     the string
        //
        //   compareTo:
        //     the string to compare to
        public static bool EqualsIgnoreCase(this string text, string compareTo)
        {
            return text?.Equals(compareTo, StringComparison.OrdinalIgnoreCase) ?? (compareTo == null);
        }

        //
        // Summary:
        //     Determines whether this string equals one of the given strings.
        //
        // Parameters:
        //   text:
        //     the string
        //
        //   compareTo:
        //     all strings to compare to
        public static bool EqualsIgnoreCase(this string text, params string[] compareTo)
        {
            foreach (string compareTo2 in compareTo)
            {
                if (text.EqualsIgnoreCase(compareTo2))
                {
                    return true;
                }
            }

            return false;
        }

        //
        // Summary:
        //     Converts a string to UTF-8
        //
        // Parameters:
        //   text:
        //     text to convert
        //
        // Returns:
        //     text as utf8 encoded string
        public static string ToUtf8(this string text)
        {
            return Encoding.UTF8.GetString(Encoding.GetEncoding(0).GetBytes(text));
        }

        //
        // Summary:
        //     Converts a string to UTF-8
        //
        // Parameters:
        //   text:
        //     text to convert
        //
        //   encoding:
        //     the encoding of the text
        //
        // Returns:
        //     text as utf8 encoded string
        public static string ToUtf8(this string text, Encoding encoding)
        {
            if (encoding == Encoding.UTF8)
            {
                return text;
            }

            if (encoding == Encoding.GetEncoding(0))
            {
                return text;
            }

            Encoding uTF = Encoding.UTF8;
            byte[] bytes = Encoding.Convert(encoding, uTF, encoding.GetBytes(text));
            return Encoding.UTF8.GetString(bytes);
        }

        //
        // Summary:
        //     Gets the value of an xml element encoded as utf8
        //
        // Parameters:
        //   element:
        //     the xml element
        //
        // Returns:
        //     value of the element utf8 encoded
        public static string GetValue(this XElement element)
        {
            return element?.Value;
        }

        //
        // Summary:
        //     Gets the value of the element "name"
        //
        // Parameters:
        //   element:
        //     xml element
        //
        //   name:
        //     name of the element
        //
        // Returns:
        //     the value of the XElement
        public static string GetValue(this XElement element, string name)
        {
            return element?.GetElement(name).GetValue();
        }

        //
        // Summary:
        //     Gets the value of the element "name"
        //
        // Parameters:
        //   element:
        //     xml element
        //
        //   namespacePrefix:
        //     the namespace prefix of the element that should be returned
        //
        //   name:
        //     name of the element
        //
        // Returns:
        //     the value of the XElement
        public static string GetValue(this XElement element, string namespacePrefix, string name)
        {
            return element?.GetElement(namespacePrefix, name).GetValue();
        }

        //
        // Summary:
        //     Gets the value of the given attribute
        //
        // Parameters:
        //   attribute:
        //     the xml attribute
        //
        // Returns:
        //     value
        public static string GetValue(this XAttribute attribute)
        {
            return attribute?.Value;
        }

        //
        // Summary:
        //     Gets the value of the attribute name
        //
        // Parameters:
        //   element:
        //     the xml element
        //
        //   name:
        //     the name of the attribute
        //
        // Returns:
        //     value of the attribute
        public static string GetAttributeValue(this XElement element, string name)
        {
            return element.GetAttribute(name)?.GetValue();
        }

        //
        // Summary:
        //     Gets the attribute name of the given XElement
        //
        // Parameters:
        //   element:
        //     the xml element
        //
        //   name:
        //     the name of the attribute
        //
        // Returns:
        //     the xml attribute
        public static XAttribute GetAttribute(this XElement element, string name)
        {
            Tuple<string, string> tuple = SplitName(name);
            return element?.GetAttribute(tuple.Item1, tuple.Item2);
        }

        //
        // Summary:
        //     Gets the attribute with the namespace namespacePrefix and name name of the given
        //     XElement
        //
        // Parameters:
        //   element:
        //     the xml element
        //
        //   namespacePrefix:
        //     the namespace prefix of the attribute
        //
        //   name:
        //     the name of the attribute
        //
        // Returns:
        //     the xml attribute
        public static XAttribute GetAttribute(this XElement element, string namespacePrefix, string name)
        {
            if (string.IsNullOrEmpty(namespacePrefix))
            {
                return element.Attribute((XName?)name);
            }

            XNamespace namespacePrefix2 = element.GetNamespacePrefix(namespacePrefix);
            return element.Attribute(namespacePrefix2 + name);
        }

        //
        // Summary:
        //     Gets the element of the given XElement
        //
        // Parameters:
        //   element:
        //     the xml element
        //
        //   name:
        //     Name of the element that should be returned
        //
        // Returns:
        //     the "name" element of the XElement
        public static XElement GetElement(this XElement element, string name)
        {
            Tuple<string, string> tuple = SplitName(name);
            return element?.GetElement(tuple.Item1, tuple.Item2);
        }

        //
        // Summary:
        //     Gets the element of the given XElement
        //
        // Parameters:
        //   element:
        //     the xml element
        //
        //   namespacePrefix:
        //     the namespace prefix of the element that should be returned
        //
        //   name:
        //     Name of the element that should be returned
        //
        // Returns:
        //     the "name" element with the prefix "namespacePrefix" of the XElement
        public static XElement GetElement(this XElement element, string namespacePrefix, string name)
        {
            XNamespace namespacePrefix2 = element.GetNamespacePrefix(namespacePrefix);
            if (namespacePrefix2 == null)
            {
                return null;
            }

            return element.Element(namespacePrefix2 + name);
        }

        //
        // Summary:
        //     Gets all elements of the given XElement
        //
        // Parameters:
        //   element:
        //     the xml element
        //
        //   name:
        //     Name of the elements that should be returned
        //
        // Returns:
        //     all "name" elements of the given XElement
        public static IEnumerable<XElement> GetElements(this XElement element, string name)
        {
            Tuple<string, string> tuple = SplitName(name);
            return element.GetElements(tuple.Item1, tuple.Item2);
        }

        //
        // Summary:
        //     Gets all elements of the given XElement
        //
        // Parameters:
        //   element:
        //     the xml element
        //
        //   namespacePrefix:
        //     the namespace prefix of the elements that should be returned
        //
        //   name:
        //     Name of the elements that should be returned
        //
        // Returns:
        //     all "name" elements of the given XElement
        public static IEnumerable<XElement> GetElements(this XElement element, string namespacePrefix, string name)
        {
            XNamespace namespacePrefix2 = element.GetNamespacePrefix(namespacePrefix);
            if (namespacePrefix2 == null)
            {
                return null;
            }

            return element.Elements(namespacePrefix2 + name);
        }

        //
        // Summary:
        //     Gets the namespace prefix of the given XElement
        //
        // Parameters:
        //   element:
        //     the xml element
        //
        // Returns:
        //     the namespace prefix
        public static XNamespace GetNamespacePrefix(this XElement element)
        {
            return element.GetNamespacePrefix(null);
        }

        //
        // Summary:
        //     Gets the namespace prefix of the given XElement, if namespacePrefix is null or
        //     empty, it returns the default namespace.
        //
        // Parameters:
        //   element:
        //     the xml element
        //
        //   namespacePrefix:
        //     the namespace prefix
        //
        // Returns:
        //     the namespace prefix or default namespace if the namespacePrefix is null or empty
        public static XNamespace GetNamespacePrefix(this XElement element, string namespacePrefix)
        {
            if (!string.IsNullOrWhiteSpace(namespacePrefix))
            {
                return element.GetNamespaceOfPrefix(namespacePrefix);
            }

            return element.GetDefaultNamespace();
        }

        //
        // Summary:
        //     splits the name into namespace and name if it contains a : if it does not contain
        //     a namespace, item1 is null and item2 is the original name
        //
        // Parameters:
        //   name:
        //     the input name
        //
        // Returns:
        //     splitted namespace and name, item1 is null if namespace is empty
        private static Tuple<string, string> SplitName(string name)
        {
            string item = null;
            if (name.Contains(":"))
            {
                int num = name.IndexOf(':');
                item = name.Substring(0, num);
                name = name.Substring(num + 1);
            }

            return new Tuple<string, string>(item, name);
        }
    }
}
