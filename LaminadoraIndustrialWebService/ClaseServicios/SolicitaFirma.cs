using Microsoft.SqlServer.Server;
using RestSharp;
using System;
using System.IO;
using System.Net;
using System.Xml;

namespace LaminadoraIndustrialWebService
{
    class SolicitaFirma
    {

        public string PostFirma(string xmlEnvia, string token_fel, string link_fel)
        {
            string xmlEnviado = null;

            try
            {

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                // var client = new RestClient("https://dev.api.soluciones-mega.com/api/solicitaFirma");
                var client = new RestClient(link_fel);
                //client.Authenticator = new JwtAuthenticator("eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJzY29wZSI6WyJvcGVuaWQiXSwiZXhwIjoxNjM3MjEwMjQ2LCJhdXRob3JpdGllcyI6WyJST0xFX0VNSVNPUiJdLCJqdGkiOiJiOTE4MzY1OC01MzUzLTQzNzEtYTMxMy01ZDQ3YWNlMmNkNzciLCJjbGllbnRfaWQiOiIyODQxNzkxNyJ9.u1MDo3D1-gOEzZgnmP0R5zmHwKm_a1-pCUII9OkZf0Ejb0yYt14lQhb_dFFweLjyIFT9usRBb8mNTt0HWN0aQ4t0RhXZL2ysOL2phKCewA3Yb1GLcku5I1qekjvCu7YhsH2_oMv38emZ9l_KL8egYeTplijBL1p2AulNTm7USotaDKhv88rzU3DSXt6zQTQ1hXdE-gmj2WkzbTqkk50BKFYFV8G64aioPad8eNbHab0DR2N98GGdWCEHKC5YffbEgn8AWT6YT4Mj0DoVghLMRPLJhRSMvYVLRgvIuCCPX88U59aYugEMVDHPCPKjdZJ0BJA7CqFSumcMM7cLAXcCbQ");

                var request = new RestRequest();
                request.Method = Method.POST;
                request.AddHeader("Accept", "application/xml");
                request.AddHeader("ContentType", "application/xml");
                request.AddHeader("Authorization", "Bearer " + token_fel);
                //request.Parameters.Clear();
                 


                const string xmlTxt = @"<?xml version=""1.0"" encoding=""UTF-8""?>
                                   <FirmaDocumentoRequest id='A50E4C76-6084-11E9-8647-D663BD873D93'>
                                    <xml_dte></xml_dte>
                                       </FirmaDocumentoRequest>";
                TextReader treader = new StringReader(xmlTxt);
                XmlReader xreader = XmlReader.Create(treader);
                XmlDocument xdoc = new XmlDocument();
                xdoc.Load(xreader);

                XmlNode xnode = xdoc.SelectSingleNode("FirmaDocumentoRequest/xml_dte");

                xnode.InnerXml = xmlEnvia;


                request.AddParameter("application/xml;charset=UTF-8", xdoc.InnerXml, ParameterType.RequestBody);


                var response = client.Execute(request);


                // Encoding encoding = Encoding.GetEncoding("ISO-8859-1");

                response.ContentType = "application/xml;charset=UTF-8";
                // response.Charset = "utf-8";

                //SqlContext.Pipe.Send(xdoc.InnerXml);
                 
                string a = null;
                XmlDocument respuesta = new XmlDocument();
                string xmlRespuesta = response.Content;

                respuesta.LoadXml(xmlRespuesta);

                XmlNodeList nodeList = respuesta.GetElementsByTagName("xml_dte");

                foreach (XmlElement ns in nodeList)
                {

                    var ab = ns.InnerXml;
                    a += ab;
                }


                //return response.Content;

                //  return token_fel;
                //SqlContext.Pipe.Send(response.ToString());

               // SqlContext.Pipe.Send("innerxml SOLICITA" + "-" + xnode.InnerXml.ToString());


                //SqlContext.Pipe.Send(a.ToString());

                //SqlContext.Pipe.Send(xmlRespuesta.ToString());



                //return xdoc.InnerXml;
                return a;

                //return ;
            }
            catch (Exception er)
            {
                return er.ToString();

               // return xdoc.InnerXml;
            }

        }
    }
}
