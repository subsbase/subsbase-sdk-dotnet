// using Microsoft.AspNetCore.Mvc;
// using SubsBase.SDK;
//
// namespace SampleApi.Controllers;
//
// [ApiController]
// public class TestController  : ControllerBase
// {
//     private readonly SubsBaseClient _client;
//
//     public TestController(SubsBaseClient client)
//     {
//         _client = client;
//     }
//
//     public async void GetCustomer()
//     {
//         var c = await _client.Query.Customer("asdf")
//             .Select(c => new
//             {
//                 c.Name,
//                 c.EmailAddress,
//                 PaymentMethod= c.PaymentMethod
//                     .Select( p => new
//                     {
//                         Id =  p.Id,
//                         Type = p.Type
//                     })
//             })
//             .ExecuteAsync();
//         //
//         // var mut = await _client.Mutate.Customer("asdf").Update(sdfadf)
//         //     .ExcuteAsync();
//         //
//         //
//         // var cs = await _client.Query.Customers()
//         //     .SortBy(c => c.Name)
//         //     .FilterBy(c => c.Name == adf)
//         //     .ExecuteAsync();
//
//
//     }
// }