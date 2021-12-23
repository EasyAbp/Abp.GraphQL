using System;
using GraphQL.Server.Ui.Voyager;

namespace EasyAbp.Abp.GraphQL.Web;

public class AbpVoyagerOptions : VoyagerOptions, ICloneable
{
    public string UiBasicPath = "/ui/Voyager";
    
    public object Clone()
    {
        return MemberwiseClone();
    }
}