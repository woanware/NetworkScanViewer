using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System;
using System.Windows.Forms;

namespace woanware
{
    /// <summary>
    /// 
    /// </summary>
    public class IgnorePlugins
    {
        private List<Plugin> _plugins = new List<Plugin>();
        private const string FILENAME = "IgnorePlugins.xml";

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Load()
        {
            try
            {
                string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"woanware\" + Application.ProductName + @"\");

                if (File.Exists(System.IO.Path.Combine(path, FILENAME)) == false)
                {
                    return string.Empty;
                }

                XmlSerializer serializer = new XmlSerializer(typeof(IgnorePlugins));
                if (File.Exists(System.IO.Path.Combine(path, FILENAME)) == false)
                {
                    return ("Cannot locate Ignore Plugins file: " + System.IO.Path.Combine(path, FILENAME));
                }

                FileInfo info = new FileInfo(System.IO.Path.Combine(path, FILENAME));
                using (FileStream stream = info.OpenRead())
                {
                    IgnorePlugins plugins = (IgnorePlugins)serializer.Deserialize(stream);
                    _plugins = plugins.Plugins;

                    return string.Empty;
                }
            }
            catch (FileNotFoundException fileNotFoundEx)
            {
                return fileNotFoundEx.Message;
            }
            catch (UnauthorizedAccessException unauthAccessEx)
            {
                return unauthAccessEx.Message;
            }
            catch (IOException ioEx)
            {
                return ioEx.Message;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Save()
        {
            try
            {
                string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"woanware\" + Application.ProductName + @"\");
                if (System.IO.Directory.Exists(path) == false)
                {
                    System.IO.Directory.CreateDirectory(path);
                }

                XmlSerializer serializer = new XmlSerializer(typeof(IgnorePlugins));
                using (StreamWriter writer = new StreamWriter(System.IO.Path.Combine(path, FILENAME), false))
                {
                    serializer.Serialize((TextWriter)writer, this);
                    return string.Empty;
                }
            }
            catch (FileNotFoundException fileNotFoundEx)
            {
                return fileNotFoundEx.Message;
            }
            catch (UnauthorizedAccessException unauthAccessEx)
            {
                return unauthAccessEx.Message;
            }
            catch (IOException ioEx)
            {
                return ioEx.Message;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<Plugin> Plugins
        {
            get
            {
                return _plugins;
            }
        }
    }
}