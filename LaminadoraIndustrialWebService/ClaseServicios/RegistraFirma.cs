
using RestSharp;
using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml;

namespace LaminadoraIndustrialWebService
{
    class RegistraFirma
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






        public string PostRegistraFirma(string xmlEnvia, string token_fel, string link_fel)
        {
            string a = null;

            try
            {



                XmlDocument respuesta = new XmlDocument();
                string xmlRespuesta = xmlEnvia;

                respuesta.LoadXml(xmlRespuesta);

                XmlNodeList nodeList = respuesta.GetElementsByTagName("xml_dte");

                foreach (XmlElement ns in nodeList)
                {

                    var ab = ns.InnerXml;
                    a += ab;
                }




                //string conversion = "<![CDATA[" + a + "]]>";


                string conversion = a;



                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;



                var cliente = new RestClient();


                //cliente.BaseUrl = new Uri("https://apiv2.ifacere-fel.com/api/registrarDocumentoXML");

                // cliente.BaseUrl = new Uri("https://dev2.api.ifacere-fel.com/api/registrarDocumentoXML");
                cliente.BaseUrl = new Uri(link_fel);
                //

                /*  cliente.Authenticator = new HttpBasicAuthenticator("28417917", "zzaqHTr7U864qHrqah23rYP");
                  cliente.Authenticator = new JwtAuthenticator("eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJzY29wZSI6WyJvcGVuaWQiXSwiZXhwIjoxNjQ2NzE5NDU5LCJhdXRob3JpdGllcyI6WyJST0xFX0VNSVNPUiJdLCJqdGkiOiJiYTkyZmU3Ny0xMGFiLTQ5ZDQtODc2Mi0yZjRjNjlkMmM1YTQiLCJjbGllbnRfaWQiOiIyODQxNzkxNyJ9.DeEIQO3uQXhd0iltQLn1lXMBbSbaDLaDh_4uru6YQ3e7vAlPH0WzRZL5LN30pmOUQmDVDtAWnsO3JembTQagbO_UNmNyPdKFpEWbP85cDb8EZgz2p82XX0W33IValWnaagHsbMx3TsQLjkQW9Zg6irakIayrfLB4HoBHFFYy3Ah2w3NW7n170H6d9EJWMFs295actQelHtRRytCkcVP8v2UIvIByhkPCmcSZx9vDdYAz1zpifhoNL9JhhBRr4X0XFFCr3BqiJ_JiOIj7u6plZZb2X44XHr3Lx16eb0nWDiSOuGQH0dSO2QyZqQHC0W6TIkXXzRvXOYyyd_HjxX7maA");
                */

                /*
                var request = new RestRequest();
                request.Method = Method.POST;
                request.AddHeader("Accept", "application/xml");
                request.AddHeader("ContentType", "application/xml");
                request.AddHeader("Authorization", "Bearer eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJzY29wZSI6WyJvcGVuaWQiXSwiZXhwIjoxNjQ2NzE5NDU5LCJhdXRob3JpdGllcyI6WyJST0xFX0VNSVNPUiJdLCJqdGkiOiJiYTkyZmU3Ny0xMGFiLTQ5ZDQtODc2Mi0yZjRjNjlkMmM1YTQiLCJjbGllbnRfaWQiOiIyODQxNzkxNyJ9.DeEIQO3uQXhd0iltQLn1lXMBbSbaDLaDh_4uru6YQ3e7vAlPH0WzRZL5LN30pmOUQmDVDtAWnsO3JembTQagbO_UNmNyPdKFpEWbP85cDb8EZgz2p82XX0W33IValWnaagHsbMx3TsQLjkQW9Zg6irakIayrfLB4HoBHFFYy3Ah2w3NW7n170H6d9EJWMFs295actQelHtRRytCkcVP8v2UIvIByhkPCmcSZx9vDdYAz1zpifhoNL9JhhBRr4X0XFFCr3BqiJ_JiOIj7u6plZZb2X44XHr3Lx16eb0nWDiSOuGQH0dSO2QyZqQHC0W6TIkXXzRvXOYyyd_HjxX7maA");
                
                // Copyright (c) 2020 
                // </copyright>
                // <author>Kevin Barrios</author>
                 
                 
                 */

                var request = new RestRequest();
                request.Method = Method.POST;
                request.AddHeader("Accept", "application/xml");
                request.AddHeader("ContentType", "application/xml");
                // request.AddHeader("Authorization", "Bearer eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJzY29wZSI6WyJvcGVuaWQiXSwiZXhwIjoxNjU4NjMwMDUwLCJhdXRob3JpdGllcyI6WyJST0xFX0VNSVNPUiJdLCJqdGkiOiI4ZDg5YWY2OC0wMzRlLTQ3ZjEtODcyNi0xNjY1ZTQ5ZTFhZjEiLCJjbGllbnRfaWQiOiIxMDYzMDA1NTUifQ.Pmko8uBuCzquCv4uqXaSBykKWMewO6CrE0RZMevwV5YksfgLBcHkmao-1YrjOdk9umlvvrpPPLt4StTywtRVrSBMUDt5U_UpLwH1HxaYZuPGw-FSgSBMojr-FWC1uAOekDaLMMs4-dsppYMh1URPdl9w46PZakf972daeJrq-u7ZJSeqEmWVtXTnXM5OkpcQceZ2tZUnjvbgYO-2dQKCDZtgJ8x4AbHmyojsEc2zHQPWFc5gAW9gVMSENObJZg7bNbyixM6Khnf6ZquDVfLLEs_LqybFxQJVUhlNBTEGBV1JuiK4d6cH_fri-lYkGcpnPScvZeV29jy7siGQH3zKGw");
                request.AddHeader("Authorization", "Bearer " + token_fel);

                const string xmlTxt = @"<?xml version=""1.0"" encoding=""UTF-8""?>
                                   <RegistraDocumentoXMLRequest id='A50E4C76-6084-11E9-8647-D663BD873D93'>
                                    <xml_dte></xml_dte>
                                    </RegistraDocumentoXMLRequest>";
                TextReader treader = new StringReader(xmlTxt);
                XmlReader xreader = XmlReader.Create(treader);
                XmlDocument xdoc = new XmlDocument();
                xdoc.Load(xreader);

                XmlNode xnode = xdoc.SelectSingleNode("RegistraDocumentoXMLRequest/xml_dte");


                //    xnode.InnerXml = conversion;
                xnode.InnerXml = "<![CDATA[" + xmlEnvia + "]]>"; ;

                request.AddParameter("application/xml", xdoc.InnerXml, ParameterType.RequestBody);


                var response = cliente.Execute(request);




                return response.Content;




                // return xdoc.InnerXml;
                //   return conversion;

            }

            catch (Exception e)
            {
                return e.ToString();
            }



        }
    }
}
