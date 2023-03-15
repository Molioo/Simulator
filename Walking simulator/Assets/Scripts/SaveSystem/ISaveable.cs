using System.Collections.Generic;

public interface ISaveable
{
    Dictionary<string, object> OnSave();

    void OnLoad(Dictionary<string, object> data);

    string UniqueID { get; }
}
