using System.ComponentModel;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TimeTracker.Presentation.ModelBinders;

public class ArrayModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (!bindingContext.ModelMetadata.IsEnumerableType)
        {
            bindingContext.Result = ModelBindingResult.Failed();
            return Task.CompletedTask;
        }

        var provideValue = bindingContext.ValueProvider
            .GetValue(bindingContext.ModelName)
            .ToString();

        if (string.IsNullOrEmpty(provideValue))
        {
            bindingContext.Result = ModelBindingResult.Success(null);
            return Task.CompletedTask;
        }

        if (bindingContext.ModelType.GetTypeInfo().GenericTypeArguments.Length < 1)
        {
            bindingContext.Result = ModelBindingResult.Failed();
            return Task.CompletedTask;
        }

        var genericType = bindingContext.ModelType.GetTypeInfo().GetGenericArguments()[0];
        var converter = TypeDescriptor.GetConverter(genericType);

        var objectArray = provideValue.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
            .Select(x => converter.ConvertFromString(x.Trim())).ToArray();

        var guidArray = Array.CreateInstance(genericType, provideValue.Length);
        objectArray.CopyTo(guidArray, 0);
        bindingContext.Model = guidArray;

        bindingContext.Result = ModelBindingResult.Success(bindingContext.Model);
        return Task.CompletedTask;
    }
}