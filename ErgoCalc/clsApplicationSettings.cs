using System;
using System.Drawing;
using System.Xml;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Utilidades
{
    class clsApplicationSettings
    {
        // Nombre del archivo que almacena los datos.
        private readonly string _fileName;

        // Documento XML para guardar los datos.
        private readonly XmlDocument _document;

        public clsApplicationSettings(string fileName)
        {
            //
            // TODO: Add constructor logic here
            //

            // Assign the file name.
            _fileName = fileName;

            // If the file already exists, load it. Otherwise, create a new document.
            _document = new XmlDocument();
            try
            {
                _document.Load(this._fileName);
            }
            catch (Exception)
            {
                // Create a new XML document and set the root node.
                _document = new XmlDocument();
                _document.AppendChild(_document.CreateElement("ProgramSettings"));
            }
        }

        /// <summary>
        /// Guarda los datos en el archivo
        /// </summary>
        public void Save()
        {
            _document.Save(this._fileName);
        }

        /// <summary>
        /// Lee un valor XML del archivo de datos
        /// </summary>
        /// <param name="section">Sección del archivo XML</param>
        /// <param name="name">Nombre del nodo</param>
        /// <returns>Cadena de texto del archivo XML</returns>
        public string GetValue(string section, string name)
        {
            try
            {
                return _document.DocumentElement.SelectSingleNode(section + "/" + name).InnerText;
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// Guarda un valor XML en el archivo de datos
        /// </summary>
        /// <param name="section">Sección del archivo XML</param>
        /// <param name="name">Nombre del nodo</param>
        /// <param name="value">Valor a guardar</param>
        public void SetValue(string section, string name, string value)
        {
            // If the section does not exist, create it.
            XmlNode sectionNode = _document.DocumentElement.SelectSingleNode(section);
            if (sectionNode == null)
                sectionNode = _document.DocumentElement.AppendChild(_document.CreateElement(section));
            // If the node does not exist, create it.
            XmlNode node = sectionNode.SelectSingleNode(name);
            if (node == null)
                node = sectionNode.AppendChild(_document.CreateElement(name));

            // Set the value.
            node.InnerText = value;
        }
    }

    class ApplicationSettingsData
    {
        #region Member variables

        public bool bCenterWindow;
        public bool bTransparency;
        public byte nTransparencyValue;
        public bool bOnlyParents;
        public Color cRectColor;
        public Int32 nRectWidth;
        //public Int32 idioma;

        #endregion Member variables

        #region Class constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public ApplicationSettingsData()
        {
            SetDefaults();
        }

        /// <summary>
        /// Overloaded constructor.
        /// </summary>
        /// <param name="settings"></param>
        public ApplicationSettingsData(ApplicationSettingsData settings)
        {
            bCenterWindow       = settings.bCenterWindow;
            bTransparency       = settings.bTransparency;
            nTransparencyValue  = settings.nTransparencyValue;
            bOnlyParents        = settings.bOnlyParents;
            cRectColor          = settings.cRectColor;
            nRectWidth          = settings.nRectWidth;
            //idioma = (Int32)Language.System;
        }

        #endregion Class constructors

        #region Class methods

        /// <summary>
        /// Sets the member variable default values.
        /// </summary>
        public void SetDefaults()
        {
            bCenterWindow       = true;
            bTransparency       = false;
            nTransparencyValue  = 0;
            bOnlyParents        = false;
            cRectColor          = Color.Black;
            nRectWidth          = 1;
        }
        
        #endregion Class methods
    }
}
