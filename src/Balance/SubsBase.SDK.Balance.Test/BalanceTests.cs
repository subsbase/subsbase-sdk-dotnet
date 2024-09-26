using FluentAssertions;
using Subsbase.Balance.Inputs;
using SubsBase.SDK.Balance.Contracts;

namespace SubsBase.SDK.Balance.Test;

[NonParallelizable]
[TestFixture]
[SingleThreaded]
public class BalanceTests
{
    private static readonly string _publicKey = "publicKey";
    private static readonly string _privateKey = "privateKey";
    private static Guid _1StBalanceId;
    private static Guid _2NdBalanceId;
    private static string _releasedAmountId;
    private static string _capturedAmountId;
    private static BalanceSdk balanceSdk;

    [OneTimeSetUp]
    public void OneTimeInit()
    {
        balanceSdk = new BalanceSdk(_publicKey, _privateKey, environment: Environment.Development);
    }

    [Test, Order(1)]
    public void Step_01_AddNewBalanceWithoutMetaData_ShouldCreateNewBalance()
    {
        var balance = balanceSdk.Balance.CreateAsync(new BalanceInfoNew()
        {
            Unit = "EGP",
            AllowTotalBalanceToBeNegative = false,
            Metadata = new Dictionary<string, string>()
            {
                {"customerId" , "subsbase_customer1"},
                {"age", "25"}
            },
        }).GetAwaiter().GetResult();
    
        balance.IsSuccess.Should().BeTrue();
        balance.Value.Should().NotBeNull();
        _1StBalanceId = balance.Value!.BalanceId;
    }
    
    [Test, Order(2)]
    public void Step_02_AddNewBalance_ShouldCreateNewBalance()
    {
        var balance = balanceSdk.Balance.CreateAsync(new BalanceInfoNew()
        {
            Unit = "EGP",
            Metadata = new Dictionary<string, string>()
            {
                {"customerId" , "subsbase_customer2"},
                {"age", "35"}
            },
            AllowTotalBalanceToBeNegative = false,
        }).GetAwaiter().GetResult();
    
        balance.IsSuccess.Should().BeTrue();
        balance.Value.Should().NotBeNull();
        _2NdBalanceId = balance.Value!.BalanceId;
    }
    
    [Test, Order(3)]
    public void Step_03_AddCreditBalanceMovement_ShouldLoadBalanceWithSpecificAmount()
    {
        var balance = balanceSdk.BalanceMovement.CreateAsync(new BalanceMovementNew()
        {
            BalanceId = _1StBalanceId,
            Type = MovementType.Credit,
            Amount = 1000,
            Description = "Load Balance With 1000 EGP - settle invoice",
        }).GetAwaiter().GetResult();
    
        balance.IsSuccess.Should().BeTrue();
        balance.Value.Should().NotBeNull();
        balance.Value!.BalanceMovementId.Should().NotBeNullOrEmpty();
        balance.Value.BalanceSummary.AvailableAmount.Should().Be(1000.0M);
        balance.Value.BalanceSummary.TotalAmount.Should().Be(1000.0M);
        balance.Value.BalanceSummary.OnHoldAmount.Should().Be(0.0M);

    }
    
    [Test, Order(4)]
    public void Step_04_HoldBalanceAmountToBeCaptured_ShouldHoldAmount()
    {
        var balance = balanceSdk.OnHoldAmount.CreateAsync(new HoldAmountNew()
        {
            BalanceId = _1StBalanceId,
            Amount = 1000,
            Description = "Hold 1000 EGP",
            ReleaseDate = DateTime.UtcNow.AddSeconds(10)
        }).GetAwaiter().GetResult();
    
        balance.IsSuccess.Should().BeTrue();
        balance.Value.Should().NotBeNull();
        balance.Value!.OnHoldAmountId.Should().NotBeNullOrWhiteSpace();
        _capturedAmountId = balance.Value!.OnHoldAmountId;
    }
    
