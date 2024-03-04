using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace CoreAutomation.Interfce
{
    public interface IBrowserHelper
    {
        void Init();
        void Close();
        void ClickOnElement(By by);
        void ClickButtonUsingJavaScriptExecutor(By by);
        void MouseClick(By by);
        void SetTextUsingSendKeys(By by, string text);
        void SetTextUsingAction(By by, string text);
        void SelectElementFromListByValue(By by, string value);
        string GetText(By by);
        string GetAttribute(By by, string attribute);
        string GetScreenshot();
        List<IWebElement> GetListofExistingElements(By by);
        string GetAttributeOfActiveElement(string attribute);
        bool IsElementDisplayed(By by);
        bool IsElementSelected(By by);
        bool IsElementNoLongerVisible(By by);
        bool IsTextPresentInElementLocated(By by, string ExpectedText);
    }
}
