using UnityEngine;
using System.Xml;
using System.Xml.Linq;
using System.Linq;

public class XMLReader : MonoBehaviour
{
    public string fileName = "nome_do_arquivo";
    public string searchIdentifier = "hello_world";

    string text = "";

    void Start()
    {
        // Carrega o arquivo XML como um recurso
        TextAsset xmlAsset = Resources.Load<TextAsset>("XML/" + fileName);

        if (xmlAsset != null)
        {
            // Acesso ao conte�do do arquivo XML
            string conteudoXML = xmlAsset.text;

            // Carrega o XML usando XDocument
            XDocument xmlDoc = XDocument.Parse(conteudoXML);

            // Procura o elemento com o identificador especificado
            XElement element = xmlDoc.Root.Descendants("row")
                .FirstOrDefault(e => e.Element("Identifier")?.Value == searchIdentifier);

            /*O XElement � uma classe da biblioteca System.Xml.Linq que faz parte do namespace System.Xml. 
            Essa classe � usada para representar um elemento XML em um documento XML.
            O XElement fornece uma maneira conveniente de manipular e interagir com elementos XML.
            Ele possui propriedades e m�todos que permitem acessar e modificar os atributos, conte�do e estrutura de um elemento XML.*/

            if (element != null)
            {
                // Obt�m o texto da segunda coluna
                text = element.Element("OriginalText")?.Value;

                // Use o texto conforme necess�rio
                Debug.Log("<color=yellow>" + text + "</color>");
            }
            else
            {
                Debug.LogWarning("Identifier not found: " + searchIdentifier);
            }

            // Imprime uma mensagem em verde se o arquivo XML for encontrado
            Debug.Log("<color=green>XML file found!</color>");
        }
        else
        {
            // Imprime uma mensagem de erro em vermelho se o arquivo XML n�o for encontrado
            Debug.LogError("<color=red>XML file not found!</color>");
        }
    }
}