using DddInPracticeSandbox.UI.Common;
using CustomWindow = DddInPractice.UI.Common.CustomWindow;

namespace DddInPracticeSandbox.UI.Utils
{
    public class DialogService
    {
        public bool? ShowDialog(ViewModel viewModel)
        {
            CustomWindow window = new CustomWindow(viewModel);
            return window.ShowDialog();
        }
    }
}
