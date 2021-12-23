using System;
using GraphQL.Server.Ui.Altair;

namespace EasyAbp.Abp.GraphQL.Web;

public class AbpAltairOptions : AltairOptions, ICloneable
{
    public string UiBasicPath = "/ui/Altair";
    
    public object Clone()
    {
        return MemberwiseClone();
    }
}