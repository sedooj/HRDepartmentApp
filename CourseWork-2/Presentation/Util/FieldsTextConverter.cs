using System.Globalization;

namespace CourseWork_2.Presentation.Util
{
    public class EnumTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString() ?? "–";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    
    public class EmptyFieldConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.IsNullOrEmpty(value?.ToString()) ? "–" : value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    
    public class EmptyListTemplateSelector : DataTemplateSelector
    {
        public DataTemplate NonEmptyTemplate { get; set; }
        public DataTemplate EmptyTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is System.Collections.ICollection collection && collection.Count == 0)
            {
                return EmptyTemplate;
            }
            return NonEmptyTemplate;
        }
    }
}