namespace LightYourHearth.ViewModels
{
    public class EditAnimationViewModel : BaseViewModel
    {

        private string animation = string.Empty;

        public string Animation
        {
            get { return animation; }
            set { SetProperty(ref animation, value); }
        }

        public EditAnimationViewModel()
        {
            Title = "Edit Animation";
        }
    }
}