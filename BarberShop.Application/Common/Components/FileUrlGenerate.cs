using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BarberShop.Application.Common.Components
{
    public static partial class Extension
    {
        //public const string Root = "api";
        //public const string Version = "v1";
        //public const string Base = Root + "/" + Version;
        //private static string baseUrl;
        //public const string Getphoto = Base + "/file/GetPhoto/{cyrptedPhoto}";

        public const string Root = "api";
        //public const string Version = "v1";
        public const string Base = Root;
        private static string baseUrl;
        public const string Getphoto = Base + "/File?cyrptedPhoto={cyrptedPhoto}";
        public static string GeneratePhotoUrl(this IHttpContextAccessor httpContextAccessor,int photoId)
        {
            baseUrl = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host.ToUriComponent()}";
            if (photoId == 0)
            {
                return string.Empty;
            }
            return baseUrl + "/" + Getphoto.Replace("{cyrptedPhoto}", HttpUtility.UrlEncode(CryptHelper.Encrypt($"{photoId}")));
        }
    }
}
