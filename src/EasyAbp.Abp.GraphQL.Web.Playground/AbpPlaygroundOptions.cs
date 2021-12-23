using System;
using GraphQL.Server.Ui.Playground;

namespace EasyAbp.Abp.GraphQL.Web;

public class AbpPlaygroundOptions : PlaygroundOptions, ICloneable
{
    public string UiBasicPath = "/ui/Playground";
    
    public object Clone()
    {
        return MemberwiseClone();
    }
}