namespace LightYourHearth.Models
{
    public abstract class ConfigurationItem
    {
        public string Id { get; set; }
        public abstract string Text { get; }
        public abstract string Description { get; }

        public abstract void SaveToLocalStorage();

        public abstract void LoadFromLocalStorage();
    }
}