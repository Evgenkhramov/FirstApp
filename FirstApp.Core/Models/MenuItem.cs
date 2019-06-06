using System;

namespace FirstApp.Core.Models
{
    public class MenuItem
    {
        public string Title { get; private set; }
        public Type ShowCommand { get; private set; }

        public MenuItem(string title, Type viewModelUrl)
        {
            Title = title;
            ShowCommand = viewModelUrl;
        }
    }
}
