using System.Drawing;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using System;

namespace woanware
{
    /// <summary>
    /// 
    /// </summary>
    public class Settings
    {
        public bool AlwaysOnTop { get; set; }
        public bool ColourSevereItems { get; set; }
        public bool RemoveNewLinesOnExport { get; set; }
        public bool MoveFocusToList { get; set; }
        public Point FormLocation { get; set; }
        public Size FormSize { get; set; }
        public FormWindowState FormState { get; set; }
        private const string FILENAME = "Settings.xml";
        public int NumResultsPerPage = 5000;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Load()
        {
            try
            {
                string path = GetPath();

                if (File.Exists(path) == false)
                {
                    return string.Empty;
                }

                XmlSerializer serializer = new XmlSerializer(typeof(Settings));

                FileInfo info = new FileInfo(path);
                using (FileStream stream = info.OpenRead())
                {
                    Settings settings = (Settings)serializer.Deserialize(stream);
                    FormLocation = settings.FormLocation;
                    FormSize = settings.FormSize;
                    FormState = settings.FormState;
                    FormState = settings.FormState;
                    ColourSevereItems = settings.ColourSevereItems;
                    RemoveNewLinesOnExport = settings.RemoveNewLinesOnExport;
                    AlwaysOnTop = settings.AlwaysOnTop;
                    NumResultsPerPage = settings.NumResultsPerPage;
                    MoveFocusToList = settings.MoveFocusToList;
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
                if (System.IO.Directory.Exists(Misc.GetUserDataDirectory()) == false)
                {
                    IO.CreateDirectory(Misc.GetUserDataDirectory());
                }

                XmlSerializer serializer = new XmlSerializer(typeof(Settings));
                using (StreamWriter writer = new StreamWriter(this.GetPath(), false))
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
        /// <returns></returns>
        private string GetPath()
        {
            return System.IO.Path.Combine(Misc.GetUserDataDirectory(), FILENAME);
        }

        /// <summary>
        /// 
        /// </summary>
        public bool FileExists
        {
            get
            {
                return File.Exists(this.GetPath());
            }
        }
    }
}
