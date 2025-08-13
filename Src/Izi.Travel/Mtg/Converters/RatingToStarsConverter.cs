using System;
using Windows.UI.Xaml.Data;

namespace Izi.Travel.Mtg.Converters
{
    public class RatingToStarsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is double rating)
            {
                // Round to nearest 0.5
                double roundedRating = Math.Round(rating * 2, MidpointRounding.AwayFromZero) / 2;
                int fullStars = (int)Math.Floor(roundedRating);
                bool hasHalfStar = (roundedRating - fullStars) > 0.1;

                string stars = new string('★', fullStars);
                if (hasHalfStar)
                {
                    stars += "½";
                }
                
                // Add empty stars if needed (optional)
                // int emptyStars = 5 - fullStars - (hasHalfStar ? 1 : 0);
                // stars += new string('☆', emptyStars);
                
                return stars;
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
