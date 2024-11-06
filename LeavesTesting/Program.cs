using Acces.Context;
using Acces.Repositories;
using Data.Models;

//string connectionString = "Host=127.0.0.1;Port=5433;Database=Leaves;Username=postgres;Password=Test123";

//LeavesDbContext leavesDbContext = new LeavesDbContext(connectionString);

//AccountRepository accountRepository = new AccountRepository(leavesDbContext);

//foreach(Account account in accountRepository.GetUserAccounts(1))
//{
//    Console.WriteLine(account.PkAccountid);
//}
//Console.WriteLine(leavesDbContext.Accounts.Where(x => x.FkUserid == 1).Count());

HttpRequestMessage httpRequest = new HttpRequestMessage
{
    Method = HttpMethod.Get,
    RequestUri = new Uri("https://localhost:32777/Authentication/")
};
httpRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...");

// Настройка обработчика
HttpClientHandler clientHandler = new HttpClientHandler();
clientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) =>
{
    return sslPolicyErrors == System.Net.Security.SslPolicyErrors.None || cert.Issuer == "CN=localhost";
};

HttpClient client = new HttpClient(clientHandler);
HttpResponseMessage response = await client.SendAsync(httpRequest);
Console.WriteLine(response.StatusCode + " " + response.Headers);

// Pass the handler to httpclient(from you are calling api)
//HttpClient client = new HttpClient();
//HttpResponseMessage response = await client.GetAsync($"https://localhost:7070/Authentication/eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjEiLCJleHAiOjQ4ODYxMzQ5NzZ9.h-wrsuUUmSTgAjDH8uuhxxU8d8mz4nhkBr2__XNj4E4");
//Console.WriteLine(response.Headers);