using System.ComponentModel.Design;
using System.Linq.Expressions;
using System.Reflection;
using SubsBase.SDK.Authentication.Service;
using SubsBase.SDK.Common.Clients;

namespace SubsBase.SDK;

public class SubsBaseClient
{
    private readonly AuthenticationService _authService;
    private readonly GraphQLClient _gqlClient;

    public SubsBaseClient(AuthenticationService authService, GraphQLClient gqlClient)
    {
        _authService = authService;
        _gqlClient = gqlClient;
    }

    public Queries Query { get; set; }
    public Mutations Mutate { get; set; }
}


