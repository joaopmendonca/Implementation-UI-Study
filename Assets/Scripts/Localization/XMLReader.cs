using System.Xml;
using UnityEngine;

public class XMLReader : MonoBehaviour
{
    public static XMLReader Instance;

    private void Awake()
    {
      Instance = this;
    }

    // Função para obter a tradução com base no arquivo XML e no identificador
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
                // Obtém o identificador do nó atual
                XmlNode identifierNode = node.SelectSingleNode("Identifier");
                if (identifierNode != null && identifierNode.InnerText == identifier)
                {
                    // Obtém o conteúdo da coluna especificada
                    XmlNode columnNode = node.SelectSingleNode(columnName);
                    if (columnNode != null)
                    {
                        return columnNode.InnerText;
                    }
                }
            }
        }

        return string.Empty; // Retorna uma string vazia se a tradução não for encontrada
    }    
}
