using System;
using GraphQL.Server.Ui.GraphiQL;

namespace EasyAbp.Abp.GraphQL.Web;

public class AbpGraphiQLOptions : GraphiQLOptions, ICloneable
{
    public string UiBasicPath = "/ui/graphiql";
    
    public object Clone()
    {
        return MemberwiseClone();
    }
}