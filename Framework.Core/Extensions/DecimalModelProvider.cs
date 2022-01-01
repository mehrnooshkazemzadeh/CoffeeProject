using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Core.Extensions
{
    public class DecimalModelBinderProvider : IModelBinderProvider
    {
      

        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Metadata.ModelType == typeof(decimal))
            {
                return new MyDecimalModelBinder();
            }

            return null;
        }
    }
}
