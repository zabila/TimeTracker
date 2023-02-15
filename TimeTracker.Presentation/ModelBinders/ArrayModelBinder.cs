using System.ComponentModel;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TimeTracker.Presentation.ModelBinders;

public class ArrayModelBinder : IModelBinder {
    public Task BindModelAsync(ModelBindingContext bindingContext) {
        if (bindingContext is null)
            throw new ArgumentNullException(nameof(bindingContext));

        var modelMetadata = bindingContext.ModelMetadata;
        if (!modelMetadata.IsEnumerableType) {
            bindingContext.Result = ModelBindingResult.Failed();
            return Task.CompletedTask;
        }

        var provideValue = bindingContext.ValueProvider
            .GetValue(bindingContext.ModelName)
            .ToString();

        if (string.IsNullOrEmpty(provideValue)) {
            bindingContext.Result = ModelBindingResult.Success(null);
            return Task.CompletedTask;
        }

        var modelType = bindingContext.ModelType;
        var typeInfo = modelType.GetTypeInfo();
        var genericTypeArguments = typeInfo.GetGenericArguments();
        if (genericTypeArguments.Length < 1) {
            bindingContext.Result = ModelBindingResult.Failed();
            return Task.CompletedTask;
        }

        var genericType = genericTypeArguments[0];
        var converter = TypeDescriptor.GetConverter(genericType);

        var objectArray = provideValue.Split(new[] {
                ","
            }, StringSplitOptions.RemoveEmptyEntries)
            .Select(x => converter.ConvertFromString(x.Trim())).ToArray();

        var guidArray = Array.CreateInstance(genericType, objectArray.Length);
        objectArray.CopyTo(guidArray, 0);
        bindingContext.Model = guidArray;

        bindingContext.Result = ModelBindingResult.Success(bindingContext.Model);
        return Task.CompletedTask;
    }
}