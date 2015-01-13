using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Markup;
using System.Threading.Tasks;

namespace Hearthstone_Deck_Tracker.Utility
{
    /// <summary>
    /// Language helper
    /// </summary>
    public class LangHelper
    {
        private static List<System.Windows.ResourceDictionary> _Resourcelist = new List<ResourceDictionary>();

        public static void LoadResource(string languageName)
        {
            var currentResourceDictionary = (from d in _Resourcelist
                                             where d.ToString().Equals(languageName)
                                             select d).FirstOrDefault();

            if (currentResourceDictionary == null)
            {
                string appPath = AppDomain.CurrentDomain.BaseDirectory;
                string langType = appPath + string.Format(@"/Langs/Language.{0}.xaml", languageName); // Language.zh-CN.xaml
                // App.Current.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = new Uri(langType, UriKind.RelativeOrAbsolute) });
                var resourceDic = new ResourceDictionary() { Source = new Uri(langType, UriKind.RelativeOrAbsolute) };
                Application.Current.Resources.MergedDictionaries.Remove(resourceDic);
                Application.Current.Resources.MergedDictionaries.Add(resourceDic);
                System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo(languageName);
                System.Threading.Thread.CurrentThread.CurrentCulture = cultureInfo;
                System.Threading.Thread.CurrentThread.CurrentUICulture = cultureInfo;
            }
        }

        public static string GetValue(string key)
        {
            if (!Application.Current.Resources.Contains(key))
            {
                return string.Format("Cannot find value with key of {0}", key);
            }
            return Application.Current.Resources[key].ToString();
        }
    }
}
