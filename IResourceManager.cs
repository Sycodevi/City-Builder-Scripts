public interface IResourceManager
{
    float MoneyCalculationInterval { get; }
    int StartMoneyAmount { get; }
    int removalPrice { get; }

    void CalculateTownIncome();
    void IncreaseMoney(int amount);
    bool Purchasable(int amount);
    bool SpendMoney(int amount);
    int structurePurchasingPower(int placementCost, int count);

    void prepareRM(BuildingManager buildingManager);

    void IncreasePopulation(int value);

    void DecreasePopulation(int value);
    void IncreaseLikes(int value);
    void DecreaseLikes(int value);
}