    [Test, Order(5)]
    public void Step_05_CaptureOnHoldAmount_ShouldCaptureOnHoldAmount()
    {
        var balance = balanceSdk.OnHoldAmount.DeleteAsync(_capturedAmountId, isCaptured: true).GetAwaiter().GetResult();
    
        balance.IsSuccess.Should().BeTrue();
        balance.Value.Should().NotBeNull();
        balance.Value!.AvailableAmount.Should().Be(0.0M);
        balance.Value.TotalAmount.Should().Be(0.0M);
        balance.Value.OnHoldAmount.Should().Be(0.0M);
    }
    
    [Test, Order(6)]
    public void Step_06_AddCreditBalanceMovement_ShouldLoadBalanceWithSpecificAmount()
    {
        var balance = balanceSdk.BalanceMovement.CreateAsync(new BalanceMovementNew()
        {
            BalanceId = _1StBalanceId,
            Type = MovementType.Credit,
            Amount = 1000,
            Description = "Load Balance With 1000 EGP",
        }).GetAwaiter().GetResult();
    
        balance.IsSuccess.Should().BeTrue();
        balance.Value.Should().NotBeNull();
        balance.Value!.BalanceMovementId.Should().NotBeNullOrEmpty();
        balance.Value.BalanceSummary.AvailableAmount.Should().Be(1000.0M);
        balance.Value.BalanceSummary.TotalAmount.Should().Be(1000.0M);
        balance.Value.BalanceSummary.OnHoldAmount.Should().Be(0.0M);
    }
    
    
    
    [Test, Order(7)]
    public void Step_07_AddDebitBalanceMovementWithAmountLargerThanAvailable_ShouldReturnErrorInsufficientFund()
    {
        var balance = balanceSdk.BalanceMovement.CreateAsync(new BalanceMovementNew()
        {
            BalanceId = _1StBalanceId,
            Type = MovementType.Debit,
            Amount = 1500,
            Description = "Unload Balance With 1500 EGP",
        }).GetAwaiter().GetResult();
    
        balance.IsSuccess.Should().BeFalse();
        balance.Value.Should().BeNull();
        balance.Message.Should().NotBeNullOrWhiteSpace();
    }
    
    [Test, Order(8)]
    public void Step_08_AddDebitBalanceMovementWithAmountLessThanAvailable_ShouldUnloadBalanceWithSpecificAmount()
    {
        var balance = balanceSdk.BalanceMovement.CreateAsync(new BalanceMovementNew()
        {
            BalanceId = _1StBalanceId,
            Type = MovementType.Debit,
            Amount = 500,
            Description = "Unload Balance With 500 EGP",
        }).GetAwaiter().GetResult();
    
        balance.IsSuccess.Should().BeTrue();
        balance.Value.Should().NotBeNull();
        balance.Value!.BalanceMovementId.Should().NotBeNullOrEmpty();
        balance.Value.BalanceSummary.AvailableAmount.Should().Be(500.0M);
        balance.Value.BalanceSummary.TotalAmount.Should().Be(500.0M);
        balance.Value.BalanceSummary.OnHoldAmount.Should().Be(0.0M);

    }
    
    [Test, Order(9)]
    public void Step_09_HoldBalanceAmount_ShouldHoldAmount()
    {
        var balance = balanceSdk.OnHoldAmount.CreateAsync(new HoldAmountNew()
        {
            BalanceId = _1StBalanceId,
            Amount = 200,
            Description = "Hold 200 EGP",
        }).GetAwaiter().GetResult();
    
        balance.IsSuccess.Should().BeTrue();
        balance.Value.Should().NotBeNull();
        balance.Value!.OnHoldAmountId.Should().NotBeNullOrWhiteSpace();
        _releasedAmountId = balance.Value!.OnHoldAmountId;
    }
    
    [Test, Order(10)]
    public void Step_10_GetOnHoldAmountWithId_ShouldGetOnHoldAmountDetails()
    {
        var onHoldAmountResult = balanceSdk.OnHoldAmount.GetAsync(_releasedAmountId).GetAwaiter().GetResult();
    
        onHoldAmountResult.IsSuccess.Should().BeTrue();
        onHoldAmountResult.Value.Should().NotBeNull();
        onHoldAmountResult.Value!.BalanceId.Should().Be(_1StBalanceId);
        onHoldAmountResult.Value!.Amount.Should().Be(200.0M);
    }
    
    
    [Test, Order(10)]
    public void Step_10_ReleaseOnHoldAmount_ShouldReleaseOnHoldAmount()
    {
        var balance = balanceSdk.OnHoldAmount.DeleteAsync(_releasedAmountId, isCaptured: false).GetAwaiter().GetResult();
    
        balance.IsSuccess.Should().BeTrue();
        balance.Value.Should().NotBeNull();
        balance.Value!.AvailableAmount.Should().Be(500.0M);
        balance.Value!.TotalAmount.Should().Be(500.0M);
        balance.Value!.OnHoldAmount.Should().Be(0.0M);
    
    }
    
