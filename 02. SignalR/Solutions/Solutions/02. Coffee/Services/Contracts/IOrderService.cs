using _02._Coffee.Models;

namespace _02._Coffee.Services.Contracts;

public interface IOrderService
{
    int NewOrder();

    CheckResult GetUpdate(int orderId);
}
