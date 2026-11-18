

using ICSharpCode.SharpZipLib.Zip;
using Software.Api.Vendors;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Software.Tests.Vendors;

public class Validations
{
    [Fact]
    public void CheckingAttributesOnVendorCreate()
    {
        /*In .NET API validation, the C# required modifier enforces that a JSON key exists during deserialization 
         * and object construction, whereas [Required] (RequiredAttribute) validates that the parsed property value is not null,
         * empty, or whitespace during model validation.
         */

       // Assert.True(HasAttribute<RequiredAttribute, VendorCreateModel>(nameof(VendorCreateModel.Name)));
        Assert.True(HasAttribute<MinLengthAttribute, VendorCreateModel>(nameof(VendorCreateModel.Name)));
        Assert.True(HasAttribute<MaxLengthAttribute, VendorCreateModel>(nameof(VendorCreateModel.Name)));
        Assert.True(HasAttribute<UrlAttribute, VendorCreateModel>(nameof(VendorCreateModel.Url)));

       
        Assert.Equal(100, GetAttribute<MaxLengthAttribute, VendorCreateModel>(nameof(VendorCreateModel.Name))?.Length);
        Assert.Equal(5, GetAttribute<MinLengthAttribute, VendorCreateModel>(nameof(VendorCreateModel.Name))?.Length);

    }

    public static bool HasAttribute<TAttribute, TDto>(string propName)where TAttribute: ValidationAttribute
    {
        return GetAttribute<TAttribute, TDto>(propName) != null;
    }
    public static TAttribute? GetAttribute<TAttribute, TDto>(string propName) where TAttribute : ValidationAttribute
    {
        var prop = typeof(TDto).GetProperty(propName);
        var allAttributes = prop?.GetCustomAttributes(true);
        if(allAttributes is null)
        {
            return null;
        }
        return (TAttribute?)allAttributes.FirstOrDefault(a => a is TAttribute);
    }
}
