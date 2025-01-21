namespace FinderBE.Domain;

public interface IGetValues<ModelType>
{
    public Task<List<ModelType>> GetValues();

    public Task<ModelType> GetValue(Guid id);
}
