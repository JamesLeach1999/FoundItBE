using FinderBE.Models;

namespace FinderBE.Domain;

public interface ICreateValues<ObjectRequest, ModelType>
{
    public ModelType PostUser(ObjectRequest objectRequest);
}
