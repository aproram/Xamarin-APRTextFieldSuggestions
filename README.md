# Xamarin-APRTextFieldSuggestions
Xamarin.iOS Autocomplete suggestions for UITextfield in iOS

## Screenshot
![APRTextFieldSuggestions_screenshot](https://github.com/aproram/Xamarin-APRTextFieldSuggestions/raw/master/Xamarin-APRTextFieldSuggestions_screenshot.png)
## Installation
Simply import the file ```APRTextFieldSuggestions.cs``` to your Xamarin.iOS project

## Usage

### In your ViewController

You can setup your suggestions in 4 simple steps inside your view controller 
```c#
//1-Suggestions List To Look At
string[] theList = {"Alabama",
              "Alaska",
              "Arizona",
              "Arkansas",
              "California",
              "Colorado",
              "Connecticut",
              "District Of Columbia"
            };
            
//2-Initialize Suggestions Provider
APRTextFieldSuggestions suggestionsProvider1 = new APRTextFieldSuggestions();

//3-Set Some Optional Styling Properties 
suggestionsProvider1.suggestionRowHeight = 30;
suggestionsProvider1.suggestionFontSize = 13;
suggestionsProvider2.suggestionsBackgroundColor = UIColor.DarkGray;
suggestionsProvider2.suggestionTextColor = UIColor.White;
//4-Add Your Suggestions To Your Textfield
suggestionsProvider1.initializeSuggestions(myTextField, theList);
```
### Public Proberties
```c#
UITextField theTF;
public float suggestionRowHeight = 30;
public float suggestionFontSize = 13;
public UIColor suggestionTextColor = UIColor.Gray;

public float suggestionsMaxVisibleCount = 3.4f;
public float suggestionsBackgroundAlpha = 0.9f;
public UIColor suggestionsBackgroundColor = UIColor.White;

public iAPRSuggestionsTextFieldDelegate myDelegate;
```
### Delegate Methods
```c#
interface iAPRSuggestionsTextFieldDelegate
{
    void suggestionTextField_userSelectedItem(int itemIndex, string itemVal);
}
```

# Author
[Aproram](https://aproram.com) Feel free to contact me for any inquiries or help :)
