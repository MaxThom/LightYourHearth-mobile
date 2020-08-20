using LightYourHearth.Models;
using LightYourHearth.Services;

using System.Linq;

using Xamarin.Forms;

namespace LightYourHearth.ViewModels
{
    public class EditAnimationViewModel : BaseViewModel
    {
        private ServerService _serverService => DependencyService.Get<ServerService>();
        private LedAnimation ledAnimation { get; set; }

        public EditAnimationViewModel()
        {
            Title = "Edit Animation";
        }

        public LedAnimation GetAnimation(string animationName)
        {
            ledAnimation = _serverService.AnimationCapabilities.Where(x => x.Name.Equals(animationName)).FirstOrDefault();
            return ledAnimation;
        }

        public void UpdateAnimationArgument(string argName, string argValue)
        {
            var arg = ledAnimation.Arguments.Where(x => x.Name.Equals(argName)).FirstOrDefault();
            arg.Value = argValue;
        }
    }
}