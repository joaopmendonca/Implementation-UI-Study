using System.Xml;
using UnityEngine;

public class XMLReader : MonoBehaviour
{
    public static XMLReader Instance;

    private void Awake()
    {
      Instance = this;
    }

    // Fun��o para obter a tradu��o com base no arquivo XML e no identificador
    public string GetTranslation(TextAsset xmlFile, string identifier, string columnName)
    {
        if (xmlFile != null)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlFile.text);

            // Acesso aos elementos do XML
            XmlNodeList nodes = xmlDoc.GetElementsByTagName("row");
            foreach (XmlNode node in nodes)
            {
                // Obt�m o identificador do n� atual
                XmlNode identifierNode = node.SelectSingleNode("Identifier");
                if (identifierNode != null && identifierNode.InnerText == identifier)
                {
                    // Obt�m o conte�do da coluna especificada
                    XmlNode columnNode = node.SelectSingleNode(columnName);
                    if (columnNode != null)
                    {
                        return columnNode.InnerText;
                    }
                }
            }
        }

        return string.Empty; // Retorna uma string vazia se a tradu��o n�o for encontrada
    }    
}
