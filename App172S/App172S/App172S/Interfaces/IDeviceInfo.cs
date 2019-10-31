using System;
using System.Collections.Generic;
using System.Text;

namespace App172S.Interfaces
{
    public interface IDeviceInfo
    {
        string GetImei();
        void ValidateLocatorPermission();
        void ValidateMapsPermission();
    }
}
