using App172S.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace App172S.Infrastructure
{
    public class InstanceLocator
    {
        public MainViewModel Main { get; set; }
        public InstanceLocator()
        {
            this.Main = MainViewModel.Instance(); //new MainViewModel();
        }
    }
}
