using EasyAbp.Abp.GraphQL.Authors.Dtos;
using EasyAbp.Abp.GraphQL.Books.Dtos;
using EasyAbp.Abp.GraphQL.Citys.Dtos;
using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.Abp.GraphQL;

/* The demo CRUD app services use the DTO type as their entity, so ABP maps
 * each DTO onto itself. Mapperly requires these self-maps to be defined
 * explicitly (AutoMapper created them implicitly). */

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class AuthorDtoToAuthorDtoMapper : MapperBase<AuthorDto, AuthorDto>
{
    public override partial AuthorDto Map(AuthorDto source);
    public override partial void Map(AuthorDto source, AuthorDto destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class BookDtoToBookDtoMapper : MapperBase<BookDto, BookDto>
{
    public override partial BookDto Map(BookDto source);
    public override partial void Map(BookDto source, BookDto destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
[MapExtraProperties(DefinitionChecks = MappingPropertyDefinitionChecks.None)]
public partial class CityDtoToCityDtoMapper : MapperBase<CityDto, CityDto>
{
    /* ExtraProperties has a protected setter Mapperly cannot write; MapExtraProperties copies it instead.
     * The demo data sets extra properties without declaring them on the ObjectExtensionManager,
     * so definition checks have to be disabled to copy them all. */
    [MapperIgnoreTarget(nameof(CityDto.ExtraProperties))]
    public override partial CityDto Map(CityDto source);

    [MapperIgnoreTarget(nameof(CityDto.ExtraProperties))]
    public override partial void Map(CityDto source, CityDto destination);
}
