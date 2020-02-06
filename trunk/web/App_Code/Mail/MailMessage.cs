using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Net.Mail;

namespace Original.Mail
{
    /// <summary>
    /// Classe responsável herdada do (System.Net.Mail.MailMessage), com a algumas expansões
    /// </summary>
    public class MailMessage : System.Net.Mail.MailMessage
    {
        private IDictionary _dictionary = new Dictionary<string, string>();
        private string _pathTemplate = string.Empty;

        public MailMessage()
        {
            //Contructor
        }

        /// <summary>
        /// Propriedade responsável pela implementação da funcionalidade de substituição de palavras chaves no Template por parametros da aplicação.
        /// </summary>
        /// <example> This sample shows how to call the GetZero method.
        /// <code>
        ///   MailMessage msg = new MailMessage();
        ///   msg.PathTemplate = "C:\Template\template.htm";
        ///   msg.Dicionary.Add("#nome#", variavel);
        /// </code>
        /// </example>
        public IDictionary Dicionary
        {
            get { return this._dictionary; }
        }

        /// <summary>
        /// Propriedade responsável em indicar o caminho físico do Template de e-mail para a aplicação.
        /// </summary>
        /// <example> This sample shows how to call the GetZero method.
        /// <code>
        ///   MailMessage msg = new MailMessage();
        ///   msg.PathTemplate = "C:\Template\template.htm";
        /// </code>
        /// </example>
        public string PathTemplate
        {
            get { return this._pathTemplate; }
            set { this._pathTemplate = value; }
        }
    }
}