    [Test, Order(11)]
    public void Step_11_HoldBalanceAmountToBeCaptured_ShouldHoldAmount()
    {
        var balance = balanceSdk.OnHoldAmount.CreateAsync(new HoldAmountNew()
        {
            BalanceId = _1StBalanceId,
            Amount = 200,
            Description = "Hold 200 EGP",
            ReleaseDate = DateTime.UtcNow.AddHours(1)
        }).GetAwaiter().GetResult();
    
        balance.IsSuccess.Should().BeTrue();
        balance.Value.Should().NotBeNull();
        balance.Value!.OnHoldAmountId.Should().NotBeNullOrWhiteSpace();
        _capturedAmountId = balance.Value!.OnHoldAmountId;
    }
    
    [Test, Order(12)]
    public void Step_12_CaptureOnHoldAmount_ShouldCaptureOnHoldAmount()
    {
        var balance = balanceSdk.OnHoldAmount.DeleteAsync(_capturedAmountId, isCaptured: true).GetAwaiter().GetResult();
    
        balance.IsSuccess.Should().BeTrue();
        balance.Value.Should().NotBeNull();
        balance.Value!.AvailableAmount.Should().Be(300.0M);
        balance.Value!.TotalAmount.Should().Be(300.0M);
        balance.Value!.OnHoldAmount.Should().Be(0.0M);
    }
    
    [Test, Order(13)]
    public void Step_13_HoldBalanceAmountWithExpirationDate_ShouldHoldAmount()
    {
        var balance = balanceSdk.OnHoldAmount.CreateAsync(new HoldAmountNew()
        {
            BalanceId = _1StBalanceId,
            Amount = 200,
            Description = "Hold 200 EGP",
            ReleaseDate = DateTime.UtcNow.AddDays(1)
        }).GetAwaiter().GetResult();
    
        balance.IsSuccess.Should().BeTrue();
        balance.Value.Should().NotBeNull();
        balance.Value!.OnHoldAmountId.Should().NotBeNullOrWhiteSpace();
        _releasedAmountId = balance.Value!.OnHoldAmountId;
    
        var onHoldAmount = balanceSdk.OnHoldAmount.GetAsync(balance.Value!.OnHoldAmountId).GetAwaiter().GetResult();
        onHoldAmount.IsSuccess.Should().BeTrue();
        onHoldAmount.Value.Should().NotBeNull();
        onHoldAmount.Value.Amount.Should().Be(expected: 200.0M);
        onHoldAmount.Value.ReleaseDate.Should().NotBeNull();
        onHoldAmount.Value.ReleaseDate.Value.Date.Should().Be(DateTime.UtcNow.AddDays(1).Date);
    }
    
    
    [Test, Order(14)]
    public void Step_14_AddCreditBalanceMovementWithExpirationDate_ShouldLoadBalanceWithSpecificAmount()
    {
        var balance = balanceSdk.BalanceMovement.CreateAsync(new BalanceMovementNew()
        {
            BalanceId = _1StBalanceId,
            Type = MovementType.Credit,
            Amount = 1000,
            Description = "Load Balance With 1000 EGP",
            ExpirationDate = DateTime.UtcNow.AddSeconds(5)
        }).GetAwaiter().GetResult();
    
        balance.IsSuccess.Should().BeTrue();
        balance.Value.Should().NotBeNull();
        balance.Value!.BalanceMovementId.Should().NotBeNullOrEmpty();
        balance.Value.BalanceSummary.AvailableAmount.Should().Be(1100.0M);
        balance.Value.BalanceSummary.TotalAmount.Should().Be(1300.0M);
        balance.Value.BalanceSummary.OnHoldAmount.Should().Be(200.0M);

        
    }
    
