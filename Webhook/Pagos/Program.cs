using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Numerics;
using System.Runtime.Intrinsics;
using System.Text;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
//USAR HASH SHA256
using System.Security.Cryptography;
using Pagos;
using System;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.MapGet("/eii", () => "Servidor EII");

app.MapPost("/eii/webhook", ([FromBody] object? myJsonResponse) =>
{
    try
    {
        //Escribe el Json en un archivo
        File.WriteAllText("./respuesta.json ", myJsonResponse.ToString());
        var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json");
        var configurationm = configuration.Build();

        //Convierte el Json en una clase de C#
        Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse.ToString());

        //Compara las propiedades del Json
        if (myDeserializedClass.type != "verification")
        {
            if (myDeserializedClass.transaction.transaction_type == "charge")//Si es un cargo
            {
                using (SqlConnection cnn = new SqlConnection("Server=192.168.60.89,10000;Database=NewSoftvWeb;User Id=sa;Password=0601x-2L;"))
                {
                    cnn.Open();
                    //Pago con tarjeta
                    if (myDeserializedClass.transaction.payment_method.type == "redirect")
                    {
                        SqlCommand sqlCommand = new SqlCommand("AfectaPagoNotificacionOpenPay", cnn);
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Add("@Clv_Session", SqlDbType.BigInt).Value = myDeserializedClass.transaction.order_id == null ? 0 : int.Parse(myDeserializedClass.transaction.order_id);
                        sqlCommand.Parameters.Add("@ID", SqlDbType.VarChar).Value = myDeserializedClass.transaction.id;
                        sqlCommand.Parameters.Add("@JsonResponse", SqlDbType.VarChar).Value = myJsonResponse.ToString();
                        sqlCommand.Parameters.Add("@type", SqlDbType.VarChar).Value = myDeserializedClass.type;
                        sqlCommand.ExecuteNonQuery();
                        return Results.Ok();
                    }
                    //Pago en tienda
                    if (myDeserializedClass.transaction.payment_method.type == "store")
                    {
                        SqlCommand sqlCommand = new SqlCommand("AfectaPagoNotificacionOpenPayStore", cnn);
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.Add("@Clv_Session", SqlDbType.BigInt).Value = myDeserializedClass.transaction.order_id == null ? 0 : int.Parse(myDeserializedClass.transaction.order_id);
                        sqlCommand.Parameters.Add("@ID", SqlDbType.VarChar).Value = myDeserializedClass.transaction.id;
                        sqlCommand.Parameters.Add("@JsonResponse", SqlDbType.VarChar).Value = myJsonResponse.ToString();
                        sqlCommand.Parameters.Add("@type", SqlDbType.VarChar).Value = myDeserializedClass.type;
                        sqlCommand.Parameters.Add("@barcode", SqlDbType.VarChar).Value = myDeserializedClass.transaction.payment_method.barcode_url;
                        sqlCommand.ExecuteNonQuery();
                        return Results.Ok();
                    }
                    cnn.Close();
                    return Results.Accepted();
                }
            }
            else
            {
                return Results.Conflict();
            }
        }
        else
        {
            return Results.Ok();
        }
    }
    catch
    {
        return Results.NotFound();
    }
});

app.Run();