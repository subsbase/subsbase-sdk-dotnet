using System.ComponentModel.Design;
using System.Linq.Expressions;
using System.Reflection;
using SubsBase.SDK.Authentication.Service;
using SubsBase.SDK.Common;
using SubsBase.SDK.Common.Clients;
using SubsBase.SDK.Subscription;

namespace SubsBase.SDK;

public class SubsBaseClient
{
    private readonly AuthenticationService _authService;
    private readonly GraphQLClient _gqlClient;

    public SubsBaseClient(AuthenticationService authService, GraphQLClient gqlClient, Queries query)
    {
        _authService = authService;
        _gqlClient = gqlClient;
        Query = query;
    }

    public Queries Query { get; set; }

    public Mutations Mutate { get; set; }
}


