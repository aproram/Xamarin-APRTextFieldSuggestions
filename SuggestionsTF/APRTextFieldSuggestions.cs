using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using UIKit;

namespace APRTextField
{
   
    public interface iAPRSuggestionsTextFieldDelegate
    {
        void suggestionTextField_userSelectedItem(int itemIndex, string itemVal);
    }
    public interface iInnerSuggestionDelegate
    {
        void InnerDelegate_selectedAnItem(int itemIndex);
    }
    public partial class APRTextFieldSuggestions :  iInnerSuggestionDelegate
    {
        UITextField theTF;
        public float suggestionRowHeight = 30;
        public float suggestionFontSize = 13;
        public UIColor suggestionTextColor = UIColor.Gray;

        public float suggestionsMaxVisibleCount = 3.4f;
        public float suggestionsBackgroundAlpha = 0.9f;
        public UIColor suggestionsBackgroundColor = UIColor.White;

        public iAPRSuggestionsTextFieldDelegate myDelegate;
        string[] AllSuggestionsData;
        string[] filteredSuggestionsData;

        UITableView suggestionsTable;

        suggestionTableDelegate assignedDelegate;
        suggestionTableDataSource assignedDataSource;
        public APRTextFieldSuggestions()
        {
           // theTF = this;
        }
        public void initializeSuggestions(UITextField textFieldForSuggestions,string[] suggestions)
        {
            this.AllSuggestionsData = suggestions;
            theTF = textFieldForSuggestions;

            CoreGraphics.CGRect frameThis = theTF.Frame;

            CoreGraphics.CGRect frameForTable = new CoreGraphics.CGRect(frameThis.X, frameThis.Y + 3, frameThis.Width, 0);

            suggestionsTable = new UITableView(frameForTable, UITableViewStyle.Plain);
            suggestionsTable.BackgroundColor = UIColor.White.ColorWithAlpha(suggestionsBackgroundAlpha);
            suggestionsTable.Layer.BorderColor = UIColor.LightGray.ColorWithAlpha((float)0.3).CGColor;
            suggestionsTable.Layer.BorderWidth = 1;

            theTF.EditingChanged += (object sender, EventArgs e) => {

                List<string> list = new List<string>();
                for (int i = 0; i < AllSuggestionsData.Length; i++)
                {

                    string anItem = AllSuggestionsData[i];
                    if (anItem.ToLower().Contains(theTF.Text.ToLower()))
                        list.Add(anItem);
                    /*JObject anItem = (JObject)suggestionsDataArr[i];

                    string itemName = (string)anItem["name"];
                    if (itemName.ToLower().Contains(textName.Text.ToLower()))
                        list.Add(itemName);*/

                }
                string[] toShowSuggestions = list.Distinct().ToArray();

                this.showSuggestions(toShowSuggestions, theTF.Text);
            };
            theTF.EditingDidEnd+=(object sender, EventArgs e) => {
                this.hideSuggestions();
            };


        }
        public void initializeSuggestions(UITextField textFieldForSuggestions,string[] suggestions,iAPRSuggestionsTextFieldDelegate theDelegate)
        {
            this.myDelegate = theDelegate;
            this.initializeSuggestions(textFieldForSuggestions,suggestions);
        }
        private void showSuggestions(string[] suggestions, String stringToHighlight)
        {
            this.filteredSuggestionsData = suggestions;

            CoreGraphics.CGRect frameThis = theTF.Frame;

            float estimatedHeight_ForTable = (float)(Math.Min(suggestionsMaxVisibleCount, suggestions.Length) * suggestionRowHeight + frameThis.Height);
            CoreGraphics.CGRect frameForTable = new CoreGraphics.CGRect(frameThis.X, frameThis.Y+3, frameThis.Width, estimatedHeight_ForTable);
            suggestionsTable.Frame = frameForTable;
            suggestionsTable.EstimatedRowHeight = suggestionRowHeight;
            suggestionsTable.RowHeight = suggestionRowHeight;
            this.suggestionsTable.Hidden = false;


            theTF.Superview.BringSubviewToFront(theTF);
            theTF.Superview.InsertSubviewBelow(suggestionsTable, theTF);
            suggestionsTable.ContentInset = new UIEdgeInsets(theTF.Frame.Height, 0, 0, 0);

            suggestionsTable.Delegate = assignedDelegate = new suggestionTableDelegate(this);
            assignedDataSource = new suggestionTableDataSource(this.filteredSuggestionsData, stringToHighlight);
            assignedDataSource.fSize = suggestionFontSize;
            assignedDataSource.bgAlpha = suggestionsBackgroundAlpha;
            assignedDataSource.bgColor = suggestionsBackgroundColor;
            assignedDataSource.textColor = suggestionTextColor;

            suggestionsTable.DataSource = assignedDataSource;
            suggestionsTable.ReloadData();
            CoreGraphics.CGRect tableFrame = suggestionsTable.Frame;

        }
        public void InnerDelegate_selectedAnItem(int itemIndex)
        {
            string itemVal = filteredSuggestionsData[itemIndex];
            if(this.myDelegate!=null)
                this.myDelegate.suggestionTextField_userSelectedItem(itemIndex, itemVal);

            theTF.Text = itemVal;
            hideSuggestions();

        }
        public void hideSuggestions()
        {
            this.suggestionsTable.Hidden = true;
            this.suggestionsTable.RemoveFromSuperview();
        }

