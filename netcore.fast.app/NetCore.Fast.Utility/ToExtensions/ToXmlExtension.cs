using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace NetCore.Fast.Utility.ToExtensions
{
    /// <summary>
    /// XML扩展转换
    /// </summary>
    public static class ToXmlExtension
    {
        /// <summary>
        /// 根据XML字符串转换成对象
        /// <para>方法由类对应XML文档key对应，可使用特性[XmlType(TypeName = "xml")]对 对象重命名</para>
        /// <para>可使用 XmlElement(ElementName = "key") 生成对应XML key的值</para>
        /// </summary>
        /// <typeparam name="T">Type类型</typeparam>
        /// <param name="xml">xml字符串</param>
        /// <returns></returns>
        public static T XmlToObject<T>(this string xml)
        {
            using (StringReader sr = new StringReader(xml))
            {
                var xs = new XmlSerializer(typeof(T));
                return (T)xs.Deserialize(sr);
            }
        }

        /// <summary>
        /// 对象序列化成 xml字符串
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="o">一个对象</param>
        /// <returns></returns>
        public static string ToXmlString<T>(this T o)
        {
            var oxml = new XmlSerializer(typeof(T));
            XmlSerializerNamespaces xm = new XmlSerializerNamespaces();
            xm.Add("", "");
            var xmlStr = new StringBuilder();
            var setting = new XmlWriterSettings
            {
                Encoding = Encoding.UTF8,
                Indent = true,
                OmitXmlDeclaration = true
            };
            using (XmlWriter ms = XmlWriter.Create(xmlStr, setting))
            {
                oxml.Serialize(ms, o, xm);
            }
            return xmlStr.ToString();
        }
    }
}
