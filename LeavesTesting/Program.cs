using Acces.Context;
using Acces.Repositories;
using Data.Models;

HttpRequestMessage httpRequest = new HttpRequestMessage
{
    Method = HttpMethod.Get,
    RequestUri = new Uri("http://localhost:32774/Authentication")
};
httpRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjEiLCJleHAiOjQ4ODYxMzQ5NzZ9.h-wrsuUUmSTgAjDH8uuhxxU8d8mz4nhkBr2__XNj4E4");



HttpClient client = new HttpClient(/*clientHandler*/);
HttpResponseMessage response = await client.SendAsync(httpRequest);
Console.WriteLine(response.StatusCode);

Console.ReadLine();

////Настройка обработчика
//HttpClientHandler clientHandler = new HttpClientHandler();
//clientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) =>
//{
//    return sslPolicyErrors == System.Net.Security.SslPolicyErrors.None || cert.Issuer == "CN=localhost";
//};