        private class suggestionTableDelegate : UITableViewDelegate
        {
            public iInnerSuggestionDelegate myDelegate;
            public suggestionTableDelegate(iInnerSuggestionDelegate theDelegate)
            {
                this.myDelegate = theDelegate;
            }
            public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
            {
                //base.RowSelected(tableView, indexPath);
                myDelegate.InnerDelegate_selectedAnItem(indexPath.Row);

            }
        }
        private class suggestionTableDataSource : UITableViewDataSource
        {
            string[] SuggestionsData;
            String hightlightText;
            public float fSize;
            public float bgAlpha;
            public UIColor bgColor;
            public UIColor textColor;

            public suggestionTableDataSource(string[] suggestionsData, String textToHighlight)
            {
                this.SuggestionsData = suggestionsData;
                hightlightText = textToHighlight;
            }
            public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
            {
                string CellIdentifier = "cellId";
                UITableViewCell aCell = tableView.DequeueReusableCell(CellIdentifier);
                if (aCell == null)
                {
                    aCell = new UITableViewCell(UITableViewCellStyle.Default, CellIdentifier);
                }
                String plainText = SuggestionsData[indexPath.Row];

                UIStringAttributes stringAttr_F17Reg = new UIStringAttributes();
                stringAttr_F17Reg.Font = UIFont.SystemFontOfSize(fSize);
                stringAttr_F17Reg.ForegroundColor = textColor;

                NSMutableAttributedString attributedSText = new NSMutableAttributedString(plainText, stringAttr_F17Reg);
                aCell.TextLabel.AttributedText = attributedSText;

                NSRange rangeInText = ((NSString)plainText.ToLower()).LocalizedStandardRangeOfString(((NSString)hightlightText.ToLower()));

                if (rangeInText.Location > -1)
                {
                    attributedSText.AddAttribute(UIStringAttributeKey.Font,
                                                 UIFont.BoldSystemFontOfSize(fSize), rangeInText);
                    aCell.TextLabel.AttributedText = attributedSText;
                }
                //aCell.TextLabel.TextColor = textColor;
                //aCell.TextLabel.AttributedText = ;
                //aCell.TextLabel.TextColor = UIColor.DarkGray;
                aCell.BackgroundColor = aCell.ContentView.BackgroundColor = bgColor.ColorWithAlpha(bgAlpha);
                return aCell;
            }

            public override nint RowsInSection(UITableView tableView, nint section)
            {
                return SuggestionsData.Length;
            }
            public override nint NumberOfSections(UITableView tableView)
            {
                return 1;
            }

        }
    }

}
