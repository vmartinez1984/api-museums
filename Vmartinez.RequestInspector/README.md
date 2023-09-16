# Inspector de Request y Response

Coloquese la cadena de conexión en el segmento connectionStrings

## Tabla donde se registraran los datos
''' sql
CREATE TABLE [HttpContext] (
    [Id] int NOT NULL IDENTITY,
    [Application] nvarchar(255) NULL,
    [Path] nvarchar(max) NULL,
    [Method] nvarchar(max) NULL,
    [RequestHeader] nvarchar(max) NULL,
    [RequestBody] nvarchar(max) NULL,
    [ResponseHeader] nvarchar(max) NULL,
    [ResponseBody] nvarchar(max) NULL,
    [RequestDateRegistration] datetime2 NOT NULL,
    [ResponseDateRegistration] datetime2 NOT NULL,
    CONSTRAINT [PK_HttpContext] PRIMARY KEY ([Id])
);
'''
## Coloque la cadena de conexión
´´´json
"ApplicationName": "Nombre de la app",
"ConnectionStrings": {
    "MyConnection": "...",
    "RequestInspector": "Server=.;Database=RequestInspector;User Id=sa;Password=123456; TrustServerCertificate=True"
  },
´´´

## Agregue

builder.Services.AddRequestInpector();
app.UseMiddleware<RequestInspectorMiddleware>();
