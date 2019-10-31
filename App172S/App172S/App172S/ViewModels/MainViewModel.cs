using System;
using System.Collections.Generic;
using System.Text;

namespace App172S.ViewModels
{
    public class MainViewModel
    {
        public LoginViewModel Login { get; set; }
        public SecondViewModel Second { get; set; }
        public MapsPreviousViewModel MapsPrevious { get; set; }
        public MainViewModel()
        {
            //instance = this;
            this.Login = new LoginViewModel();            
            //this.Second = new SecondViewModel();
        }

        private static MainViewModel instance { get; set; }
        public static MainViewModel Instance()
        {
            if (instance == null)
            {
                instance = new MainViewModel();
            }
            return instance;
        }
    }
}
