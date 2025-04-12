using LaminadoraIndustrialWebService;
using Microsoft.SqlServer.Server;
using System;
using System.Xml;

public partial class StoredProcedures
{



    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void PostRegistraFirma(string xmlEnvia, string token_fel, string link_fel, out string @repuestaSp, out string @uuid)
    {


        try
        {
            SqlContext.Pipe.Send("ejecutandose");

            RegistraFirma registrafirma = new RegistraFirma();

            var respuesta = registrafirma.PostRegistraFirma(xmlEnvia, token_fel, link_fel);

            // string pp = respuesta.ToString();


            @repuestaSp = respuesta;


            // SqlContext.Pipe.Send(pp.ToString());

            SqlContext.Pipe.Send("OK  ---- se ha enviado correctamente la firma");

            string a = null;
            XmlDocument xmlRegistra = new XmlDocument();
            string xmlRespuesta = respuesta;

            xmlRegistra.LoadXml(xmlRespuesta);

            XmlNodeList nodeList = xmlRegistra.GetElementsByTagName("uuid");

            foreach (XmlElement ns in nodeList)
            {

                var ab = ns.InnerXml;
                a += ab;
            }


            @uuid = a;
        }
        catch (Exception e)
        {

            SqlContext.Pipe.Send(e.ToString() + "ha sucedido un error a enviar ");
            @repuestaSp = e.ToString();

            @uuid = null;
        }

    }
}