    [Test, Order(15)]
    public void Step_15_GetBalanceDetails_ShouldGetAllBalanceDetails()
    {
        Thread.Sleep(20 * 1000);
        var balance = balanceSdk.Balance.GetAsync(_1StBalanceId).GetAwaiter().GetResult();
        balance.Should().NotBeNull();
        balance.IsSuccess.Should().BeTrue();
        balance.Value!.BalanceSummary.AvailableAmount.Should().Be(100.0M);
        balance.Value!.BalanceSummary.TotalAmount.Should().Be(300.0M);
        balance.Value!.BalanceSummary.OnHoldAmount.Should().Be(200.0M);
    }
    
    [Test, Order(16)]
    public void Step_16_GetAllBalancesDetails_ShouldGetAllBalances()
    {
        var balance = balanceSdk.Balance.GetAllAsync(new List<string>{_1StBalanceId.ToString(),_2NdBalanceId.ToString()}).GetAwaiter().GetResult();
        balance.Should().NotBeNull();
        balance.IsSuccess.Should().BeTrue();
        balance.Value.Count.Should().Be(expected: 2);
        balance.Value[0].BalanceId.Should().Be(_1StBalanceId);
        balance.Value[0].AvailableAmount.Should().BePositive();
        balance.Value[0].OnHoldAmount.Should().BePositive();
        balance.Value[0].Unit.Should().NotBeNullOrEmpty();
    }
    
    [Test, Order(17)]
    public void Step_17_GetAllBalancesOnHoldAmountsDetails_ShouldGetAllOnHoldAmountsDetails()
    {
        var balance = balanceSdk.OnHoldAmount.GetBalanceOnHoldAmountsAsync(_1StBalanceId).GetAwaiter().GetResult();
        balance.Should().NotBeNull();
        balance.IsSuccess.Should().BeTrue();
        balance.Value.OnHoldAmounts.Should().NotBeEmpty();
        balance.Value.BalanceId.Should().Be(_1StBalanceId);
        balance.Value.OnHoldAmounts.Sum(x => x.Amount).Should().Be(200);
    }
    
    [Test, Order(18)]
    public void Step_18_GetAllBalanceMovements_ShouldGetAlBalanceMovements()
    {
        var balance = balanceSdk.BalanceMovement.GetAsync(
            balanceId: _1StBalanceId,
            filter: new FilterInput(){ SearchTerm = "invoice"},
            sorting: new SortingInput{ SortBy = "amount" , SortDirection = SortingDirection.Descending},
            pagination: new PaginationInput {PageNumber = 1, PageSize = 100}
            ).GetAwaiter().GetResult();
        balance.Should().NotBeNull();
        balance.IsSuccess.Should().BeTrue();
        balance.Value.Should().NotBeNull();

        balance.Value.TotalRecords.Should();
        balance.Value.Data.FirstOrDefault().NetBalance.Should().BePositive();
    }
    
    [Test, Order(19)]
    public void Step_19_AddBalanceMovementWithInvalidTimestamp_ShouldNotAddMovement()
    {
        var balance = balanceSdk.BalanceMovement.CreateAsync(new BalanceMovementNew()
        {
            BalanceId = _1StBalanceId,
            Type = MovementType.Credit,
            Amount = 1000,
            Description = "Load Balance With timestamp - 1000 EGP",
            UtcTimestamp = DateTime.UtcNow.AddDays(-1)
        }).GetAwaiter().GetResult();
    
        balance.IsFailed.Should().BeTrue();
        balance.Value.Should().BeNull();
    }
    
    [Test, Order(20)]
    public void Step_20_AddBalanceMovementWithFutureTimestamp_ShouldNotAddMovement()
    {
        var balance = balanceSdk.BalanceMovement.CreateAsync(new BalanceMovementNew()
        {
            BalanceId = _1StBalanceId,
            Type = MovementType.Credit,
            Amount = 1000,
            Description = "Load Balance With timestamp - 1000 EGP",
            UtcTimestamp = DateTime.UtcNow.AddDays(1)
        }).GetAwaiter().GetResult();
    
        balance.IsFailed.Should().BeTrue();
        balance.Value.Should().BeNull();
    }
}