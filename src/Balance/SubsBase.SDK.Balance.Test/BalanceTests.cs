using SubsBase.SDK.Balance.Contracts;
using FluentAssertions;

namespace SubsBase.SDK.Balance.Test;

[NonParallelizable]
[TestFixture]
[SingleThreaded]
public class BalanceTests
{
    private static readonly string _publicKey = "F68B22FC-E444-4296-B415-D22434DD8061";
    private static readonly string _privateKey = "nDCH601zDitO6gNRnDXhYLu0NN99CJbg";
    private static Guid _balanceId;
    private static string _releasedAmountId;
    private static string _capturedAmountId;


    [Test, Order(1)]
    public void Step_01_AddNewBalanceWithoutMetaData_ShouldCreateNewBalance()
    {
        var balanceSdk = new BalanceSdk(_publicKey, _privateKey);
        var balance = balanceSdk.Balance.CreateAsync(new BalanceInfoNew()
        {
            Unit = "EGP",
            AllowTotalBalanceToBeNegative = false,
        }).GetAwaiter().GetResult();

        balance.IsSuccess.Should().BeTrue();
        balance.Value.Should().NotBeNull();
        //_balanceId = balance.Value!.BalanceId;
    }

    [Test, Order(1)]
    public void Step_01_AddNewBalance_ShouldCreateNewBalance()
    {
        var balanceSdk = new BalanceSdk(_publicKey, _privateKey);
        var balance = balanceSdk.Balance.CreateAsync(new BalanceInfoNew()
        {
            Unit = "EGP",
            Metadata = new Dictionary<string, string>()
            {
                {"customerId" , "shaker_customer"},
                {"age", "25"}
            },
            AllowTotalBalanceToBeNegative = false,
        }).GetAwaiter().GetResult();

        balance.IsSuccess.Should().BeTrue();
        balance.Value.Should().NotBeNull();
        _balanceId = balance.Value!.BalanceId;
    }

    [Test, Order(2)]
    public void Step_02_AddCreditBalanceMovement_ShouldLoadBalanceWithSpecificAmount()
    {
        var balanceSdk = new BalanceSdk(_publicKey, _privateKey);
        var balance = balanceSdk.BalanceMovement.CreateAsync(new BalanceMovementNew()
        {
            BalanceId = _balanceId,
            Type = MovementType.Credit,
            Amount = 1000,
            Description = "Load Balance With 1000 EGP",
        }).GetAwaiter().GetResult();

        balance.IsSuccess.Should().BeTrue();
        balance.Value.Should().NotBeNull();
        balance.Value!.AvailableAmount = 1000.0M;
        balance.Value.TotalAmount = 1000.0M;
        balance.Value.OnHoldAmount = 0.0M;
    }
    
    
    [Test, Order(3)]
    public void Step_03_AddDebitBalanceMovementWithAmountLargerThanAvailable_ShouldReturnErrorInsufficientFund()
    {
        var balanceSdk = new BalanceSdk(_publicKey, _privateKey);
        var balance = balanceSdk.BalanceMovement.CreateAsync(new BalanceMovementNew()
        {
            BalanceId = _balanceId,
            Type = MovementType.Debit,
            Amount = 1500,
            Description = "Unload Balance With 1500 EGP",
        }).GetAwaiter().GetResult();

        balance.IsSuccess.Should().BeFalse();
        balance.Value.Should().BeNull();
    }
    
    [Test, Order(4)]
    public void Step_04_AddDebitBalanceMovementWithAmountLessThanAvailable_ShouldUnloadBalanceWithSpecificAmount()
    {
        var balanceSdk = new BalanceSdk(_publicKey, _privateKey);
        var balance = balanceSdk.BalanceMovement.CreateAsync(new BalanceMovementNew()
        {
            BalanceId = _balanceId,
            Type = MovementType.Debit,
            Amount = 500,
            Description = "Unload Balance With 500 EGP",
        }).GetAwaiter().GetResult();

        balance.IsSuccess.Should().BeTrue();
        balance.Value.Should().NotBeNull();
        balance.Value!.AvailableAmount = 500.0M;
        balance.Value.TotalAmount = 500.0M;
        balance.Value.OnHoldAmount = 0.0M;
    }
    
    [Test, Order(5)]
    public void Step_05_HoldBalanceAmount_ShouldHoldAmount()
    {
        var balanceSdk = new BalanceSdk(_publicKey, _privateKey);
        var balance = balanceSdk.OnHoldAmount.CreateAsync(new HoldAmountNew()
        {
            BalanceId = _balanceId,
            Amount = 200,
            Description = "Hold 200 EGP",
        }).GetAwaiter().GetResult();

        balance.IsSuccess.Should().BeTrue();
        balance.Value.Should().NotBeNull();
        balance.Value!.OnHoldAmountId.Should().NotBeNullOrWhiteSpace();
        _releasedAmountId = balance.Value!.OnHoldAmountId;
    }
    
    [Test, Order(6)]
    public void Step_06_ReleaseOnHoldAmount_ShouldReleaseOnHoldAmount()
    {
        var balanceSdk = new BalanceSdk(_publicKey, _privateKey);
        var balance = balanceSdk.OnHoldAmount.DeleteAsync(_releasedAmountId, isCaptured: false).GetAwaiter().GetResult();

        balance.IsSuccess.Should().BeTrue();
        balance.Value.Should().NotBeNull();
        balance.Value!.AvailableAmount = 500.0M;
        balance.Value.TotalAmount = 500.0M;
        balance.Value.OnHoldAmount = 0.0M;

    }
    [Test, Order(7)]
    public void Step_07_HoldBalanceAmountWithExpirationDate_ShouldHoldAmount()
    {
        var balanceSdk = new BalanceSdk(_publicKey, _privateKey);
        var balance = balanceSdk.OnHoldAmount.CreateAsync(new HoldAmountNew()
        {
            BalanceId = _balanceId,
            Amount = 200,
            Description = "Hold 200 EGP",
            ExpirationDate = DateTime.UtcNow.AddDays(1)
        }).GetAwaiter().GetResult();

        balance.IsSuccess.Should().BeTrue();
        balance.Value.Should().NotBeNull();
        balance.Value!.OnHoldAmountId.Should().NotBeNullOrWhiteSpace();
        _releasedAmountId = balance.Value!.OnHoldAmountId;
    }
    
    [Test, Order(8)]
    public void Step_08_AddCreditBalanceMovementWithExpirationDate_ShouldLoadBalanceWithSpecificAmount()
    {
        var balanceSdk = new BalanceSdk(_publicKey, _privateKey);
        var balance = balanceSdk.BalanceMovement.CreateAsync(new BalanceMovementNew()
        {
            BalanceId = _balanceId,
            Type = MovementType.Credit,
            Amount = 1000,
            Description = "Load Balance With 1000 EGP",
            ExpirationDate = DateTime.UtcNow.AddDays(30)
        }).GetAwaiter().GetResult();

        balance.IsSuccess.Should().BeTrue();
        balance.Value.Should().NotBeNull();
        balance.Value!.AvailableAmount = 1300.0M;
        balance.Value.TotalAmount = 1500.0M;
        balance.Value.OnHoldAmount = 200.0M;
    }




}