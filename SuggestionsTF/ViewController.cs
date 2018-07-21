using System;
using APRTextField;
using UIKit;

namespace SuggestionsTF
{
    public partial class ViewController : UIViewController,iAPRSuggestionsTextFieldDelegate
    {
        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
        APRTextFieldSuggestions suggestionsProvider1;
        APRTextFieldSuggestions suggestionsProvider2;
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Perform any additional setup after loading the view, typically from a nib.
            string[] theList = {"Alabama",
              "Alaska",
              "Arizona",
              "Arkansas",
              "California",
              "Colorado",
              "Connecticut",
              "District Of Columbia"
            };
            suggestionsProvider1 = new APRTextFieldSuggestions();
            suggestionsProvider1.suggestionRowHeight = 30;
            suggestionsProvider1.suggestionFontSize = 13;
            suggestionsProvider1.initializeSuggestions(txt1, theList);

            suggestionsProvider2 = new APRTextFieldSuggestions();
            suggestionsProvider2.suggestionRowHeight = 50;
            suggestionsProvider2.suggestionFontSize = 16;
            suggestionsProvider2.suggestionsBackgroundColor = UIColor.DarkGray;
            suggestionsProvider2.suggestionTextColor = UIColor.White;
            suggestionsProvider2.initializeSuggestions(txt2, theList);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        public void suggestionTextField_userSelectedItem(int itemIndex, string itemVal)
        {
        
        }
    }
}
