using System;
using System.Reflection;
using System.Resources;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RealWorldApp.Extensions
{
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        private const string ResourceId = "RealWorldApp.Localization.AppResources";
        private static readonly Lazy<ResourceManager> resourceManager = new Lazy<ResourceManager>(() => new ResourceManager(ResourceId, typeof(TranslateExtension).GetTypeInfo().Assembly));

        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
            {
                return string.Empty;
            }

            var ci = Thread.CurrentThread.CurrentCulture;
            var translate = resourceManager.Value.GetString(Text, ci);
            if (translate == null)
            {
                return Text;
            }

            return translate;
        }
    }
}
