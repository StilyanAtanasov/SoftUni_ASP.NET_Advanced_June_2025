using _02._Coffee.Models;
using _02._Coffee.Services.Contracts;

namespace _02._Coffee.Services;

public class OrderService : IOrderService
{
    private readonly string[] _status =
    {
    "Grinding beans",
    "Steaming milk",
    "Quality control",
    "Delivering...",
    "Picked up"
    };

    private readonly Random _random;
    private IList<int> indexes;

    public OrderService()
    {
        _random = new Random();
        indexes = new List<int>();
    }
    public int NewOrder()
    {
        indexes.Add(0);
        return indexes.Count;
    }

    public CheckResult GetUpdate(int orderId)
    {
        Thread.Sleep(1000);
        int index = indexes[orderId - 1];
        if (_random.Next(0, 4) == 2) // Simulate random delay
        {
            if (_status.Length > indexes[orderId - 1])
            {
                var result = new CheckResult
                {
                    New = true,
                    Update = _status[index],
                    Finished = _status.Length - 1 == index,
                };
                indexes[orderId - 1]++;
                return result;
            }
        }

        return new CheckResult { New = false };
    }
}