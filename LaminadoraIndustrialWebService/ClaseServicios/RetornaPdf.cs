using Microsoft.SqlServer.Server;
using RestSharp;
using System;
using System.Net;
using System.Xml;
namespace LaminadoraIndustrialWebService
{
    class RetornaPdf
    {



        public string PostRetornaPdf(string xmlEnvia, string urlFel, string autorizacion) 
        {
            try
            { 
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                //var client = new RestClient("https://dev2.api.ifacere-fel.com/api/retornarPDF");

                 var client = new RestClient(urlFel);

                //client.Authenticator = new JwtAuthenticator("eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJzY29wZSI6WyJvcGVuaWQiXSwiZXhwIjoxNjM3MjEwMjQ2LCJhdXRob3JpdGllcyI6WyJST0xFX0VNSVNPUiJdLCJqdGkiOiJiOTE4MzY1OC01MzUzLTQzNzEtYTMxMy01ZDQ3YWNlMmNkNzciLCJjbGllbnRfaWQiOiIyODQxNzkxNyJ9.u1MDo3D1-gOEzZgnmP0R5zmHwKm_a1-pCUII9OkZf0Ejb0yYt14lQhb_dFFweLjyIFT9usRBb8mNTt0HWN0aQ4t0RhXZL2ysOL2phKCewA3Yb1GLcku5I1qekjvCu7YhsH2_oMv38emZ9l_KL8egYeTplijBL1p2AulNTm7USotaDKhv88rzU3DSXt6zQTQ1hXdE-gmj2WkzbTqkk50BKFYFV8G64aioPad8eNbHab0DR2N98GGdWCEHKC5YffbEgn8AWT6YT4Mj0DoVghLMRPLJhRSMvYVLRgvIuCCPX88U59aYugEMVDHPCPKjdZJ0BJA7CqFSumcMM7cLAXcCbQ");
                // Copyright (c) 2020 
                // </copyright>
                // <author>Kevin Barrios</author>

                var request = new RestRequest();
                request.Method = Method.POST;
                request.AddHeader("Accept", "application/xml");
                request.AddHeader("ContentType", "application/xml");
                request.AddHeader("Authorization", "Bearer " + autorizacion);// eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJzY29wZSI6WyJvcGVuaWQiXSwiZXhwIjoxNjYwMjc0MzEwLCJhdXRob3JpdGllcyI6WyJST0xFX0VNSVNPUiJdLCJqdGkiOiI5N2RhNTU0Ny05MTAxLTQ0Y2QtOGQxMC1iY2IxNWRjYWUwMWEiLCJjbGllbnRfaWQiOiIxMDM4MjM0NjgifQ.3YTIHhZUrz3RGxD-122J0FJGUazjbfT-kjbpHfR-eBL8b8psjxoL6MH9LIffMCOUFTPyzLscgMVh0nO_BSqr95gbEGXk2sWQ5Bc9q1kw3I4bGar3zcrnSDuZQBjxM8QXWU8MxEx2fIa3DjW4k0tFZ9eXdFQLmzHysBIrHcyInO6VdF7AqrrDBNsEwEgKehJKspNTGIuzAJOclZ0-LTxyEd9ey6uay0GbSd3IjEu6Ait-b8D9MD7gzu_sd_LrJwH02NmVcejXdPy6OFF9J9gMxyKNsoQfFDFIHc1s0wO0Z1TjWZ96P01-ARdifCnGuigNcNoviKFTkgpPpCIrjaAJPg");

                //request.Parameters.Clear();


                //string xmlEnvia2 = "C2C87F61-22D1-4FE1-BB32-E33A356F5700";

                string xmlTxt = @"<?xml version=""1.0"" encoding=""UTF-8""?>
                                         <RetornaPDFRequest><uuid>" + xmlEnvia + @"</uuid>
                                        </RetornaPDFRequest>";

                /*
                string xmlTxt = @"<?xml version=""1.0"" encoding=""UTF-8""?>
                                             <RetornaPDFRequest><uuid>51E9935F-9B89-463B-B23F-B7287A2B4AE7</uuid>
                                             </RetornaPDFRequest>";


                */

                XmlDocument xdoc = new XmlDocument();

                xdoc.LoadXml(xmlTxt);

                request.AddParameter("application/xml;charset=UTF-8", xdoc.InnerXml, ParameterType.RequestBody);

                var response = client.Execute(request);
                response.ContentType = "application/xml;charset=UTF-8";



                string a = null;
                XmlDocument respuesta = new XmlDocument();
                string xmlRespuesta = response.Content;

                respuesta.LoadXml(xmlRespuesta);

                XmlNodeList nodeList = respuesta.GetElementsByTagName("pdf");

                foreach (XmlElement ns in nodeList)
                {

                    var ab = ns.InnerXml;
                    a += ab;
                }

                


            SqlContext.Pipe.Send(xmlTxt);
                return a;
            }
            catch (Exception er)
            {
                return er.ToString();
            }

        }
    }
}
