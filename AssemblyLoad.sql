
 


drop ASSEMBLY WebServicesLaminadora
 drop proc SolicitaFirma
 drop proc RegistraFirma
 drop proc RegistraAnula
 drop proc RetornaPdf
  
CREATE ASSEMBLY WebServicesLaminadora
		--FROM 'E:\DESARROLLO_OUT\LaminadoraIndustrialWebService\LaminadoraIndustrialWebService\bin\Debug\LaminadoraIndustrialWebService.dll' --ruta de la dll 
		FROM 'C:\DATA\DLL LAMINA\DLL LAMINA\LaminadoraIndustrialWebService\bin\Debug\LaminadoraIndustrialWebService.dll'
		WITH PERMISSION_SET = UNSAFE; 
go




CREATE PROCEDURE SolicitaFirma(@body as nvarchar(max),@token as nvarchar(max),@link as nvarchar(max), @response as nvarchar(max) OUTPUT)  --parametros @body xml ya sea factura o anulacion, @response respuesta generada por el ws
WITH EXECUTE AS CALLER 

AS 
	EXTERNAL NAME WebServicesLaminadora.StoredProcedures.PostSolicitaFirma
GO
 
CREATE PROCEDURE RegistraFirma(@body as nvarchar(max),@token as nvarchar(max),@link as nvarchar(max), @response as nvarchar(max) OUTPUT, @uuid as nvarchar(max) OUTPUT)  --parametros @body xml ya sea factura o anulacion, @response respuesta generada por el ws
WITH EXECUTE AS CALLER 

AS 
	EXTERNAL NAME WebServicesLaminadora.StoredProcedures.PostRegistraFirma
	GO

CREATE PROCEDURE RegistraAnula(@body as nvarchar(max),@token as nvarchar(max),@link as nvarchar(max), @response as nvarchar(max) OUTPUT, @uuid as nvarchar(max) OUTPUT)  --parametros @body xml ya sea factura o anulacion, @response respuesta generada por el ws
WITH EXECUTE AS CALLER 

AS 
	EXTERNAL NAME WebServicesLaminadora.StoredProcedures.PostAnulaFactura
	GO
-- public string PostRetornaPdf(string xmlEnvia, string , string autorizacion)
CREATE PROCEDURE RetornaPdf(@body as nvarchar(max),@link as nvarchar(max), @token as nvarchar(max), @response as nvarchar(max) OUTPUT)  --parametros @body xml ya sea factura o anulacion, @response respuesta generada por el ws
WITH EXECUTE AS CALLER 

AS 
	EXTERNAL NAME WebServicesLaminadora.StoredProcedures.PostRetornaPdf
	GO

	


  
/*
NOMBRE DE LA BASE DE DATOS Y COLOCAR LAS UBICACIONES DE LOS ENSAMBLADOS
 */

 EXEC sp_changedbowner 'sa'

 ALTER DATABASE FEL_LAMINADORA_DINAMIC] SET TRUSTWORTHY ON 


EXEC sp_configure 'show advanced options', '1';
GO
RECONFIGURE;
EXEC sp_configure 'clr enabled', '1'
GO
RECONFIGURE;

EXEC sp_configure 'show advanced options', '0';
GO



 


CREATE ASSEMBLY [System.Runtime.Serialization] 
FROM 'C:\Windows\Microsoft.NET\Framework64\v4.0.30319\System.Runtime.Serialization.dll'
WITH PERMISSION_SET = UNSAFE
GO
ALTER DATABASE FEL_LAMINADORA_DINAMIC SET TRUSTWORTHY ON  ---nombre de la base de datos
RECONFIGURE;


CREATE ASSEMBLY [System.Messaging]
FROM 'C:\Windows\Microsoft.NET\Framework64\v4.0.30319\System.Messaging.dll'
WITH PERMISSION_SET = UNSAFE
GO

CREATE ASSEMBLY [System.IdentityModel]
FROM 'C:\Windows\Microsoft.NET\Framework64\v4.0.30319\System.IdentityModel.dll'
WITH PERMISSION_SET = UNSAFE
GO

CREATE ASSEMBLY [System.IdentityModel.Selectors]
FROM 'C:\Windows\Microsoft.NET\Framework64\v4.0.30319\System.IdentityModel.Selectors.dll'
WITH PERMISSION_SET = UNSAFE
GO 
create assembly [System.Web]
from 'C:\Windows\Microsoft.NET\Framework64\v4.0.30319\System.Web.dll'
with permission_set = unsafe;
go

CREATE ASSEMBLY [Microsoft.Transaction.Bridge]
FROM 'C:\Windows\Microsoft.NET\Framework64\v4.0.30319\System.ServiceModel.dll'
WITH PERMISSION_SET = UNSAFE
GO

CREATE ASSEMBLY [Microsoft.Transaction.Bridge]
FROM 'C:\Windows\Microsoft.NET\Framework64\v4.0.30319\System.ServiceModel.dll'
WITH PERMISSION_SET = UNSAFE
GO

 
--CREATE ASSEMBLY [System.ServiceModel] from'C:\Windows\Microsoft.NET\Framework64\v4.0.30319\System.ServiceModel.dll'with permission_set = UNSAFE

