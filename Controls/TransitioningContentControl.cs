using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace TheLittleBookNest.Controls
{
    public class TransitioningContentControl : ContentControl
    {
        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);

            // Kontrollera om det nya innehållet är ett FrameworkElement
            if (newContent is FrameworkElement newElement)
            {
                // Ställ in TranslateTransform
                var translateTransform = new TranslateTransform();
                newElement.RenderTransform = translateTransform;

                // Hämta huvudfönstret
                var parentWindow = Window.GetWindow(this);
                if (parentWindow?.FindResource("SwipeWithNextViewEffect") is Storyboard storyboard)
                {
                    // Koppla storyboardet till TranslateTransform
                    Storyboard.SetTarget(storyboard, newElement);

                    // Starta storyboard och invänta animationen
                    storyboard.Completed += (s, e) =>
                    {
                        // När animationen är klar, säkerställ att vyn är synlig
                        translateTransform.X = 0;
                    };
                    storyboard.Begin(newElement, true);
                }
            }
        }
    }
}
