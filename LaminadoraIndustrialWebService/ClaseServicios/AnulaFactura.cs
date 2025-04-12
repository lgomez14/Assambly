using RestSharp;
using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml;

namespace LaminadoraIndustrialWebService
{
    class AnulaFactura
    {
        static string convierteCarecteres(string a)
        {

            string b, c, d;

            a = a.Replace("&lt;", "<");
            b = a.Replace("&gt;", ">");
            c = b.Replace("&amp;", "&");
            d = c.Replace("\"", "'");
            const string reduceMultiSpace = @"[ ]{2,}";
            var line = Regex.Replace(d.Replace("\t", " "), reduceMultiSpace, " ");



            return line;
        }

        public string GetXml(string xmlscr)
        {
            string abb = convierteCarecteres(xmlscr);

            return abb;

        }

        public string PostAnulaFactura(string xmlEnvia, string token_fel_anula, string link_fel)
        {
            try
            {
                string a = null;
                XmlDocument respuesta = new XmlDocument();
                string xmlRespuesta = xmlEnvia;

                respuesta.LoadXml(xmlRespuesta);

                XmlNodeList nodeList = respuesta.GetElementsByTagName("xml_dte");

                foreach (XmlElement ns in nodeList)
                {

                    var ab = ns.InnerXml;
                    a += ab;
                }


                string conversion = a;

                //


                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var cliente = new RestClient();


                // cliente.BaseUrl = new Uri("https://apiv2.ifacere-fel.com/api/anularDocumentoXML");

                cliente.BaseUrl = new Uri(link_fel.ToString()); //https://dev2.api.ifacere-fel.com/api/anularDocumentoXML


                var request = new RestRequest();
                request.Method = Method.POST;
                request.AddHeader("Accept", "application/xml");
                request.AddHeader("ContentType", "application/xml");
                request.AddHeader("Authorization", "Bearer " + token_fel_anula);




                const string xmlTxt = @"<?xml version=""1.0"" encoding=""UTF-8""?>
                                   <AnulaDocumentoXMLRequest id='A50E4C76-6084-11E9-8647-D663BD873D93'>
                                    <xml_dte></xml_dte>
                                    </AnulaDocumentoXMLRequest>";
                TextReader treader = new StringReader(xmlTxt);
                XmlReader xreader = XmlReader.Create(treader);
                XmlDocument xdoc = new XmlDocument();
                xdoc.Load(xreader);

                XmlNode xnode = xdoc.SelectSingleNode("AnulaDocumentoXMLRequest/xml_dte");


                //    xnode.InnerXml = conversion;
                xnode.InnerXml = "<![CDATA[" + xmlEnvia + "]]>"; ;

                request.AddParameter("application/xml", xdoc.InnerXml, ParameterType.RequestBody);


                var response = cliente.Execute(request);




                return response.Content;


            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }



    }
}
