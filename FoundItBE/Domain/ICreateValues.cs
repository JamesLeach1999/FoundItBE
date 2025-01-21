using FoundItBE.Models;

namespace FoundItBE.Domain;

public interface ICreateValues<ObjectRequest, ModelType>
{
    public ModelType PostUser(ObjectRequest objectRequest);
}
