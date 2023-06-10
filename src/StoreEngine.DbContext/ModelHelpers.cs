namespace StoreEngine.DbContext;

using DataClasses;

public static class ModelHelpers
{
    public static MediaTootModel AsModel(this MediaToot mediaToot)
        =>
        new()
        {
            AccountId = mediaToot.AccountId,
            AccountName = mediaToot.AccountName,
            TootId = mediaToot.TootId,
            HasAltText = mediaToot.HasAltText,
            CreatedAt = mediaToot.CreatedAt,
        };

    public static MediaToot AsData(this MediaTootModel model)
        =>
        new(
            model.Id,
            model.AccountId,
            model.AccountName,
            model.TootId,
            model.HasAltText,
            model.CreatedAt
        );

}
