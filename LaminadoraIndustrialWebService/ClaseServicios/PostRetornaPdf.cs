using LaminadoraIndustrialWebService;
using Microsoft.SqlServer.Server;
using System;
using System.IO;

public partial class StoredProcedures
{
    [Microsoft.SqlServer.Server.SqlProcedure]


    // public static void PostRetornaPdf(string pdf, out string @outPdf, string _urlFel, string _autorizacion)


    
    //public static void PostRetornaPdf(string pdf, out string @outPdf)
     public static void PostRetornaPdf(string pdf, string _urlFel, string _autorizacion,  out string outPdf)
    {



        try
        {
            SqlContext.Pipe.Send("ejecutandose");
            RetornaPdf retornaPdf = new RetornaPdf();


            var respuesta = retornaPdf.PostRetornaPdf(pdf, _urlFel, _autorizacion);
             
                

            outPdf = "Se ha generado el documento";
            // SqlContext.Pipe.Send(pp.ToString());

            SqlContext.Pipe.Send("OK SE HA GENERADO EL INFORME");
            // return @ase;

            byte[] bytes = Convert.FromBase64String(respuesta);

            System.IO.FileStream stream =
             new FileStream(@"C:\Facturadas\" + pdf + ".pdf", FileMode.CreateNew);
            System.IO.BinaryWriter writer =
                new BinaryWriter(stream);
            writer.Write(bytes, 0, bytes.Length);
            writer.Close();

        }
        catch (Exception e)
        {

            SqlContext.Pipe.Send(e.ToString());
            outPdf = e.ToString();
        }


    }
}
