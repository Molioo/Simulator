using System.Collections.Generic;

public interface ISaveable
{
    Dictionary<string, string> OnSave();

    void OnLoad(Dictionary<string, string> data);

    string UniqueID { get; }

}
