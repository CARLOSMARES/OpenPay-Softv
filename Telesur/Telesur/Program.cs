using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using static Telesur.openpay;
using System.Text.Json.Nodes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", () => "Bienvenido a la api de CosmoRed");

app.MapGet("/Telesur", () => "Bienvenido a CosmoRed");

bool IsJson(string maybeJson)
{
    try
    {
        var obj = System.Text.Json.JsonSerializer.Deserialize<object>(maybeJson);
        return true;
    }
    catch
    {
        return false;
    }
}

app.MapPost("/Telesur/pago", ([FromBody] object? myJsonResponse) =>
{
    
    File.WriteAllText("./respuesta.json ", myJsonResponse.ToString());

    var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json");

    var configurationm = configuration.Build();

    Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse.ToString());

    string? cadena = configurationm["Connectionstring?s:WebApiDatabase"];

    using (SqlConnection cnn = new SqlConnection("Server=201.162.194.2,10000;Database=NewSoftvWeb;User Id=sa;Password=0601x-2L;"))
    {

        cnn.Open();

        SqlCommand sqlCommand = new SqlCommand("AfectaPagoNotificacionOpenPay", cnn);

        sqlCommand.CommandType = CommandType.StoredProcedure;

        sqlCommand.Parameters.Add("@Clv_Session", SqlDbType.BigInt).Value = myDeserializedClass.Transaction.order_id == null ? 0 : int.Parse(myDeserializedClass.Transaction.order_id);

        sqlCommand.Parameters.Add("@ID", SqlDbType.VarChar).Value = myDeserializedClass.Transaction.id;

        sqlCommand.Parameters.Add("@JsonResponse", SqlDbType.VarChar).Value = myJsonResponse.ToString();

        sqlCommand.Parameters.Add("@type", SqlDbType.VarChar).Value = myDeserializedClass.type;

        sqlCommand.ExecuteNonQuery();

        cnn.Close();

    }

});

app.Run();