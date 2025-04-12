using LaminadoraIndustrialWebService;
using Microsoft.SqlServer.Server;
using System;

public partial class StoredProcedures
{
    
        [Microsoft.SqlServer.Server.SqlProcedure]
        public static void PostSolicitaFirma(string parametros, string token_fel, string link_fel, out string @ase)
        {

        SqlContext.Pipe.Send("hola");
        try
            {
               // SqlContext.Pipe.Send("ejecutandose");
                SolicitaFirma firma = new SolicitaFirma();
            SqlContext.Pipe.Send(parametros);
            var respuesta = firma.PostFirma(parametros, token_fel, link_fel);

                @ase = respuesta;

                // SqlContext.Pipe.Send(pp.ToString());

                // SqlContext.Pipe.Send("OK");
                //  SqlContext.Pipe.Send(@ase.ToString());
                //return @ase;



            }
            catch (Exception e)
            {

                SqlContext.Pipe.Send(e.ToString());
                @ase = e.ToString();
            }

        }
    }
