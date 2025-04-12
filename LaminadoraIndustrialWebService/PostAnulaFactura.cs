using LaminadoraIndustrialWebService;
using Microsoft.SqlServer.Server;
using System;
using System.Xml;

public partial class StoredProcedures
{
    [Microsoft.SqlServer.Server.SqlProcedure]

    public static void PostAnulaFactura(string xmlEnvia, string token_fel, string link_fel, out string @repuestaSp, out string @uuid)
    {

        try
        {
            SqlContext.Pipe.Send("ANULACION DE FACTURA");

            AnulaFactura anulaFactura = new AnulaFactura();

            var respuesta = anulaFactura.PostAnulaFactura(xmlEnvia, token_fel, link_fel);

            // string pp = respuesta.ToString();


            @repuestaSp = respuesta;


            // SqlContext.Pipe.Send(pp.ToString());

            SqlContext.Pipe.Send("OK  ---- se ha anulado");

            string a = null;
            XmlDocument xmlAnulaFactura = new XmlDocument();
            string xmlRespuesta = respuesta;

            xmlAnulaFactura.LoadXml(xmlRespuesta);

            XmlNodeList nodeList = xmlAnulaFactura.GetElementsByTagName("uuid");

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
